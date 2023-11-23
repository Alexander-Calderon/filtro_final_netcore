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

public class DetallePedidoController : BaseApiController
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public DetallePedidoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]        
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<DetallePedidoDto>>> GetAll()
    {
        var detallepedidos = await _unitOfWork.DetallePedidos.GetAll();
        return _mapper.Map<List<DetallePedidoDto>>(detallepedidos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetallePedidoDto>> GetById(int id)
    {
        var dato = await _unitOfWork.DetallePedidos.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        return _mapper.Map<DetallePedidoDto>(dato);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetallePedidoDto>> GuardarCurso(DetallePedidoDto param)
    {
        var dato = _mapper.Map<DetallePedido>(param);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.DetallePedidos.Add(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetallePedidoDto>> Actualizar(DetallePedidoDto param)
    {
        var dato = await _unitOfWork.DetallePedidos.GetById(param.CodigoPedido);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.DetallePedidos.Update(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetallePedidoDto>> Borrar(int id)
    {
        var dato = await _unitOfWork.DetallePedidos.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.DetallePedidos.Remove(dato);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<DetallePedidoDto>(dato);
    }



    // CONSULTAS

    


}
