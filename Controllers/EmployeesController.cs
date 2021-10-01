using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeApix.Data;
using EmployeeApix.Models;

namespace EmployeeApix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : CustomControllerBase
    {
        private readonly HumanResourcesContext _context;

        public EmployeesController(HumanResourcesContext context) : base(context)
        {
            _context = context;
        }
    }
}
