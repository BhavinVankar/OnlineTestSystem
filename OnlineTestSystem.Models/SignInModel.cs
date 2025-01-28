using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Models
{
    public class SignInModel
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage ="The EmailAddress is required!")]
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
