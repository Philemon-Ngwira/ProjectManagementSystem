using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using ProjectManagementSystem.Client;
using ProjectManagementSystem.Client.Shared;
using MudBlazor;
using ProjectManagementSystem.Shared.Models;
using ProjectManagementSystem.Client.Services;


namespace ProjectManagementSystem.Client.Pages
{
    public partial class IndexBase : ComponentBase
    {

        [Inject]
        ISnackbar Snackbar { get; set; }

        protected TableApplyButtonPosition applyButtonPosition = TableApplyButtonPosition.End;
        protected List<string> editEvents = new();
        protected int index = -1; //default value cannot be 0 -> first selectedindex is 0.
        protected int dataSize = 4;
        protected double[] data = { 77, 25 };
        protected string[] labels = { "Completed", "Cancelled" };
        protected Employee selectedEmployee = new();
        protected Employee EmployeeBeforeEdit = new();
        protected Employee selectedItem1 = null;
        protected string searchString = "";
        protected Project project1 = new();
        protected Project project2 = new();
        protected Project project3 = new();
        [Inject] IGenericRepositoryService _genericRepositoryService { get; set; }
        [Inject] AuthenticationStateProvider AuthenticationState { get; set; }
        Random random = new Random();
        protected ChartOptions options = new ChartOptions();
        protected IList<Project> _projects = new List<Project>();
        public List<ChartSeries> Series = new List<ChartSeries>()
        {new ChartSeries()
        {Name = "POP", Data = new double[]{90, 79, 72, 69, 62, 62, 55, 65, 70}}, new ChartSeries()
        {Name = "ABDUCTION", Data = new double[]{35, 41, 35, 51, 49, 62, 69, 91, 148}}, };
        public IList<Employee> employees = new List<Employee>();
        protected IList<Employee> employeeList = new List<Employee>();
        public string[] XAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep" };
        protected bool AuthenticatedUser = false;
        protected string Project1Name = string.Empty;
        protected string Project2Name = string.Empty;
        protected string Project3Name = string.Empty;
        protected double Value0;
        protected double Value1;
        protected double Value2;
        protected double ValueDis;
        protected double ValueDis1;
        protected override async void OnInitialized()
        {
            var user = (await AuthenticationState.GetAuthenticationStateAsync()).User.Identity;
            if (user != null && user.IsAuthenticated)
            {
                await GetProjects();
                AuthenticatedUser = true;
                options.InterpolationOption = InterpolationOption.NaturalSpline;
                options.YAxisFormat = "c2";
                await GetProjectImages();
                await GetEmployees();
                FindPercent();
                Console.WriteLine(Value0);
                Console.WriteLine(Value1);
                ValueDis = Value0;
                ValueDis1 = Value1;
            }

        }

        protected void RandomizeData()
        {
            var new_data = new double[dataSize];
            for (int i = 0; i < new_data.Length; i++)
                new_data[i] = random.NextDouble() * 100;
            data = new_data;
            StateHasChanged();
        }

        protected void AddDataSize()
        {
            if (dataSize < 20)
            {
                dataSize = dataSize + 1;
                RandomizeData();
            }
        }

        protected void RemoveDataSize()
        {
            if (dataSize > 0)
            {
                dataSize = dataSize - 1;
                RandomizeData();
            }
        }
        protected async Task GetProjects()
        {
            var result = await _genericRepositoryService.GetAllAsync<Project>("api/ProjectManagement/GetProjects");
            _projects = result.ToList();
        }
        protected async Task GetProjectImages()
        {
           
            if (_projects[0] != null)
            {
                project1 = _projects[0];
                Project1Name = project1.ProjectName;
            }
            if (_projects[1] != null)
            {
                project2 = _projects[1];
                Project2Name = project2.ProjectName;
            }
            //if (_projects[2] != null)
            //{
            //    project3 = _projects[2];
            //    Project3Name = project3.ProjectName;
            //}

        }

        protected void FindPercent()
        {
            
            #region Value 0
            if (_projects[0].ProjectProgress == 1)
            {
                Value0 = 0;
            }
            else if (_projects[0].ProjectProgress == 2)
            {
                Value0 = 25;
            }
            else if (_projects[0].ProjectProgress == 3)
            {
                Value0 = 75;
            }
            else if (_projects[0].ProjectProgress == 4)
            {
                Value0 = 100;
            }
            #endregion
            #region Value 1
            if (_projects[1].ProjectProgress == 1)
            {
                Value1 = 0;
            }
            else if (_projects[1].ProjectProgress == 2)
            {
                Value1 = 25;
            }
            else if (_projects[1].ProjectProgress == 3)
            {
                Value1 = 75;
            }
            else if (_projects[1].ProjectProgress == 4)
            {
                Value1 = 100;
            }
            #endregion
            #region Value 2
            //if (_projects[2].ProjectProgress == 1)
            //{
            //    Value2 = 0;
            //}
            //else if (_projects[2].ProjectProgress == 2)
            //{
            //    Value2 = 25;
            //}
            //else if (_projects[2].ProjectProgress == 3)
            //{
            //    Value2 = 75;
            //}
            //else if (_projects[2].ProjectProgress == 4)
            //{
            //    Value2 = 100;
            //}
            #endregion
        }
        protected async Task GetEmployees()
        {
            var result = await _genericRepositoryService.GetAllAsync<Employee>("api/ProjectManagement/GetEmployees");
            employees = result.ToList();
            employeeList = employees;
        }
        #region Table
        private void ClearEventLog()
        {
            editEvents.Clear();
        }

        private void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }

        protected void BackupItem(object employee)
        {
            EmployeeBeforeEdit = new()
            { Id = ((Employee)employee).Id, FirstName = ((Employee)employee).FirstName, LastName = ((Employee)employee).LastName, Role = ((Employee)employee).Role, };
            AddEditionEvent($"RowEditPreview event: made a backup of employee {((Employee)employee).FirstName}");
        }

        protected void ItemHasBeenCommitted(object employee)
        {
            AddEditionEvent($"RowEditCommit event: Changes to employee {((Employee)employee).FirstName} committed");
        }

        protected void ResetItemToOriginalValues(object employee)
        {
            ((Employee)employee).Id = EmployeeBeforeEdit.Id;
            ((Employee)employee).FirstName = EmployeeBeforeEdit.FirstName;
            ((Employee)employee).LastName = EmployeeBeforeEdit.LastName;
            ((Employee)employee).Role = EmployeeBeforeEdit.Role;
            AddEditionEvent($"RowEditCancel event: Editing of employee {((Employee)employee).FirstName} cancelled");
            Snackbar.Add("Changes Canceled", Severity.Info);
        }

        protected bool FilterFunc(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (employee.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (employee.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            //if ($"{employee.Shift}".Contains(searchString))
            //    return true;
            return false;
        }

        private bool FilterFunc1(Employee element) => FilterFunc(element, searchString);

        private bool FilterFunc(Employee employee, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (employee.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (employee.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
        #endregion
    }

}