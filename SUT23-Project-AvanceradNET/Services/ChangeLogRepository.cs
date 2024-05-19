using ClassLibrary_Project_AvanceradNET.Models;
using Microsoft.EntityFrameworkCore;
using SUT23_Project_AvanceradNET.Data;

namespace SUT23_Project_AvanceradNET.Services
{
    public class ChangeLogRepository : IBookingSystem<ChangeLog>, IAppointment<ChangeLog>
    {
        private ProjectDbContext _projectDbContext;
        public ChangeLogRepository(ProjectDbContext appDbContext)
        {
            _projectDbContext = appDbContext;
        }

        public async Task<ChangeLog> Add(ChangeLog newEntity)
        {
            var result = await _projectDbContext.ChangeLogs.AddAsync(newEntity);
            await _projectDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public Task<ChangeLog> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ChangeLog>> GetAll()
        {
            return await _projectDbContext.ChangeLogs.ToListAsync();
        }

        public Task<ChangeLog> GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeLog>> SearchAll(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeLog>> SearchSpecific(DateTime start, DateTime end, int id)
        {
            throw new NotImplementedException();
        }

        public Task<ChangeLog> Update(ChangeLog Entity)
        {
            throw new NotImplementedException();
        }
    }
}
