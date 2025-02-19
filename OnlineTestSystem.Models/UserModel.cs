﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }  
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string EmailAddress { get; set; }  
        public string Password{ get; set; }  
        public string ConfirmPassword { get; set; }  
        public string Role { get; set; }  
        public DateTime CreatedOn { get; set; }  
        public DateTime UpdatedDate { get; set; }  
        public bool IsDeleted { get; set; }  
        public bool IsActive { get; set; }
        public string? AddedBy { get; set; }
    }
    public class UpdateUserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
