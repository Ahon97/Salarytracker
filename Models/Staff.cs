using System;
using System.Collections.Generic;

namespace Salary_tracker.Models
{
    public partial class Staff
    {
        public int StaffId { get; set; }
        public int TransactionId { get; set; }
        public string EmpName { get; set; }
        public int? Age { get; set; }
        public string Dept { get; set; }

        public virtual Salary Transaction { get; set; }
    }
}
