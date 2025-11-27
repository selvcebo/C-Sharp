using System;
using System.Collections.Generic;

namespace Sistema_escolar.Models;

public partial class Matricula
{
    public int MatriculaId { get; set; }

    public int EstudianteId { get; set; }

    public int CursoId { get; set; }

    public DateTime? FechaMatricula { get; set; }

    public string Semestre { get; set; } = null!;

    public string? Estado { get; set; }

    public decimal? NotaFinal { get; set; }

    public virtual Curso Curso { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}
