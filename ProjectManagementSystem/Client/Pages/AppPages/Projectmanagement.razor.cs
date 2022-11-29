using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProjectManagementSystem.Client.Pages.Dialogs;
using ProjectManagementSystem.Client.Services;
using ProjectManagementSystem.Shared.Models;

namespace ProjectManagementSystem.Client.Pages.AppPages
{
    public partial class Projectmanagement
    {
        // Table Code
        private string ProjectProjectName = string.Empty;
        DateTime? date = DateTime.Today;

        MudTimelineItem itemPreProd = new();
        MudTimelineItem itemProd = new();
        MudTimelineItem itemTest = new();
        MudTimelineItem itemRelease = new();
        MudAlert MudAlertPreProduction { get; set; }
        MudAlert MudAlertProduction { get; set; }
        MudAlert MudAlertTesting { get; set; }
        MudAlert MudAlertRelease { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        protected string DelayReason = string.Empty;
        private int selectedRowNumber = -1;
        private MudTable<CompletedProject> mudTable;
        private MudTable<Project> mudTableCurrentProject;
        private List<string> clickedEvents = new();
        protected Project project = null;
        protected IList<Project> projects = new List<Project>();
        protected IList<CompletedProject> completedProjects = new List<CompletedProject>();
        [Inject] IGenericRepositoryService _genericRepositoryService { get; set; }

        protected override async Task OnInitializedAsync()
        {

            await GetCurrentProjects();

        }
        protected async Task GetCurrentProjects()
        {
            var result = await _genericRepositoryService.GetAllAsync<Project>("api/ProjectManagement/GetProjects");
            projects = result.ToList();
        }
        private void RowClickEvent(TableRowClickEventArgs<CompletedProject> tableRowClickEventArgs)
        {

            clickedEvents.Add("Row has been clicked");
        }
        private void RowClickEventCurrentProject(TableRowClickEventArgs<Project> tableRowClickEventArgs)
        {

            project = tableRowClickEventArgs.Item;
            UpdateProject(project).GetAwaiter();
        }
        async Task UpdateProject(Project project)
        {
            var parameters = new DialogParameters { ["project"] = project };
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium };
            var dialog = DialogService.Show<EditProjectDialog>("Edit Project", parameters, options);
            var result = await dialog.Result;
        }
        private string SelectedRowClassFunc(CompletedProject Project, int rowNumber)
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

        //Other Code
       

        #region TimeLine
        protected void addItem()
        {
            DateTime PreProductionCurrentdate = DateTime.Today;
            DateTime ProductionCompletionCurrentDate = DateTime.Today;
            DateTime TestingCompletionCurrentDate = DateTime.Today;
            DateTime ReleaseDate = DateTime.Today;

       
            #region Pre Production
            if (PreProductionCurrentdate <= project.PreProductionCompletionDate & project.ProjectProgress == 2)
            {
                MudAlertPreProduction.Severity = Severity.Success;
                itemPreProd.Color = Color.Success;
            }
            else if (PreProductionCurrentdate > project.PreProductionCompletionDate & project.ProjectProgress == 2)
            {
                MudAlertPreProduction.Severity = Severity.Success;
                itemPreProd.Color = Color.Success;
            }
            else if (PreProductionCurrentdate <= project.PreProductionCompletionDate && project.ProjectProgress == 1)
            {
                MudAlertPreProduction.Severity = Severity.Info;
                itemPreProd.Color = Color.Info;
            }
            else
            {
                MudAlertPreProduction.Severity = Severity.Error;
               
                itemPreProd.Color = Color.Error;
            }
            #endregion

            #region Production
            if (ProductionCompletionCurrentDate <= project.ProductionCompletion & project.ProjectProgress == 3)
            {
                MudAlertProduction.Severity = Severity.Success;
                itemProd.Color = Color.Success;
            }
            else if (ProductionCompletionCurrentDate > project.ProductionCompletion & project.ProjectProgress == 3)
            {
                MudAlertProduction.Severity = Severity.Success;
                itemProd.Color = Color.Success;
            }
            else if (ProductionCompletionCurrentDate <= project.ProductionCompletion && project.ProjectProgress == 2)
            {
                MudAlertProduction.Severity = Severity.Info;
                itemProd.Color = Color.Info;
            }
            else
            {
                MudAlertProduction.Severity = Severity.Error;
               
                itemProd.Color = Color.Error;
            }

            #endregion
            #region Testing
            if (TestingCompletionCurrentDate <= project.TestingCompletion & project.ProjectProgress == 4)
            {
                MudAlertTesting.Severity = Severity.Success;
                itemTest.Color = Color.Success;
            }
            else if (TestingCompletionCurrentDate > project.TestingCompletion & project.ProjectProgress == 4)
            {
                MudAlertTesting.Severity = Severity.Success;
                itemTest.Color = Color.Success;
            }
            else if (TestingCompletionCurrentDate <= project.TestingCompletion && project.ProjectProgress == 3)
            {
                MudAlertTesting.Severity = Severity.Info;
                itemTest.Color = Color.Info;
            }
            else if (TestingCompletionCurrentDate <= project.TestingCompletion && project.ProjectProgress == 2 || project.ProjectProgress == 1)
            {
                MudAlertTesting.Severity = Severity.Normal;
                itemTest.Color = Color.Default;
            }
            else
            {
                MudAlertTesting.Severity = Severity.Error;
               
                itemTest.Color = Color.Error;
            }

            #endregion
            #region Release
            if (ReleaseDate <= project.ExpectedReleaseDate & project.ProjectProgress == 4 && project.Status == 2)
            {
                MudAlertRelease.Severity = Severity.Success;
                itemRelease.Color = Color.Success;
            }
            else if (ReleaseDate > project.ExpectedReleaseDate & project.ProjectProgress == 4 && project.Status == 2)
            {
                MudAlertRelease.Severity = Severity.Success;
                itemRelease.Color = Color.Success;
            }
         
            else if (ReleaseDate <= project.ExpectedReleaseDate && project.ProjectProgress == 2 || project.ProjectProgress == 1 || project.ProjectProgress == 3)
            {
                MudAlertRelease.Severity = Severity.Normal;
                itemRelease.Color = Color.Default;
            }
            else
            {
                MudAlertRelease.Severity = Severity.Error;
               
                itemRelease.Color = Color.Error;
            }

            #endregion
          

        }
        #endregion

        protected Func<Project, string> converter = p => p.ProjectName;
    }
}