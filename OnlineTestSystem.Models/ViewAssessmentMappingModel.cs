using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Models
{
    public class AssessmentMappingViewModel
    {
        public Guid Id { get; set; }  
        public string Name { get; set; }      
        public string EmailAddress { get; set; } 
        public string TestName { get; set; }  
        public int PassingScore { get; set; } 
        public string Description { get; set; } 
        public Guid UserId { get; set; }      
    }

}
