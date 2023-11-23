using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository
{
    public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleado
    {
        private readonly APIContext _context;
        public EmpleadoRepository(APIContext context) : base(context)
        {
            _context = context;
        }

        // Consultas:









    }
}