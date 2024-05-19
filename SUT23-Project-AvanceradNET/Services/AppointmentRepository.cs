using ClassLibrary_Project_AvanceradNET.Models;
using Microsoft.EntityFrameworkCore;
using SUT23_Project_AvanceradNET.Data;

namespace SUT23_Project_AvanceradNET.Services
{
    public class AppointmentRepository : IBookingSystem<Appointment>, IAppointment<Appointment>
    {
        private ProjectDbContext _projectDbContext;
        public AppointmentRepository(ProjectDbContext appDbContext)
        {
            _projectDbContext = appDbContext;
        }
        public async Task<Appointment> Add(Appointment newEntity)
        {
            var result = await _projectDbContext.Appointments.AddAsync(newEntity);
            await _projectDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Appointment> Delete(int id)
        {
            var result = await _projectDbContext.Appointments.FirstOrDefaultAsync(o => o.AppointmentId == id);
            if (result != null)
            {
                _projectDbContext.Appointments.Remove(result);
                await _projectDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Appointment> GetSingle(int id)
        {
            return await _projectDbContext.Appointments.Include(o => o.Customer).FirstOrDefaultAsync(o => o.AppointmentId == id);
        }

        public async Task<Appointment> Update(Appointment Entity)
        {
            var result = await _projectDbContext.Appointments.FirstOrDefaultAsync(o => o.AppointmentId == Entity.AppointmentId);

            if (result != null)
            {
                result.AppointmentStart = Entity.AppointmentStart;
                result.AppointmentEnd = Entity.AppointmentEnd;
                result.Customer = Entity.Customer;
                

                var newLog = (new ChangeLog
                {
                    OldStart = Entity.AppointmentStart,
                    OldEnd = Entity.AppointmentEnd,
                    NewStart = result.AppointmentStart,
                    NewEnd = result.AppointmentEnd,
                    ChangedDate = DateTime.Now,
                    AppointmentId = Entity.AppointmentId,
                });
                _projectDbContext.Add(newLog);

                await _projectDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Appointment>> SearchAll(DateTime searchDateFirst, DateTime searchDateLast)
        {
            IQueryable<Appointment> query = _projectDbContext.Appointments;

            query = query.Where(a => a.AppointmentStart >= searchDateFirst && a.AppointmentEnd <=searchDateLast);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> SearchSpecific(DateTime searchDateFirst, DateTime searchDateLast, int CustomerId)
        {
            IQueryable<Appointment> query = _projectDbContext.Appointments;

            query = query.Where(a => a.AppointmentStart >= searchDateFirst && a.AppointmentEnd <= searchDateLast && a.CustomerId == CustomerId);

            return await query.ToListAsync();
        }

        Task<IEnumerable<Appointment>> IBookingSystem<Appointment>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
