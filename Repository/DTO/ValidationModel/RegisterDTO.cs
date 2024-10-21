using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO.ValidationModel
{
    public class RegisterDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\sÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẮẰẲẴẶắằẳẵặƯứừửữự]+$", ErrorMessage = "UserName must not contain special characters.")]
        [MinLength(5, ErrorMessage = "User name must be at least 5 characters long.")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password must be at least 8 characters long", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[a-z])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one number, and one character")]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must be between 0 and 10 digits long.")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        //[RegularExpression(@"^[\w-\.]+@(gmail\.com|fpt\.edu\.vn)$", ErrorMessage = "Email must be a valid Gmail or FPT email address.")]
        public string Email { get; set; }

        [MinLength(10, ErrorMessage = "Full name must be at least 10 characters long.")]
        [MaxLength(200, ErrorMessage = "Full name max 200 characters long.")]
        [RegularExpression(@"^[^!@#$%^&*()_+=\[{\]};:<>|./?,-]*$", ErrorMessage = "Full name must not contain special characters.")]
        public string Fullname { get; set; }

        public string Status = "inactive".ToLower();
    }
}
