using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }  
        public string EmailAddress { get; set; } 
        public string Password { get; set; }  
        public string Role { get; set; }  
        public DateTime CreatedDate { get; set; }  
        public DateTime UpdatedDate { get; set; }  
        public bool IsDeleted { get; set; }  
        public bool IsActive { get; set; } 
    }
}
