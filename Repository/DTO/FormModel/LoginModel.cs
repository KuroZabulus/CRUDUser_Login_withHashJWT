using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO.FormModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please input a username")]
        ///<example>BlackDevil</example>
        public string Username { get; set; }
        [Required(ErrorMessage = "Please input a password")]
        ///<example>@UndeadBeing7834</example>
        public string Password { get; set; }
    }
}
