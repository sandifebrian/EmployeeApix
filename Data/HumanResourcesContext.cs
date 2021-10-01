using Microsoft.EntityFrameworkCore;
using EmployeeApix.Models;
using System;
using System.Reflection;
using System.Linq;

namespace EmployeeApix.Data
{
    public class HumanResourcesContext : DbContext
    {
        public HumanResourcesContext(DbContextOptions<HumanResourcesContext> options) : base(options)
        {
        }

        public DbSet<Employee> employees{ get; set; }
    }
}
