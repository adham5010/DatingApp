using System.ComponentModel.DataAnnotations;

namespace DatingApp.Api.DTOs {
    public class UserRegisterModelDTO {
        [Required]
        [StringLength (8, MinimumLength = 4, ErrorMessage = "User Name Must Be At Least 4 Characters and At most 8")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}