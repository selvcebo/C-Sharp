using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaControlVehiculos
{
    internal class Vehiculo
    {
        // Atributos privados
        private string marca;
        private string modelo;
        private double kilometraje;

        // Propiedades públicas para acceso controlado (encapsulamiento)
        public string Marca
        {
            get => marca;
            set => marca = value ?? throw new ArgumentNullException(nameof(Marca));
        }

        public string Modelo
        {
            get => modelo;
            set => modelo = value ?? throw new ArgumentNullException(nameof(Modelo));
        }

        public double Kilometraje
        {
            get => kilometraje;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException("El kilometraje no puede ser negativo.");
                kilometraje = value;
            }
        }

        // Constructor
        public Vehiculo(string marca, string modelo, double kilometrajeInicial)
        {
            Marca = marca;
            Modelo = modelo;
            Kilometraje = kilometrajeInicial;
        }

        // Método público para mostrar información
        public virtual void MostrarInformacion()
        {
            Console.WriteLine("----- Información del Vehículo -----");
            Console.WriteLine($"Marca     : {Marca}");
            Console.WriteLine($"Modelo    : {Modelo}");
            Console.WriteLine($"Kilometraje: {Kilometraje} km");
        }

        // Método protegido para calcular el costo de mantenimiento según el kilometraje
        // Sólo accesible dentro de esta clase y clases que heredan de ella
        protected virtual double CalcularCostoMantenimientoInterno()
        {
            // Ejemplo: un costo base, digamos 0.10 dólares por km
            return Kilometraje * 0.10;
        }

        // Método público que usa el cálculo interno y devuelve el costo
        public double ObtenerCostoMantenimiento()
        {
            return CalcularCostoMantenimientoInterno();
        }

        // Método para actualizar kilometraje (simula uso del vehículo)
        public void RegistrarNuevoKilometraje(double nuevoKilometraje)
        {
            if (nuevoKilometraje < Kilometraje)
                throw new ArgumentException("El nuevo kilometraje debe ser mayor o igual al anterior.");
            Kilometraje = nuevoKilometraje;
        }
    }
}
