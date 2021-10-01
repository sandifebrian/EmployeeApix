using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApix.Models
{
    public class Employee : IModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }

        private DateTime? joinDate;

        public DateTime? GetJoinDate()
        {
            return joinDate;
        }

        public void SetJoinDate(DateTime? value)
        {
            joinDate = value;
        }
    }
}
