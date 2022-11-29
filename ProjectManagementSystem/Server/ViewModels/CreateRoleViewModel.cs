using Microsoft.Build.Framework;

namespace ProjectManagementSystem.Server.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
