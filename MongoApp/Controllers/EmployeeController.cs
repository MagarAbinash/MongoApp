﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoApp.Services;
using MongoApp.Models;

namespace MongoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get() =>
            _employeeService.Get();

        [HttpGet("{id:length(24)}", Name = "GetEmployee")]
        public ActionResult<Employee> Get(string id)
        {
            var employee = _employeeService.Get(id);

            if(employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public ActionResult<Employee> Create(Employee employee)
        {
            _employeeService.Create(employee);
            return CreatedAtRoute("GetEmployee", new { id = employee.Id.ToString() }, employee);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Employee employeeIn)
        {
            var employee = _employeeService.Get(id);
            if(employee == null)
            {
                return NotFound();
            }
            employeeIn.Id = id;
            _employeeService.Update(id, employeeIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var employee = _employeeService.Get(id);

            if (employee == null)
            {
                return NotFound();
            }
            _employeeService.Remove(employee.Id);
            return NoContent();
        }
    }
}
