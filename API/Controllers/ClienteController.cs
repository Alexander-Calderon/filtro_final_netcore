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

public class ClienteController : BaseApiController
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]        
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ClienteDto>>> GetAll()
    {
        var clientes = await _unitOfWork.Clientes.GetAll();
        return _mapper.Map<List<ClienteDto>>(clientes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDto>> GetById(int id)
    {
        var dato = await _unitOfWork.Clientes.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        return _mapper.Map<ClienteDto>(dato);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDto>> GuardarCurso(ClienteDto param)
    {
        var dato = _mapper.Map<Cliente>(param);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Clientes.Add(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDto>> Actualizar(ClienteDto param)
    {
        var dato = await _unitOfWork.Clientes.GetById(param.CodigoCliente);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Clientes.Update(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDto>> Borrar(int id)
    {
        var dato = await _unitOfWork.Clientes.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Clientes.Remove(dato);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<ClienteDto>(dato);
    }



    // CONSULTAS

    [HttpGet("C2GetClientesSinPagos")]
    public async Task<ActionResult<IEnumerable<object>>> C2GetClientesSinPagos()
    {
        var clientes = await _unitOfWork.Clientes.C2GetClientesSinPagos();
        return Ok(clientes);
    }

    


}
