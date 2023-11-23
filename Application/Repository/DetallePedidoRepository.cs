using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository
{
    public class DetallePedidoRepository : GenericRepository<DetallePedido>, IDetallePedido
    {
        private readonly APIContext _context;
        public DetallePedidoRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        // Consultas:









    }
}