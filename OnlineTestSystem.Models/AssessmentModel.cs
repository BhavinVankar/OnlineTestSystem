using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Models
{
    public class AssessmentModel
    {
        public Guid Id { get; set; }
        public string  TestName { get; set; }
        public string Description { get; set; }
        public int PassingScore { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
