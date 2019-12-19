using System.ComponentModel.DataAnnotations;

namespace CloudBacktesting.AuthentificationService.WebAPI.Models
{
    public class AuthentificateModel
    {
        [Required]
        public string Token { get; set; }
    }
}
