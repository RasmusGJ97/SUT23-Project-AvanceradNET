using ClassLibrary_Project_AvanceradNET.Models;
using Microsoft.EntityFrameworkCore;
using SUT23_Project_AvanceradNET.Data;
using System;

namespace SUT23_Project_AvanceradNET.Services
{
    public class CustomerRepository : IBookingSystem<Customer>
    {
        private ProjectDbContext _projectDbContext;
        public CustomerRepository(ProjectDbContext appDbContext)
        {
            _projectDbContext = appDbContext;
        }
        public async Task<Customer> Add(Customer newEntity)
        {
            var result = await _projectDbContext.Customers.AddAsync(newEntity);
            await _projectDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Customer> Delete(int id)
        {
            var result = await _projectDbContext.Customers.FirstOrDefaultAsync(o => o.CustomerId == id);
            if (result != null)
            {
                _projectDbContext.Customers.Remove(result);
                await _projectDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Customer> GetSingle(int id)
        {
            return await _projectDbContext.Customers.Include(o => o.Appointment).FirstOrDefaultAsync(o => o.CustomerId == id);
        }

        public async Task<Customer> Update(Customer Entity)
        {
            var result = await _projectDbContext.Customers.FirstOrDefaultAsync(o => o.CustomerId == Entity.CustomerId);

            if (result != null)
            {
                result.CustomerFName = Entity.CustomerFName;
                result.CustomerLName = Entity.CustomerFName;
                result.CustomerPhone = Entity.CustomerPhone;
                result.CustomerEmail = Entity.CustomerEmail;

                await _projectDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _projectDbContext.Customers.ToListAsync();
        }
    }
}
