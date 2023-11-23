using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository
{
    public class OficinaRepository : GenericRepository<Oficina>, IOficina
    {
        private readonly APIContext _context;
        public OficinaRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        // Consultas:

        public async Task<IEnumerable<object>> C3GetOficinasSinVentasFrutales()
        {
            var oficinasConVentasFrutales = _context.Empleados
                .Where(e => e.Clientes
                    .Any(c => c.Pedidos
                        .Any(p => p.DetallePedidos
                            .Any(dp => dp.CodigoProductoNavigation.Gama == "Frutales"))))
                .Select(e => e.CodigoOficinaNavigation)
                .Distinct();

            return await _context.Oficinas
                .Where(o => !oficinasConVentasFrutales.Contains(o))
                .Select(o => new 
                {
                    o.CodigoOficina,
                    o.Ciudad,
                    o.Pais,
                    o.Region
                })
                .ToListAsync();
        }







    }
}