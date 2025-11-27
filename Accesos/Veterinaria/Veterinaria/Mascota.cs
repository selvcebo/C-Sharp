using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria
{
    internal class Mascota
    {
        
        
        private string _nombre { get; set; }
        private int _edad { get; set; }
        private string _tipo {  get; set; }
        private double _peso { get; set; }

        public Mascota(string nombre, int edad, string tipo, double peso)
        {
            _nombre = nombre;
            _edad = edad;
            _tipo = tipo;
            _peso = peso;
        }

        public void informacionMascota()
        {
            Console.WriteLine($"\n Nombre: {_nombre} \n Edad: {_edad} años \n Tipo: {_tipo} \n Peso: {_peso}kg");

        }
        public void calcularEdad()
        {
            _tipo.ToLower();
            if (_tipo == "gato" | _tipo == "Gato")
            {
                int edadHumano = _edad * 6;
                Console.WriteLine($"La edad de {_nombre} en años humanos es {edadHumano} años");
            }
            if (_tipo == "perro" | _tipo == "Perro")
            {
                int edadHumano = _edad * 7;
                Console.WriteLine($"La edad de {_nombre} en años humanos es {edadHumano} años");
            }
            else
            {
                Console.WriteLine($"No es posible calcular la edad de {_nombre} en años humanos, porque es {_tipo}");
            }
        }
    }
}
