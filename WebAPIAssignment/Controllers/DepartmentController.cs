using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAssignment.Models;

namespace WebAPIAssignment.Controllers
{
    [Produces("application/json")]
    [Route("api/Department")]
    public class DepartmentController : Controller
    {
        private readonly DepartmentContext _departmentContext;

        public DepartmentController(DepartmentContext departmentContext)
        {
            _departmentContext = departmentContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                var departments = await _departmentContext.Departments.ToListAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDepartment(int id)
        {
            var department = await _departmentContext.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(department);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddDepartment(Department department)
        {
            _departmentContext.Departments.Add(department);
            await _departmentContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
        }
    }
}