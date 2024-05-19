using ClassLibrary_Project_AvanceradNET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUT23_Project_AvanceradNET.Data;
using SUT23_Project_AvanceradNET.Services;
using System;

namespace SUT23_Project_AvanceradNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IBookingSystem<Customer> _customer;
        private IBookingSystem<Appointment> _appointmentBS;
        private IAppointment<Appointment> _appointmentA;
        private IBookingSystem<ChangeLog> _changeLogRepository;
        private IAppointment<ChangeLog> _changeLog;

        public CustomerController(IBookingSystem<Customer> cBooking, IBookingSystem<Appointment> aBooking, 
            IAppointment<Appointment> aAppoint, IBookingSystem<ChangeLog> changeLogRepo, IAppointment<ChangeLog> changeLog)
        {
            _customer = cBooking;
            _appointmentBS = aBooking;
            _appointmentA = aAppoint;
            _changeLogRepository = changeLogRepo;
            _changeLog = changeLog;
        }

        [HttpGet("{id:int} - Get Customer by id:")]
        public async Task<ActionResult<Customer>> GetSingle(int id)
        {
            try
            {
                var customer = await _customer.GetSingle(id);
                if (customer == null)
                {
                    return NotFound();
                }

                var appointments = customer.Appointment.Select(appointment => new
                {
                    appointment.AppointmentId,
                    appointment.AppointmentStart,
                    appointment.AppointmentEnd
                });

                var result = new
                {
                    customer.CustomerId,
                    customer.CustomerFName,
                    customer.CustomerLName,
                    appointments
                };
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrive data from Database . . .");
            }
        }

        [HttpGet(" - Get all customers")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customer = await _customer.GetAll();

                var result = customer.Select(c => new
                {
                    c.CustomerId,
                    c.CustomerFName,
                    c.CustomerLName
                });
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrive data from Database . . .");
            }
        }

        [HttpPost(" - Add appointment 'yyyy-mm-ddThh:mm:ss'")]
        public async Task<ActionResult<Appointment>> Add(DateTime startTime, DateTime endTime, int CustomerId)
        {
            try
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to create data in the Database . . .");
            }
        }




        [HttpPut(" - Update appointment 'yyyy-mm-ddThh:mm:ss'")]
        public async Task<ActionResult<Appointment>> UpdateAppointment(int appointmentId, DateTime startTime, DateTime endTime)
        {
            try
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to update Data in database . . .");
            }
        }

        [HttpDelete(" - Delete appointment")]
        public async Task<ActionResult<Appointment>> DeleteAppointment(int appointmentId)
        {
            try
            {
                var appointmentToDelete = await _appointmentBS.GetSingle(appointmentId);

                if (appointmentToDelete == null)
                {
                    return NotFound($"Appointment with {appointmentId} not found . . .");
                }

                return await _appointmentBS.Delete(appointmentId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to update Data in database . . .");
            }
        }

    }
}
