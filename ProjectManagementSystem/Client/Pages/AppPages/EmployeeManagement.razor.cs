using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProjectManagementSystem.Client.Pages.Dialogs;
using ProjectManagementSystem.Client.Services;
using ProjectManagementSystem.Shared.Models;

namespace ProjectManagementSystem.Client.Pages.AppPages
{
    public partial class EmployeeManagement
    {
        protected IList<Employee> employees = new List<Employee>();
        protected MudTable<Employee> mudTable = new MudTable<Employee>();
        private List<string> clickedEvents = new();
        private int selectedRowNumber = -1;
        protected bool isVisible;
        [Inject]IGenericRepositoryService _genericRepositoryService { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetEmployees();
        }

        protected async Task GetEmployees()
        {
            var result = await _genericRepositoryService.GetAllAsync<Employee>("api/ProjectManagement/GetEmployees");
            employees = result.ToList();
        }
        private void RowClickEvent(TableRowClickEventArgs<Employee> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");

        }

        private string SelectedRowClassFunc(Employee Project, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (mudTable.SelectedItem != null && mudTable.SelectedItem.Equals(Project))
            {
                selectedRowNumber = rowNumber;
                clickedEvents.Add($"Selected Row: {rowNumber}");
                return "selected";

            }
            else
            {
                return string.Empty;
            }
        }
        protected void OpenDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth=true};
            DialogService.Show<AddEmployeeDialog>("Add New Person to Team", options);
            isVisible = true;
        }
    }
}