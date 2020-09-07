using System;
using System.Collections.Generic;

namespace Salary_tracker.Models
{
    public partial class Salary
    {
        public Salary()
        {
            Staff = new HashSet<Staff>();
        }

        public int TransactionId { get; set; }
        public double Amount { get; set; }
        public string SalaryStatus { get; set; }

        public virtual ICollection<Staff> Staff { get; set; }
    }
}
