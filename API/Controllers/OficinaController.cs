using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

public class OficinaController : BaseApiController
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public OficinaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]        
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<OficinaDto>>> GetAll()
    {
        var oficinas = await _unitOfWork.Oficinas.GetAll();
        return _mapper.Map<List<OficinaDto>>(oficinas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OficinaDto>> GetById(int id)
    {
        var dato = await _unitOfWork.Oficinas.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        return _mapper.Map<OficinaDto>(dato);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OficinaDto>> GuardarCurso(OficinaDto param)
    {
        var dato = _mapper.Map<Oficina>(param);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Oficinas.Add(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OficinaDto>> Actualizar(OficinaDto param)
    {
        var dato = await _unitOfWork.Oficinas.GetById(param.CodigoOficina);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Oficinas.Update(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OficinaDto>> Borrar(int id)
    {
        var dato = await _unitOfWork.Oficinas.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Oficinas.Remove(dato);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<OficinaDto>(dato);
    }



    // CONSULTAS

    [HttpGet("C3GetOficinasSinVentasFrutales")]
    public async Task<ActionResult<IEnumerable<object>>> C3GetOficinasSinVentasFrutales()
    {
        var oficinas = await _unitOfWork.Oficinas.C3GetOficinasSinVentasFrutales();
        return Ok(oficinas);
    }


}
