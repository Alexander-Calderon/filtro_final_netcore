using Domain.Interfaces;
using Persistence.Data;
using Application.Repository;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private ICliente _cliente;
    private IDetallePedido _detallePedido;
    private IEmpleado _empleado;
    private IGamaProducto _gamaProducto;
    private IOficina _oficina;
    private IPago _pago;
    private IPedido _pedido;
    private IProducto _producto;


    private readonly APIContext _context;
    public UnitOfWork(APIContext context)
    {
        _context = context;
    }

    public ICliente Clientes
    {
        get
        {
            _cliente ??= new ClienteRepository(_context);
            return _cliente;
        }
    }

    public IDetallePedido DetallePedidos
    {
        get
        {
            _detallePedido ??= new DetallePedidoRepository(_context);
            return _detallePedido;
        }
    }

    public IEmpleado Empleados
    {
        get
        {
            _empleado ??= new EmpleadoRepository(_context);
            return _empleado;
        }
    }

    public IGamaProducto GamaProductos
    {
        get
        {
            _gamaProducto ??= new GamaProductoRepository(_context);
            return _gamaProducto;
        }
    }

    public IOficina Oficinas
    {
        get
        {
            _oficina ??= new OficinaRepository(_context);
            return _oficina;
        }
    }

    public IPago Pagos
    {
        get
        {
            _pago ??= new PagoRepository(_context);
            return _pago;
        }
    }

    public IPedido Pedidos
    {
        get
        {
            _pedido ??= new PedidoRepository(_context);
            return _pedido;
        }
    }

    public IProducto Productos
    {
        get
        {
            _producto ??= new ProductoRepository(_context);
            return _producto;
        }
    }


    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
}
