namespace GestionAcademicaAPI.DTOs
{
    public class CursoResponseDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public int CantidadEstudiantes { get; set; }
    }
}