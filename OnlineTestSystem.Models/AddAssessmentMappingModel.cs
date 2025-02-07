using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Models
{
    public class AddAssessmentMappingModel
    {
        public Guid Id { get; set; }
        public Guid TestsId { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public Guid Assigned_At { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string[] users { get; set; }
    }
    public class ResponseAssessmentMappingModel
    {
        public Guid Id { get; set; }
        public Guid TestsId { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public Guid Assigned_At { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string[] users { get; set; }
    }
}
