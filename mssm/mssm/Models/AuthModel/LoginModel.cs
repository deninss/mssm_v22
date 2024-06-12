using System.ComponentModel.DataAnnotations;

namespace mssm.Models.AuthModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Запомнить меня?")]
        public string RememberMe { get; set; }
    }
}
