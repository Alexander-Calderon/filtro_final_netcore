using System;
using System.Collections.Generic;

namespace API.Dtos;

public class DetallePedidoDto
{
    public int CodigoPedido { get; set; }

    public string CodigoProducto { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal PrecioUnidad { get; set; }

    public short NumeroLinea { get; set; }
}
