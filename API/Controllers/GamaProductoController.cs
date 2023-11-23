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

public class GamaProductoController : BaseApiController
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public GamaProductoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]        
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<GamaProductoDto>>> GetAll()
    {
        var gamaproductos = await _unitOfWork.GamaProductos.GetAll();
        return _mapper.Map<List<GamaProductoDto>>(gamaproductos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GamaProductoDto>> GetById(int id)
    {
        var dato = await _unitOfWork.GamaProductos.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        return _mapper.Map<GamaProductoDto>(dato);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GamaProductoDto>> GuardarCurso(GamaProductoDto param)
    {
        var dato = _mapper.Map<GamaProducto>(param);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.GamaProductos.Add(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GamaProductoDto>> Actualizar(GamaProductoDto param)
    {
        var dato = await _unitOfWork.GamaProductos.GetById(param.Gama);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.GamaProductos.Update(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GamaProductoDto>> Borrar(int id)
    {
        var dato = await _unitOfWork.GamaProductos.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.GamaProductos.Remove(dato);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<GamaProductoDto>(dato);
    }



    // CONSULTAS

    


}
