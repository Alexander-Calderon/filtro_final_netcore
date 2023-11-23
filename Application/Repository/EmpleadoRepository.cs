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


        
        public async Task<IEnumerable<object>> C9GetEmpleadosSinClientes()
        {
            return await _context.Empleados
                .Where(e => !e.Clientes.Any())
                .Select(e => new 
                { 
                    NombreEmpleado = e.Nombre, 
                    ApellidoEmpleado = e.Apellido1,
                    NombreJefe = e.CodigoJefeNavigation.Nombre,
                    ApellidoJefe = e.CodigoJefeNavigation.Apellido1
                })
                .ToListAsync();
        }







    }
}