using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;


namespace Domain.Interfaces;

public interface IProducto : IGenericRepository<Producto>
{
    Task<IEnumerable<object>> C4GetTop20ProductosMasVendidos();
    Task<IEnumerable<object>> C5GetProductosFacturadosMasDe3000();
    Task<object> C6GetProductoMasVendido();
    Task<IEnumerable<object>> C7GetClientesYPedidos();
    
}


