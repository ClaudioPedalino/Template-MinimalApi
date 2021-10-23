﻿public class GetBookResponse : IQueryResponse
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Genero { get; set; }
    public decimal Precio { get; set; }
    public uint CantidadPaginas { get; set; }
}