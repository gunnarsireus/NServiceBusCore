using System.ComponentModel.DataAnnotations;

namespace Shared.Models.AccountViewModels
{
	public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
