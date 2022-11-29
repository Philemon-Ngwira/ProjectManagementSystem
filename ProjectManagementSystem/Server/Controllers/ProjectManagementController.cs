using Microsoft.AspNetCore.Mvc;
using ProjectManagementLibrary.Repository;
using ProjectManagementSystem.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectManagementController : ControllerBase
    {

        private readonly ProjectManagementRepository _projectManagementRepository;

        public ProjectManagementController(ProjectManagementRepository managementrepo)
        {
            _projectManagementRepository = managementrepo;
        }
        // GET: api/<ProjectManagementController>
        
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var projects = await _projectManagementRepository.GetAllAsync<AspNetRole>();
            return Ok(projects);
        }
        [HttpGet("GetDelayTypes")]
        public async Task<IActionResult> GetDelaytypes()
        {
            var projects = await _projectManagementRepository.GetAllAsync<DelayType>();
            return Ok(projects);
        }
        [HttpGet("GetGenders")]
        public async Task<IActionResult> GetGenders()
        {
            var genders = await _projectManagementRepository.GetAllAsync<GenderTable>();
            return Ok(genders);
        }
        [HttpGet("GetProjects")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectManagementRepository.GetAllProjectsAsync();
            return Ok(projects);
        }
        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var projects = await _projectManagementRepository.GetAllEmployeesAsync();
            return Ok(projects);
        }
        [HttpGet("GetProjectTypes")]
        public async Task<IActionResult> GetProjectTypes()
        {
            var projects = await _projectManagementRepository.GetAllAsync<ProjectType>();
            return Ok(projects);
        }
        // GET api/<ProjectManagementController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProjectManagementController>
        [HttpPost("SaveProject")]
        public async Task<IActionResult> PostProjectAsync(Project project)
        {
            var projects = await _projectManagementRepository.SaveAsync(project);

            return Ok(projects);
        }

        [HttpPost("SaveEmployee")]
        public async Task<IActionResult> PostEmployeeAsync(Employee employee)
        {
            var employee1 = await _projectManagementRepository.SaveAsync(employee);

            return Ok(employee1);
        }
        [HttpPost("SaveRole")]
        public async Task<IActionResult> PostRoleAsync(AspNetRole role)
        {
            var projects = await _projectManagementRepository.SaveAsync(role);

            return Ok(projects);
        }

        // PUT api/<ProjectManagementController>/5
        [HttpPut("UpdateProject")]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            var result  =  await _projectManagementRepository.UpdateAsync<Project>(project);
            return Ok(result);
        }

        // DELETE api/<ProjectManagementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
