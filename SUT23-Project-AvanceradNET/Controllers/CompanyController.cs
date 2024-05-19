using ClassLibrary_Project_AvanceradNET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SUT23_Project_AvanceradNET.Services;
using System.Globalization;

namespace SUT23_Project_AvanceradNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private IBookingSystem<Customer> _customer;
        private IBookingSystem<Appointment> _appointmentBS;
        private IAppointment<Appointment> _appointmentA;
        private IBookingSystem<ChangeLog> _changeLogRepository;
        private IAppointment<ChangeLog> _changeLog;
        private IAuthorize<Company> _company;

        public CompanyController(IBookingSystem<Customer> cBooking, IBookingSystem<Appointment> aBooking, IAppointment<Appointment> aAppoint, 
            IBookingSystem<ChangeLog> changeLogRepo, IAppointment<ChangeLog> changeLog, IAuthorize<Company> company)
        {
            _customer = cBooking;
            _appointmentBS = aBooking;
            _appointmentA = aAppoint;
            _changeLogRepository = changeLogRepo;
            _changeLog = changeLog;
            _company = company;
        }

        [HttpGet(" - Get Customer by id:")]
        public async Task<ActionResult<Customer>> GetSingle(int customerId, string companyName, string companyPassword)
        {
            try
            {
                var company = await _company.GetAuthorized(companyName, companyPassword);
                if (company != null)
                {
                    var customer = await _customer.GetSingle(customerId);
                    if (customer == null)
                    {
                        return NotFound();
                    }
                    return Ok(customer);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrive data from Database . . .");
            }
        }

        [HttpGet(" - Get all customers")]
        public async Task<IActionResult> GetAll(string companyName, string companyPassword, string? sortBy, string? filterBy)
        {
            try
            {
                var company = await _company.GetAuthorized(companyName, companyPassword);
                if (company != null)
                {
                    var customer = await _customer.GetAll();

                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        switch (sortBy)
                        {
                            case "CustomerId":
                                customer = customer.OrderBy(c => c.CustomerId);
                                break;
                            case "CustomerFName":
                                customer = customer.OrderBy(c => c.CustomerFName);
                                break;
                            case "CustomerLName":
                                customer = customer.OrderBy(c => c.CustomerLName);
                                break;
                            default:
                                return BadRequest("Invalid sortBy parameter . . .");
                        }
                    }

                    if (!string.IsNullOrEmpty(filterBy))
                    {
                        customer = customer.Where(c => c.CustomerFName.Contains(filterBy) || c.CustomerLName.Contains(filterBy));
                    }

                    var result = customer.Select(c => new
                    {
                        c.CustomerId,
                        c.CustomerFName,
                        c.CustomerLName,
                        c.CustomerEmail,
                        c.CustomerPhone
                    });
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrive data from Database . . .");
            }
        }

        [HttpPost(" - Add appointment 'yyyy-mm-ddThh:mm:ss'")]
        public async Task<ActionResult<Appointment>> Add(
            DateTime startTime, DateTime endTime, int CustomerId, string companyName, string companyPassword)
        {
            try
            {
                var company = await _company.GetAuthorized(companyName, companyPassword);
                if (company != null)
                {
                    var newAppointment = new Appointment
                    {
                        AppointmentStart = startTime,
                        AppointmentEnd = endTime,
                        CustomerId = CustomerId
                    };

                    var createdAppointment = await _appointmentBS.Add(newAppointment);
                    return CreatedAtAction(nameof(GetSingle),
                        new { id = createdAppointment.AppointmentId }, createdAppointment);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to create data in the Database . . .");
            }
        }

        [HttpPut(" - Update appointment 'yyyy-mm-ddThh:mm:ss'")]
        public async Task<ActionResult<Appointment>> UpdateAppointment(
            int appointmentId, DateTime startTime, DateTime endTime, string companyName, string companyPassword)
        {
            try
            {
                var company = await _company.GetAuthorized(companyName, companyPassword);
                if (company != null)
                {
                    var appointmentToUpdate = await _appointmentBS.GetSingle(appointmentId);

                    if (appointmentToUpdate == null)
                    {
                        return NotFound($"Appointment with {appointmentId} not found . . .");
                    }

                    var changes = new ChangeLog
                    {
                        OldStart = appointmentToUpdate.AppointmentStart,
                        OldEnd = appointmentToUpdate.AppointmentEnd,
                        NewStart = startTime,
                        NewEnd = endTime,
                        ChangedDate = DateTime.Now,
                        AppointmentId = appointmentId
                    };

                    //await _changeLogRepository.Add(changes);

                    appointmentToUpdate.AppointmentStart = startTime;
                    appointmentToUpdate.AppointmentEnd = endTime;

                    var updatedAppointment = await _appointmentBS.Update(appointmentToUpdate);

                    return Ok(updatedAppointment);
                }
                return BadRequest();                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to update Data in database . . .");
            }
        }

        [HttpDelete(" - Delete appointment")]
        public async Task<ActionResult<Appointment>> DeleteAppointment(int appointmentId, string companyName, string companyPassword)
        {
            try
            {
                var company = await _company.GetAuthorized(companyName, companyPassword);
                if (company != null)
                {
                    var appointmentToDelete = await _appointmentBS.GetSingle(appointmentId);

                    if (appointmentToDelete == null)
                    {
                        return NotFound($"Appointment with {appointmentId} not found . . .");
                    }

                    return await _appointmentBS.Delete(appointmentId);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to update Data in database . . .");
            }
        }

        [HttpGet(" - Search appointment between dates")]
        public async Task<IActionResult> GetAllAppointments(
            string companyName, string companyPassword, DateTime firstDate, DateTime lastDate, string? sortBy)
        {
            try
            {
                var company = await _company.GetAuthorized(companyName, companyPassword);
                if (company != null)
                {
                    var appointment = await _appointmentA.SearchAll(firstDate, lastDate);

                    if (!string.IsNullOrEmpty(sortBy))
                    {
                        switch (sortBy)
                        {
                            case "AppointmentId":
                                appointment = appointment.OrderBy(c => c.AppointmentId);
                                break;
                            case "AppointmentStart":
                                appointment = appointment.OrderBy(c => c.AppointmentStart);
                                break;
                            default:
                                return BadRequest("Invalid sortBy parameter . . .");
                        }
                    }

                    return Ok(appointment);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrive data from Database . . .");
            }
        }

        [HttpGet(" - Search appointment for a customer between dates")]
        public async Task<IActionResult> GetSpecificAppointments(
            string companyName, string companyPassword, DateTime firstDate, DateTime lastDate, int customerId)
        {
            try
            {
                var company = await _company.GetAuthorized(companyName, companyPassword);
                if (company != null)
                {
                    var appointment = await _appointmentA.SearchSpecific(firstDate, lastDate, customerId);
                    
                    return Ok(appointment);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrive data from Database . . .");
            }
        }
    }
}
