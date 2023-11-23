using System;
using System.Collections.Generic;

namespace API.Dtos;

public class GamaProductoDto
{
    public string Gama { get; set; } = null!;

    public string DescripcionTexto { get; set; }

    public string DescripcionHtml { get; set; }

    public string Imagen { get; set; }
}
