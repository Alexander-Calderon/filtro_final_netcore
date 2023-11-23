using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository
{
    public class ProductoRepository : GenericRepository<Producto>, IProducto
    {
        private readonly APIContext _context;
        public ProductoRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        // Consultas:

        public async Task<IEnumerable<object>> C4GetTop20ProductosMasVendidos()
        {
            return await _context.DetallePedidos
                .GroupBy(dp => new { dp.CodigoProducto, dp.CodigoProductoNavigation.Nombre })
                .Select(g => new 
                {
                    CodigoProducto = g.Key.CodigoProducto,
                    NombreProducto = g.Key.Nombre,
                    UnidadesVendidas = g.Sum(dp => dp.Cantidad)
                })
                .OrderByDescending(x => x.UnidadesVendidas)
                .Take(20)
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> C5GetProductosFacturadosMasDe3000()
        {
            return await _context.DetallePedidos
                .GroupBy(dp => new { dp.CodigoProducto, dp.CodigoProductoNavigation.Nombre })
                .Select(g => new 
                {
                    NombreProducto = g.Key.Nombre,
                    UnidadesVendidas = g.Sum(dp => dp.Cantidad),
                    TotalFacturado = g.Sum(dp => dp.Cantidad * dp.PrecioUnidad),
                })
                .Where(x => x.TotalFacturado > 3000)
                .Select(x => new 
                {
                    x.NombreProducto,
                    x.UnidadesVendidas,
                    x.TotalFacturado,
                    TotalFacturadoConIVA = x.TotalFacturado * 1.21m
                })
                .ToListAsync();
        }

        public async Task<object> C6GetProductoMasVendido()
        {
            return await _context.DetallePedidos
                .GroupBy(dp => new { dp.CodigoProducto, dp.CodigoProductoNavigation.Nombre })
                .Select(g => new 
                {
                    NombreProducto = g.Key.Nombre,
                    UnidadesVendidas = g.Sum(dp => dp.Cantidad)
                })
                .OrderByDescending(x => x.UnidadesVendidas)
                .FirstOrDefaultAsync();
        }

        
        public async Task<IEnumerable<object>> C10GetProductosNoPedidos()
        {
            return await _context.Productos
                .Where(p => !p.DetallePedidos.Any())
                .Join(_context.GamaProductos,
                    p => p.Gama,
                    g => g.Gama,
                    (p, g) => new 
                    { 
                        NombreProducto = p.Nombre, 
                        Descripcion = p.Descripcion,
                        Imagen = g.Imagen
                    })
                .ToListAsync();
        }

        

        








    }
}