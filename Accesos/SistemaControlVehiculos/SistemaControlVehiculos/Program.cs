using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaControlVehiculos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Crear un vehículo genérico
                Vehiculo veh1 = new Vehiculo("Toyota", "Corolla", 12000);
                veh1.MostrarInformacion();
                Console.WriteLine($"Costo mantenimiento estimado: {veh1.ObtenerCostoMantenimiento():C2}\n");

                // Crear un camión
                Camion cam1 = new Camion("Volvo", "FH16", 50000, 20);
                cam1.MostrarInformacion();
                Console.WriteLine($"Costo mantenimiento estimado: {cam1.ObtenerCostoMantenimiento():C2}\n");

                // Simular que el camión viaja y registra nuevo kilometraje
                cam1.RegistrarNuevoKilometraje(55000);
                Console.WriteLine("Después de actualizar kilometraje:");
                cam1.MostrarInformacion();
                Console.WriteLine($"Costo mantenimiento estimado actualizado: {cam1.ObtenerCostoMantenimiento():C2}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Presione Enter para salir...");
            Console.ReadLine();
        }
    }
}
