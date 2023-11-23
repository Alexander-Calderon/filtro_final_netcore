using System;
using System.Collections.Generic;

namespace API.Dtos;

public class PagoDto
{
    public int CodigoCliente { get; set; }

    public string FormaPago { get; set; } = null!;

    public string IdTransaccion { get; set; } = null!;

    public DateOnly FechaPago { get; set; }

    public decimal Total { get; set; }
}
