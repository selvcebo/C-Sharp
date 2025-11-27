using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mascota mascota1 = new Mascota("Michi", 6, "Gato", 5D);
            Mascota mascota2 = new Mascota("Rocco", 5, "Perro", 3D);

            mascota1.informacionMascota();
            mascota2.informacionMascota();

            mascota1.calcularEdad();
            mascota2.calcularEdad();
        }
    }
}
