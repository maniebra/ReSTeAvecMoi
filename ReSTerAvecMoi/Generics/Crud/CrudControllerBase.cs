using Microsoft.AspNetCore.Mvc;
using ReSTerAvecMoi.Exceptions;
using ReSTerAvecMoi.Generics.Interfaces;

namespace ReSTerAvecMoi.Generics;

public class CrudControllerBase<TKey, TEntity, TIRepository, TIService, TReadDto, TModifyDto>(TIService service)
    : ControllerBase
    where TKey : IComparable<TKey>, IEquatable<TKey>
    where TEntity : CrudEntityBase<TKey>
    where TIRepository : ICrudRepositoryBase<TKey, TEntity>
    where TIService : ICrudServiceBase<TKey, TEntity, TIRepository>
    where TReadDto : CrudReadDtoBase<TKey, TEntity>, new()
    where TModifyDto : CrudModifyDtoBase<TKey, TEntity>
{
    private readonly TIService _service = service;


    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] TModifyDto modifyDto)
    {
        var entity = modifyDto.ToEntity();
        var res = await _service.Create(entity);
        if (res.IsSuccess)
            return Ok($"new {nameof(TEntity)} was created");
        return BadRequest(res);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Get()
    {
        var res = await _service.GetAll();
        if (!res.IsSuccess)
            return StatusCode(499);
        return Ok(
            res.Value?
                .Select(m => (TReadDto)Activator.CreateInstance(typeof(TReadDto), m)!)
                .ToList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(TKey id)
    {
        var res = await _service.Get(id);
        if (res.IsSuccess)
            return Ok(res.Value);
        return res.Error switch
        {
            EntityNotFoundException<TEntity> => NotFound(id),
            _ => BadRequest()
        };
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Update(
        [FromQuery] TKey id,
        [FromBody] TModifyDto modifyDto
        )
    {
        var res = await _service.Update(modifyDto.ToEntity());
        if (!res.IsSuccess)
            return BadRequest(res.ErrorMessage);
        if (res.Value == null)
            return StatusCode(400);
        return Ok((TReadDto)Activator.CreateInstance(typeof(TReadDto), res.Value)!);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Delete([FromRoute] TKey id)
    {
        var res = await _service.Delete(id);
        if (res.IsSuccess)
            return NoContent();
        return res.Error switch
        {
            EntityNotFoundException<TEntity> => NotFound(id),
            _ => BadRequest()
        };
    }
    
}