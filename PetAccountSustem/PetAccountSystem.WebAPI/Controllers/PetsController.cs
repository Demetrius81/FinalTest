using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAccountSystem.Interfaces.Repositories;
using PetAccountSystem.Models.Models;

namespace PetAccountSystem.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PetsController : ControllerBase
{
    private readonly IRepositoryAsync<Pet> _pets;
    private readonly ILogger<PetsController> _logger;

    public PetsController(IRepositoryAsync<Pet> pets, ILogger<PetsController> logger)
    {
        this._pets = pets;
        this._logger = logger;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var result = await this._pets.GetAllAsync().ConfigureAwait(true);
        this._logger.LogInformation("> > > Received from the database and passed all the entities");
        return Ok(result);
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var result = await this._pets.GetByIDAsync(id).ConfigureAwait(true);
        if (result is null)
        {
            this._logger.LogInformation("> > > Entity not found in database");
            return NotFound();
        }

        this._logger.LogInformation("> > > Received from the database and passed the entity by ID");
        return Ok(result);
    }

    [HttpPut("add")]
    public async Task<IActionResult> Add([FromBody] Pet item)
    {
        var result = await this._pets.AddAsync(item).ConfigureAwait(true);
        if (result is null)
        {
            this._logger.LogInformation("> > > Entity not added to database");
            return BadRequest();
        }

        this._logger.LogInformation("> > > New entity added to database");
        return Ok(result);
    }

    [HttpGet("count")]
    public async Task<IActionResult> Count()
    {
        var result = await this._pets.CountAsync().ConfigureAwait(true);
        this._logger.LogInformation("> > > Received from the database and passed the number of records");
        return Ok(result);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] Pet item)
    {
        var result = await this._pets.DeleteAsync(item).ConfigureAwait(true);
        if (result is null)
        {
            this._logger.LogInformation("> > > Entity not found in database");
            return NotFound();
        }

        this._logger.LogInformation("> > > Entity removed from database");
        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] Pet item)
    {
        var result = await this._pets.UpdateAsync(item).ConfigureAwait(true);
        this._logger.LogInformation("> > > Entity updated in database");
        return Ok(result);
    }
}
