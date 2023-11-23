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

public class PedidoController : BaseApiController
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public PedidoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]        
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<PedidoDto>>> GetAll()
    {
        var pedidos = await _unitOfWork.Pedidos.GetAll();
        return _mapper.Map<List<PedidoDto>>(pedidos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoDto>> GetById(int id)
    {
        var dato = await _unitOfWork.Pedidos.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        return _mapper.Map<PedidoDto>(dato);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoDto>> GuardarCurso(PedidoDto param)
    {
        var dato = _mapper.Map<Pedido>(param);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Pedidos.Add(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoDto>> Actualizar(PedidoDto param)
    {
        var dato = await _unitOfWork.Pedidos.GetById(param.CodigoPedido);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Pedidos.Update(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PedidoDto>> Borrar(int id)
    {
        var dato = await _unitOfWork.Pedidos.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Pedidos.Remove(dato);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<PedidoDto>(dato);
    }



    // CONSULTAS
    
    [HttpGet("C1GetPedidosNoEntregados")]
    public async Task<ActionResult<IEnumerable<object>>> C1GetPedidosNoEntregados()
    {
        var pedidos = await _unitOfWork.Pedidos.C1GetPedidosNoEntregados();
        // return Ok(_mapper.Map<List<PedidoDto>>(pedidos)); // AHHHH
        return Ok(pedidos);
    }

    



}
