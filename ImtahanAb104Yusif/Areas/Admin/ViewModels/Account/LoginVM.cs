using System.ComponentModel.DataAnnotations;

namespace FinalExamYusif.Areas.Admin.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        [MaxLength(50, ErrorMessage = "max 25 simwol ")]
        [MinLength(5, ErrorMessage = "min 3 simwol")]
        public string UserNameorEamil { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
    }
}
