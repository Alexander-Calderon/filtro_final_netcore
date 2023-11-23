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


        // Devuelve un listado con el codigo de pedido, codigo de cliente, fecha esperada y fecha de entrega de los pedidos que no han sido entregados a tiempo
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

        // devuelve el nombre de los clientes que no hayan hecho pagos y el nombre de sus representantes de ventas junto con la ciudad de la oficina a la que pertenece el representante.

        
        


        






    }
}