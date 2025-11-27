using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaControlVehiculos
{
    internal class Camion : Vehiculo
    {
        // Atributo adicional
        public double CargaMaximaToneladas { get; set; }

        // Constructor
        public Camion(string marca, string modelo, double kilometrajeInicial, double cargaMaxima)
            : base(marca, modelo, kilometrajeInicial)
        {
            CargaMaximaToneladas = cargaMaxima;
        }

        // Sobrescribir (override) el cálculo interno para camiones, por ejemplo coste mayor por km
        protected override double CalcularCostoMantenimientoInterno()
        {
            // Llamamos al cálculo base
            double costoBase = base.CalcularCostoMantenimientoInterno();
            // Supongamos que el camión tiene un recargo del 50% por su mayor desgaste
            return costoBase * 1.5;
        }

        // Sobrescribir mostrar información para añadir carga máxima
        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Carga máxima: {CargaMaximaToneladas} toneladas");
        }
    }

}
