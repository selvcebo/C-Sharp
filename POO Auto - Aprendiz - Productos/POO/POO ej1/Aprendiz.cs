using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_ej1
{
    internal class Aprendiz
    {
        public string _nombre { get; set; }
        public int _edad { get; set; }
        public string _direccion { get; set; }

        public Aprendiz(string nombre, int edad, string direccion)
        {
            _nombre = nombre;
            _edad = edad;
            _direccion = direccion;
        }
        public void MayorEdad()
        {
            if (_edad >= 18)
            {
                Console.WriteLine($"{_nombre} es mayor de edad.");
            }
            else
            {
                Console.WriteLine($"{_nombre} es menor de edad.");
            }
        }
        
    }



}
