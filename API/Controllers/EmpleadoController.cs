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

public class EmpleadoController : BaseApiController
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public EmpleadoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]        
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<EmpleadoDto>>> GetAll()
    {
        var empleados = await _unitOfWork.Empleados.GetAll();
        return _mapper.Map<List<EmpleadoDto>>(empleados);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpleadoDto>> GetById(int id)
    {
        var dato = await _unitOfWork.Empleados.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        return _mapper.Map<EmpleadoDto>(dato);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpleadoDto>> GuardarCurso(EmpleadoDto param)
    {
        var dato = _mapper.Map<Empleado>(param);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Empleados.Add(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpleadoDto>> Actualizar(EmpleadoDto param)
    {
        var dato = await _unitOfWork.Empleados.GetById(param.CodigoEmpleado);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Empleados.Update(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpleadoDto>> Borrar(int id)
    {
        var dato = await _unitOfWork.Empleados.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Empleados.Remove(dato);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<EmpleadoDto>(dato);
    }



    // CONSULTAS

    
    [HttpGet("C9GetEmpleadosSinClientes")]
    public async Task<ActionResult<IEnumerable<object>>> C9GetEmpleadosSinClientes()
    {
        var empleados = await _unitOfWork.Empleados.C9GetEmpleadosSinClientes();
        return Ok(empleados);
    }
    


}
