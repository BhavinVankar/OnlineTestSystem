using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Models.ResponseModel
{
    public class AssessmentHistoryModel
    {
            public string TestName { get; set; }
            public string Description { get; set; }
            public decimal Score { get; set; }
            public string Status { get; set; }
            public DateTime CompletedDate { get; set; }
            public string Name { get; set; }
            public string EmailAddress { get; set; }

    }
}
