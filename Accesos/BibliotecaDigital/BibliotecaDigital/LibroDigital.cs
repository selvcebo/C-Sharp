using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital
{
    internal class LibroDigital : Libro
    {
        private double tamanoArchivoMB;

        public LibroDigital(string titulo, string autor, int numeroPaginas, double tamanoArchivoMB)
            : base(titulo, autor, numeroPaginas)
        {
            this.tamanoArchivoMB = tamanoArchivoMB;
        }

        // Método que usa el método protegido de la clase base
        public void MostrarResumenDigital()
        {
            string resumen = GenerarResumen();
            Console.WriteLine($"{resumen}\nTamaño del archivo: {tamanoArchivoMB} MB.");
        }
    }
}
