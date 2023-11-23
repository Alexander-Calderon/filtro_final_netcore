using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository
{
    public class ClienteRepository : GenericRepository<Cliente>, ICliente
    {
        private readonly APIContext _context;
        public ClienteRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        // Consultas:

        public async Task<IEnumerable<object>> C2GetClientesSinPagos()
        {
            return await _context.Clientes
                .Where(c => !c.Pagos.Any())
                .Select(c => new 
                {
                    NombreCliente = c.NombreCliente,
                    NombreRepresentante = c.CodigoEmpleadoRepVentasNavigation.Nombre,
                    CiudadOficina = c.CodigoEmpleadoRepVentasNavigation.CodigoOficinaNavigation.Ciudad
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<object>> C7GetClientesYPedidos()
        {
            return await _context.Clientes
                .Select(c => new 
                {
                    NombreCliente = c.NombreCliente,
                    NumeroPedidos = c.Pedidos.Count
                })
                .ToListAsync();
        }

        

        public async Task<IEnumerable<object>> C8GetClientesRepresentantesYCiudad()
        {
            return await _context.Clientes
                .Select(c => new 
                { 
                    NombreCliente = c.NombreCliente, 
                    NombreRepresentante = c.CodigoEmpleadoRepVentasNavigation.Nombre,
                    ApellidoRepresentante = c.CodigoEmpleadoRepVentasNavigation.Apellido1,
                    CiudadOficina = c.CodigoEmpleadoRepVentasNavigation.CodigoOficinaNavigation.Ciudad
                })
                .ToListAsync();
        }


    }
}