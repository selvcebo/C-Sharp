using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Libro libro1 = new Libro("Cien Años de Soledad", "Gabriel García Márquez", 471);
            LibroDigital libro2 = new LibroDigital("1984", "George Orwell", 328, 1.8);

            Console.WriteLine("=== Información de Libros ===");
            libro1.MostrarInformacion();
            Console.WriteLine();
            libro2.MostrarInformacion();

            Console.WriteLine("\n=== Resumen del Libro Digital ===");
            libro2.MostrarResumenDigital();

            Console.WriteLine("\n=== Intento de modificación de páginas ===");
            libro1.ActualizarPaginasPorPersonal(500, false); // usuario sin permiso
            libro1.ActualizarPaginasPorPersonal(500, true);  // personal autorizado

            Console.ReadLine();
        }
    }
}
