using Domain.Model.Entities.Clientes;

namespace EntryPoints.GRPc.Dtos;

public class CrearClienteProto
{
    public TipoIdentificación TipoIdentificación { get; set; }
    public string NumeroIdentificación { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string CorreoElectronico { get; set; }
    public DateOnly FechaNacimiento { get; set; }
}