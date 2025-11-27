using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_ej1
{
    internal class Auto
    {
        public string _marca { get; set; }
        public string _modelo { get; set; }
        public int _anio { get; set; }
        public int _nroLlantas { get; set; }

        public Auto(string marca, string modelo, int anio, int nroLlantas)
        {
            _marca = marca;
            _modelo = modelo;
            _anio = anio;
            _nroLlantas = nroLlantas;
        }
        public void MostrarInfo()
        {
            Console.WriteLine($"El auto es marca {_marca} {_modelo} año {_anio} con {_nroLlantas} llantas.");
        }
    }

}
