using System.ComponentModel.DataAnnotations;

namespace FinalExamYusif.Areas.Admin.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(25,ErrorMessage ="max 25 simwol ")]
        [MinLength(3,ErrorMessage ="min 3 simwol")]
        public string Name { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "max 25 simwol ")]
        [MinLength(3, ErrorMessage = "min 3 simwol")]
        public string Surname { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "max 25 simwol ")]
        [MinLength(3, ErrorMessage = "min 3 simwol")]
        public string UserName { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "max 25 simwol ")]
        [MinLength(5, ErrorMessage = "min 3 simwol")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
