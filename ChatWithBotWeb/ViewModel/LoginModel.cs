using System.ComponentModel.DataAnnotations;

namespace ChatWithBotWeb_v2.ViewModel
{
    public class LoginModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
