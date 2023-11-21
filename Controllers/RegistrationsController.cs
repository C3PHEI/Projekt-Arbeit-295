using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Backend295.Model;
using System;
using System.Linq;
using Backend295.Dto;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public RegistrationController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Get (Alle)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        var registrations = _dbContext.Registrations.ToList();
        return Ok(registrations);
    }

    /// <summary>
    /// Get by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{name}")]
    public IActionResult Get(string name)
    {
        var registration = _dbContext.Registrations.FirstOrDefault(r => r.Name == name);

        if (registration == null)
        {
            return NotFound("Registration not found");
        }

        return Ok(registration);
    }

    /// <summary>
    /// Post
    /// </summary>
    /// <param name="registrationDto"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Post([FromBody] RegistrationDto registrationDto)
    {
        if (registrationDto == null)
        {
            return BadRequest("Invalid data");
        }

        try
        {
            var registrationModel = new RegistrationUser
            {
                Name = registrationDto.Name,
                Email = registrationDto.Email,
                Phone = registrationDto.Phone,
                Priority = registrationDto.Priority,
                Service = registrationDto.Service,
                CreateDate = registrationDto.CreateDate,
                PickupDate = registrationDto.PickupDate
            };

            // Fügen Sie die Daten zur Datenbank hinzu und speichern Sie die Änderungen
            registrationModel.CreateDate = DateTime.Now;
            _dbContext.Registrations.Add(registrationModel);
            _dbContext.SaveChanges();

            return Ok("Data received successfully and saved to the database");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }


    /// <summary>
    /// Put Methode
    /// </summary>
    /// <param name="id"></param>
    /// <param name="registrationDto"></param>
    /// <returns></returns>
    [HttpPut("{name}")]
    public IActionResult Put(string name, [FromBody] RegistrationDto registrationDto)
    {
        var existingRegistration = _dbContext.Registrations.FirstOrDefault(r => r.Name == name);

        if (existingRegistration == null)
        {
            return NotFound("Registration not found");
        }

        // Aktualisiere die Eigenschaften der vorhandenen Registrierung mit den neuen Daten
        existingRegistration.Name = registrationDto.Name;
        existingRegistration.Email = registrationDto.Email;
        existingRegistration.Phone = registrationDto.Phone;
        existingRegistration.Priority = registrationDto.Priority;
        existingRegistration.Service = registrationDto.Service;
        existingRegistration.PickupDate = registrationDto.PickupDate;

        // Speichern Sie die Änderungen in der Datenbank
        _dbContext.SaveChanges();

        return Ok("Registration updated successfully");
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{name}")]
    [Authorize]
    public IActionResult Delete(string name)
    {
        var registration = _dbContext.Registrations.FirstOrDefault(r => r.Name == name);

        if (registration == null)
        {
            return NotFound("Registration not found");
        }

        // Lösche die Registrierung aus der Datenbank
        _dbContext.Registrations.Remove(registration);
        _dbContext.SaveChanges();

        return Ok("Registration deleted successfully");
    }

}
