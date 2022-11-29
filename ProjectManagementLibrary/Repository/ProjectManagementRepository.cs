using Microsoft.EntityFrameworkCore;
using ProjectManagementLibrary.Data;
using ProjectManagementSystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementLibrary.Repository
{
    public class ProjectManagementRepository : GenericRepository
    {
        private readonly ProjectManagementSystemDBContext _context;

        public ProjectManagementRepository(ProjectManagementSystemDBContext managementSystemDBContext) : base(managementSystemDBContext)
        {
            _context = managementSystemDBContext;
        }
        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            try
            {
                var res = await _context.Projects
                    .Include(x => x.ProjectTypeNavigation)
                   



                   .ToListAsync();
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                var res = await _context.Employees
                    .Include(x => x.GenderNavigation)
                    .Include(x => x.RoleNavigation)
                   .ToListAsync();
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
