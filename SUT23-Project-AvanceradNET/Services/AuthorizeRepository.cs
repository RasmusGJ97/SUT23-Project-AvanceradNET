using ClassLibrary_Project_AvanceradNET.Models;
using Microsoft.EntityFrameworkCore;
using SUT23_Project_AvanceradNET.Data;
using System;

namespace SUT23_Project_AvanceradNET.Services
{
    public class AuthorizeRepository : IAuthorize<Company>
    {
        private ProjectDbContext _projectDbContext;
        public AuthorizeRepository(ProjectDbContext appDbContext)
        {
            _projectDbContext = appDbContext;
        }
        public async Task<Company> GetAuthorized(string name, string password)
        {

            if (!string.IsNullOrEmpty(name))
            {
                var company = await _projectDbContext.Companys.FirstOrDefaultAsync(c => c.CompanyName == name && c.CompanyPassword == password);
                return company;
            }
            return null;

        }
    }
}
