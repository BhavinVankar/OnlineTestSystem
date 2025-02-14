using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Models.RequestModel
{
    public class AssessmentResultModel
    {
        public Guid  Id { get; set; }
        public Guid AssignmentId { get; set; }
        public decimal Score { get; set; }
        public int Status { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
