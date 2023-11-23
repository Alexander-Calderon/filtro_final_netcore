# Consultas a la base de datos

## Enunciado: 
Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos que no han sido entregados a tiempo.

## Endpoint: 
GET `/Api/Pedido/C1GetPedidosNoEntregados`

## Código: 
```csharp
public async Task<IEnumerable<object>> C1GetPedidosNoEntregados()
{
    return await _context.Pedidos
        .Where(x => x.FechaEntrega > x.FechaEsperada)
        .Select(p => new 
        {
            p.CodigoPedido,
            p.CodigoCliente,
            p.FechaEsperada,
            p.FechaEntrega 
        })
        .ToListAsync();
}
```

## Explicación: 
Se usa la entidad de pedidos, se aplica un filtro Where para seleccionar los pedidos donde la fecha de entrega es mayor que la fecha esperada, luego se proyectan los campos solicitados a mostrar.


## Enunciado: 
Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.

## Endpoint: 
GET `/Api/Cliente/C2GetClientesSinPagos`

## Código: 
```csharp
public async Task<IEnumerable<object>> C2GetClientesSinPagos()
{
    return await _context.Clientes
        .Where(c => !c.Pagos.Any())
        .Select(c => new 
        {
            NombreCliente = c.NombreCliente,
            NombreRepresentante = c.CodigoEmpleadoRepVentasNavigation.Nombre,
            CiudadOficina = c.CodigoEmpleadoRepVentasNavigation.CodigoOficinaNavigation.Ciudad
        })
        .ToListAsync();
}
```

## Explicación: 
Se usa .Where(c => !c.Pagos.Any()) para filtrar los clientes que no tienen ningún pago registrado y se proyecta los campos que piden.



## Enunciado: 
Devuelve las oficinas donde no trabajan ninguno de los empleados que hayan sido los representantes de ventas de algún cliente que haya realizado la compra de algún producto de la gama Frutales.

## Endpoint: 
GET `/Api/Oficina/C3GetOficinasSinVentasFrutales`

## Código: 
```csharp
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
```

## Explicación: 
El where filtra los empleados que tienen al menos un cliente con al menos un pedido que contiene al menos un producto de la gama "Frutales", se selecciona la oficina y se le aplica distinc para evitar repetidas



## Enunciado: 
Devuelve un listado de los 20 productos más vendidos y el número total de unidades que se han vendido de cada uno. El listado deberá estar ordenado por el número total de unidades vendidas.

## Endpoint: 
GET `/Api/Producto/C4GetTop20ProductosMasVendidos`

## Código: 
```csharp
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
```

## Explicación: 
Con GroupBy se agrupan los detalles de los pedidos por código y el nombre del producto, se ordenan por ventas y luego take que es como un limit, toma las primeras 20.


## Enunciado: 
Lista las ventas totales de los productos que hayan facturado más de 3000 euros. Se mostrará el nombre, unidades vendidas, total facturado y total facturado con impuestos (21% IVA).

## Endpoint: 
GET `/Api/Producto/C5GetProductosFacturadosMasDe3000`

## Código: 
```csharp
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
```

## Explicación: 
Se agrupan los detalles de los pedidos por código y el nombre del producto, se sacan los campos y para cada grupo selecciona el nombre, la suma de las cantidades vendidas y el total facturado, condicionando a mas de 3000 euros, de esa salida se mapea los mismos campos pero se añade el otro con iva 1.21decimal y sale.


## Enunciado: 
Devuelve el nombre del producto del que se han vendido más unidades. (Tenga en cuenta que tendrá que calcular cuál es el número total de unidades que se han vendido de cada producto a partir de los datos de la tabla detalle_pedido)

## Endpoint: 
GET `/Api/Producto/C6GetProductoMasVendido`

## Código: 
```csharp
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
```

## Explicación: 
Se agrupan los detalles de pedidos por el código y el nombre del producto, luego a cada grupo que es 1 tipo de producto se le saca la sumatoria de ventas y se ordena por el mayor.

## Enunciado: 
Devuelve el listado de clientes indicando el nombre del cliente y cuántos pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no han realizado ningún pedido.

## Endpoint: 
GET `/Api/Cliente/C7GetClientesYPedidos`

## Código: 
```csharp
public async Task<IEnumerable<object>> C7GetClientesYPedidos()
{
    return await _context.Clientes
        .Select(c => new 
        {
            NombreCliente = c.NombreCliente,
            NumeroPedidos = c.Pedidos.Count
        })
        .ToListAsync();
}
```

## Explicación: 
Para cada cliente se crea una proyección para sacar el nombre del cliente y para cada cliente se cuentan los pedidos asociados.





## Enunciado: 
Devuelve el listado de clientes donde aparezca el nombre del cliente, el nombre y primer apellido de su representante de ventas y la ciudad donde está su oficina.

## Endpoint: 
GET `/Api/Cliente/C8GetClientesRepresentantesYCiudad`

## Código: 
```csharp
public async Task<IEnumerable<object>> C8GetClientesRepresentantesYCiudad()
{
    return await _context.Clientes
        .Select(c => new 
        { 
            NombreCliente = c.NombreCliente, 
            NombreRepresentante = c.CodigoEmpleadoRepVentasNavigation.Nombre,
            ApellidoRepresentante = c.CodigoEmpleadoRepVentasNavigation.Apellido1,
            CiudadOficina = c.CodigoEmpleadoRepVentasNavigation.CodigoOficinaNavigation.Ciudad
        })
        .ToListAsync();
}
```

## Explicación: 
Se selecciona el nombre del cliente, el nombre y primer apellido de su representante de ventas, y la ciudad donde está ubicada la oficina del representante a partir de las relaciones de navegabilidad.





## Enunciado: 
Devuelve un listado con los datos de los empleados que no tienen clientes asociados y el nombre de su jefe asociado.

## Endpoint: 
GET `/Api/Empleado/C9GetEmpleadosSinClientes`

## Código: 
```csharp
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
```

## Explicación: 
se seleccionan los empleados sin clientes asociados con e => !e.Clientes.Any() y luego al resultado se mapean los campos relacionados que se quieren sacar.


## Enunciado: 
Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto.

## Endpoint: 
GET `/Api/Producto/C10GetProductosNoPedidos`

## Código: 
```csharp
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
```

## Explicación: 
Se seleccionan los productos y se les aplica condicion para que no estén en la intermedia que representa que tiene pedidos, se hace join con tabla Gama para sacar el campo de imagen para los resultados y se mapean los campos requeridos para la salida.
