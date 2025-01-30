using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace OnlineTestSystem.Models.RequestModel
{
    public class AssessmentRequestModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string TestName { get; set; }

        public string Description { get; set; }
        public int PassingScore { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;

        public List<SectionModel> Sections { get; set; } = new List<SectionModel>();
    }
    public class AssessmentResponseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string TestName { get; set; }

        public string Description { get; set; }
        public int PassingScore { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;

        public List<SectionModel> Sections { get; set; } = new List<SectionModel>();
    }
}
