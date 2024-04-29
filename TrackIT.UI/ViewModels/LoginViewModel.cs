using System.ComponentModel.DataAnnotations;

namespace TrackIT.UI.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Email Formatı Yanlıştır.")]
        [Required(ErrorMessage = "Email Gereklidir.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre Gereklidir.")]
        public string Password { get; set; }

    }
}
