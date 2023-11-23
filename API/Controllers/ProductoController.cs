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

public class ProductoController : BaseApiController
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]        
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ProductoDto>>> GetAll()
    {
        var productos = await _unitOfWork.Productos.GetAll();
        return _mapper.Map<List<ProductoDto>>(productos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoDto>> GetById(int id)
    {
        var dato = await _unitOfWork.Productos.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        return _mapper.Map<ProductoDto>(dato);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoDto>> GuardarCurso(ProductoDto param)
    {
        var dato = _mapper.Map<Producto>(param);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Productos.Add(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoDto>> Actualizar(ProductoDto param)
    {
        var dato = await _unitOfWork.Productos.GetById(param.CodigoProducto);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Productos.Update(dato);
        await _unitOfWork.SaveAsync();

        return param;
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductoDto>> Borrar(int id)
    {
        var dato = await _unitOfWork.Productos.GetById(id);
        if (dato == null)
        {
            return BadRequest();
        }
        _unitOfWork.Productos.Remove(dato);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<ProductoDto>(dato);
    }



    // CONSULTAS

    [HttpGet("C4GetTop20ProductosMasVendidos")]
    public async Task<ActionResult<IEnumerable<object>>> GetTop20ProductosMasVendidos()
    {
        var productos = await _unitOfWork.Productos.C4GetTop20ProductosMasVendidos();
        return Ok(productos);
    }

    [HttpGet("C5GetProductosFacturadosMasDe3000")]
    public async Task<ActionResult<IEnumerable<object>>> C5GetProductosFacturadosMasDe3000()
    {
        var productos = await _unitOfWork.Productos.C5GetProductosFacturadosMasDe3000();
        return Ok(productos);
    }

    [HttpGet("C6GetProductoMasVendido")]
    public async Task<ActionResult<object>> C6GetProductoMasVendido()
    {
        var producto = await _unitOfWork.Productos.C6GetProductoMasVendido();
        return Ok(producto);
    }

    
    [HttpGet("C10GetProductosNoPedidos")]
    public async Task<ActionResult<IEnumerable<object>>> GetProductosNoPedidos()
    {
        var productos = await _unitOfWork.Productos.C10GetProductosNoPedidos();
        return Ok(productos);
    }




}
