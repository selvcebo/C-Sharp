using System;
using System.Collections.Generic;

namespace Sistema_escolar.Models;

public partial class Curso
{
    public int CursoId { get; set; }

    public string CodigoCurso { get; set; } = null!;

    public string NombreCurso { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int Creditos { get; set; }

    public int HorasSemanales { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
