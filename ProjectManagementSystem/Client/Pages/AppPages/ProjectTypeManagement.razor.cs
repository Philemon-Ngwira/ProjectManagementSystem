using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProjectManagementSystem.Client.Services;
using ProjectManagementSystem.Shared.Models;

namespace ProjectManagementSystem.Client.Pages.AppPages
{
    public partial class ProjectTypeManagement
    {
        protected ProjectType projectType = new();
        protected IList<ProjectType> projectTypes = new List<ProjectType>();
        [Inject]
        IGenericRepositoryService _genericRepositoryService { get; set; }
       
        protected MudTable<ProjectType> mudTable = new();
        protected List<string> clickedEvents = new();
        protected int selectedRowNumber;
        protected override async Task OnInitializedAsync()
        {
            await GetProjectTypes();
        }

        protected async Task GetProjectTypes()
        {
            var result = await _genericRepositoryService.GetAllAsync<ProjectType>("api/ProjectManagement/GetProjectTypes");
            projectTypes = result.ToList();
        }

        private void RowClickEvent(TableRowClickEventArgs<ProjectType> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private string SelectedRowClassFunc(ProjectType Project, int rowNumber)
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
    }
}