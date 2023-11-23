using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;


namespace Domain.Interfaces;

public interface ICliente : IGenericRepository<Cliente>
{
    Task<IEnumerable<object>> C2GetClientesSinPagos();
    Task<IEnumerable<object>> C7GetClientesYPedidos();
    Task<IEnumerable<object>> C8GetClientesRepresentantesYCiudad();

}


