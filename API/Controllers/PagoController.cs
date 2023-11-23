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

public class PagoController : BaseApiController
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public PagoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]        
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<PagoDto>>> GetAll()
    {
        var pagos = await _unitOfWork.Pagos.GetAll();
        return _mapper.Map<List<PagoDto>>(pagos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagoDto>> GetById(int id)
    {
        var dato = await _unitOfWork.Pagos.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        return _mapper.Map<PagoDto>(dato);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagoDto>> GuardarCurso(PagoDto param)
    {
        var dato = _mapper.Map<Pago>(param);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Pagos.Add(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagoDto>> Actualizar(PagoDto param)
    {
        var dato = await _unitOfWork.Pagos.GetById(param.IdTransaccion);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Pagos.Update(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagoDto>> Borrar(int id)
    {
        var dato = await _unitOfWork.Pagos.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Pagos.Remove(dato);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<PagoDto>(dato);
    }



    // CONSULTAS

    


}
