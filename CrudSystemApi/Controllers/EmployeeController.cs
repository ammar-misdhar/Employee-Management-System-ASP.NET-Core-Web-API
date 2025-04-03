using CrudSystemApi.Data;
using CrudSystemApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace CrudSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        //Select Data
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Employee>> GetEmployee()
        {
            var empoyee = _employeeDbContext.Employees.ToList();
            if (empoyee == null)
            {
                return NoContent();
            }
            return Ok(empoyee);
        }

        //Insert Data
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Employee> CreateEmployee([FromBody] Employee employee)
        {
            {            
                if (employee.Name==null || employee.Name == "" 
                    || employee.EmailAddress==null || employee.EmailAddress == "")
                {
                    return BadRequest();
                }
                var valid = _employeeDbContext.Employees.AsQueryable().Where(x => x.EmailAddress == employee.EmailAddress).ToList().Any();
                if (valid)
                {
                    return Conflict("can't enter this email");
                }

                _employeeDbContext.Employees.Add(employee);
                _employeeDbContext.SaveChanges();
                return Ok();
            }
        }

        //Update Data
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<Employee> UpdateEmployee(int id,[FromBody] Employee employee)
        {
            if(id != employee.Id ||employee.Name == null || employee.Name == ""
            || employee.EmailAddress == null || employee.EmailAddress == "" )
            {
                return BadRequest();
            } 

            var result=_employeeDbContext.Employees.Find(id);
            if (result == null)
            {
                return NoContent();
            }

            result.Name = employee.Name;
            result.MobileNo = employee.MobileNo;
            result.EmailAddress = employee.EmailAddress;

            _employeeDbContext.Employees.Update(result);
            _employeeDbContext.SaveChanges();
            return Ok();
        }

        //Delete Data
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult DeleteEmployee(int id)
        {
            var result = _employeeDbContext.Employees.Find(id);
            if (result == null)
            {
                return BadRequest();
            }

            _employeeDbContext.Employees.Remove(result);
            _employeeDbContext.SaveChanges();
            return Ok();
        }


    }
}
