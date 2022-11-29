using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ProjectManagementSystem.Client.Services;
using ProjectManagementSystem.Shared.Models;

namespace ProjectManagementSystem.Client.Pages.AppPages
{
    public partial class AddProject
    {
        #region Dates
        DateTime? ReleaseDate = DateTime.Today;
        DateTime? PreProductionDate = DateTime.Today;
        DateTime? ProductionCompletionDate = DateTime.Today;
        DateTime? EstimatedTestingCompletionDate = DateTime.Today;

        #endregion
        protected Project project = new();
        protected ProjectType projectType = new();
        protected IList<ProjectType> projectTypes = new List<ProjectType>();
        public bool HasArt { get; set; } = false;
        public bool HideImageButton = false;
        DateTime? date = DateTime.Today;
        IList<IBrowserFile> files = new List<IBrowserFile>();
        [Inject] IGenericRepositoryService _genericRepositoryService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private async void UploadFiles(InputFileChangeEventArgs e)
        {
            var format = "image/png";
            foreach (var file in e.GetMultipleFiles(10))
            {
                var resizeImage = await file.RequestImageFileAsync(format, 200, 200);
                var buffer = new byte[resizeImage.Size];
                await resizeImage.OpenReadStream().ReadAsync(buffer);
                var imagedata = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                project.ProjectImage = imagedata;

            }

            Snackbar.Add("Image Uploaded Succefully", Severity.Success);


        }
        protected override async Task OnInitializedAsync()
        {
            await GetProjectTypes();
        }

        private async Task GetProjectTypes()
        {
            var result = await _genericRepositoryService.GetAllAsync<ProjectType>("api/ProjectManagement/GetProjectTypes");
            projectTypes = result.ToList();
        }
        async Task saveProject()
        {
            try
            {
                project.ExpectedReleaseDate = (DateTime)ReleaseDate;
                project.PreProductionCompletionDate = (DateTime)PreProductionDate;
                project.ProductionCompletion = (DateTime)ProductionCompletionDate;
                project.TestingCompletion = (DateTime)EstimatedTestingCompletionDate;
                project.ProjectTypeNavigation = projectType;
                project.ProjectType = project.ProjectTypeNavigation.Id;
                project.Status = 1;
                project.StatusNavigation = null;
                project.ProjectTypeNavigation = null;
                await _genericRepositoryService.SaveAllAsync("api/ProjectManagement/SaveProject", project);
                Snackbar.Add("Saved Successfully", Severity.Success);
                NavigationManager.NavigateTo("AppPages/ProjectManagement");

            }
            catch (Exception ex)
            {
                var _ = ex.Message;
                throw;
            }

        }
    }
}