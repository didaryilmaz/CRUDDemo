using CRUDDemo.Data;
using CRUDDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeeController(AppDbContext context)
    {
        _context = context;
    }

    // Tüm Employee getir
    [HttpGet]
    public IActionResult Get()
    {
        var employees = _context.Employees.ToList(); // Veritabanından veri al
        return Ok(employees); // Veriyi döndür
    }


    // ID'ye göre Employee getir 
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
            return NotFound(); 

        return Ok(employee);
    }

    // Yeni Employee ekle 
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Employee employee)
    {
        if (employee == null)
            return BadRequest(); 

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = employee.EmployeeId }, employee);
    }

    // Employee güncelle 
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Employee updatedEmployee)
    {
        if (id != updatedEmployee.EmployeeId)
            return BadRequest(); 

        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
            return NotFound(); 

        employee.FirstName = updatedEmployee.FirstName;
        employee.LastName = updatedEmployee.LastName;
        employee.Email = updatedEmployee.Email;
        employee.Salary = updatedEmployee.Salary;

        await _context.SaveChangesAsync();

        return NoContent(); 
    }

    // Employee sil 
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
            return NotFound(); 

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return NoContent(); 
    }
}
