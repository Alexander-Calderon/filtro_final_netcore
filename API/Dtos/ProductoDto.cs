﻿using System;
using System.Collections.Generic;

namespace API.Dtos;

public class ProductoDto
{
    public string CodigoProducto { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Gama { get; set; } = null!;

    public string Dimensiones { get; set; }

    public string Proveedor { get; set; }

    public string Descripcion { get; set; }

    public short CantidadEnStock { get; set; }

    public decimal PrecioVenta { get; set; }

    public decimal PrecioProveedor { get; set; }
}
