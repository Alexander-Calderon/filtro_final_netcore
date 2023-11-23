using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedido
    {
        private readonly APIContext _context;
        public PedidoRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        // Consultas:


        
        public async Task<IEnumerable<object>> C1GetPedidosNoEntregados()
        {
            return await _context.Pedidos.Where(x => x.FechaEntrega > x.FechaEsperada)
            .Select(p => new 
            {
                p.CodigoPedido,
                p.CodigoCliente,
                p.FechaEsperada,
                p.FechaEntrega
            })
            .ToListAsync();
        }



        
        


        






    }
}