using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeApix.Data;
using EmployeeApix.Models;
using System.Web.Http;

namespace EmployeeApix.Controllers
{
    public abstract class CustomControllerBase : ControllerBase
    {
        private readonly HumanResourcesContext _context;

        public CustomControllerBase(HumanResourcesContext context)
        {
            _context = context;
        }

        protected CustomControllerBase(string v)
        {
        }

        [HttpGet("page/{page}/{dataPerPage}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetemployeesForPagination(int page, int dataPerPage)
        {
            page = page < 1 ? 1 : page;
            dataPerPage = dataPerPage < 1 ? 10 : dataPerPage;
            var skip = ((page - 1) * dataPerPage);
            return await _context.employees.Skip(skip).Take(dataPerPage).ToListAsync();
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees([FromUri] long[] ?ids)
        {
            Console.WriteLine(String.Join(",", ids));
            if (ids.Count() < 1)
            {
                return await _context.employees.ToListAsync();
            }
            else
            {
                return await _context.employees.Where(employee => ids.Contains(employee.Id)).ToListAsync();
            }
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            var employee = await _context.employees.FindAsync(id);
            // var employee = await  _context.GetType.GetProperty(_table).GetValue(_context, null);


            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(long id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        // DELETE: api/Employees/5
        [HttpDelete("multiple-id")]
        public async Task<ActionResult<IEnumerable<Employee>>> DeleteEmployees([FromUri] long[] ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var employees = await _context.employees.Where(employee => ids.Contains(employee.Id)).ToListAsync();
            if (employees.Count() < 1)
            {
                return NotFound();
            }

            _context.employees.RemoveRange(employees);
            await _context.SaveChangesAsync();
            return employees;
        }

        // DELETE: api/Employees/5
        [HttpDelete("all-data")]
        public async Task<ActionResult<IEnumerable<Employee>>> DeleteAllEmployees()
        {
            var employees = await _context.employees.ToListAsync();
            if (employees.Count() < 1)
            {
                return NotFound();
            }

            _context.employees.RemoveRange(employees);
            await _context.SaveChangesAsync();
            return employees;
        }

        private bool EmployeeExists(long id)
        {
            return _context.employees.Any(e => e.Id == id);
        }
    }
}
