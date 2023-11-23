using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository
{
    public class PagoRepository : GenericRepository<Pago>, IPago
    {
        private readonly APIContext _context;
        public PagoRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        // Consultas:









    }
}