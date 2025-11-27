using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital
{
    internal class Libro
    {
        // Atributos privados
        private string titulo;
        private string autor;
        private int numeroPaginas;

        // Constructor
        public Libro(string titulo, string autor, int numeroPaginas)
        {
            this.titulo = titulo;
            this.autor = autor;
            this.numeroPaginas = numeroPaginas;
        }

        // Métodos públicos: consultar información
        public void MostrarInformacion()
        {
            Console.WriteLine($"Título: {titulo}");
            Console.WriteLine($"Autor: {autor}");
            Console.WriteLine($"Número de páginas: {numeroPaginas}");
        }

        // Método privado: solo el personal interno puede modificar las páginas
        private void ModificarPaginas(int nuevasPaginas)
        {
            numeroPaginas = nuevasPaginas;
            Console.WriteLine($"El número de páginas ha sido actualizado a {numeroPaginas} (solo personal autorizado).");
        }

        // Método protegido: generar un resumen del libro
        protected string GenerarResumen()
        {
            return $"Resumen: '{titulo}' de {autor}, contiene {numeroPaginas} páginas.";
        }

        // Método público especial para simular acceso del personal autorizado
        public void ActualizarPaginasPorPersonal(int nuevasPaginas, bool esPersonal)
        {
            if (esPersonal)
            {
                ModificarPaginas(nuevasPaginas);
            }
            else
            {
                Console.WriteLine("Error: Solo el personal autorizado puede modificar la cantidad de páginas.");
            }
        }
    }
}
