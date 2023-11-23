using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository
{
    public class GamaProductoRepository : GenericRepository<GamaProducto>, IGamaProducto
    {
        private readonly APIContext _context;
        public GamaProductoRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        // Consultas:









    }
}