using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_ej1
{

    internal class Program
    {
        static void Main(string[] args)
        {
            // ------ AUTO -------
            //Auto carro1 = new Auto("Toyota", "Corolla", 2020, 4);
            //Auto carro2 = new Auto("Kia", "Sportage", 2022, 4);

            //carro1.MostrarInfo();
            //carro2.MostrarInfo();
            //carro2._anio = 2028;

            //// ----- APRENDIZ ------
            //Console.WriteLine("Ingrese el nombre del aprendiz:");
            //string nombre = Console.ReadLine();
            //Console.WriteLine("Ingrese al edad del aprendiz:");
            //int edad = int.Parse(Console.ReadLine());
            //Console.WriteLine("Ingrese la dirección del aprendiz:");
            //string direccion = Console.ReadLine();

            //Aprendiz aprendiz1 = new Aprendiz(nombre, edad, direccion);

            //aprendiz1.MayorEdad();

            //// ----- PRODUCTOS ------
            Producto.ProductoCRUD productoCRUD = new Producto.ProductoCRUD();
            bool seguir = true;


            while (seguir)
            {
                Console.WriteLine("Ingrese una opción \n 0. Salir \n 1. Agregar producto \n 2. Listar productos \n 3. Actualizar productos \n 4. Eliminar productos");
                int opc = int.Parse(Console.ReadLine());
                switch (opc)
                {
                    case 0:
                        seguir = false;
                        break;
                    case 1:
                        productoCRUD.CrearProducto();
                        break;
                    case 2:
                        productoCRUD.LeerProductos();
                        break;
                    case 3:
                        productoCRUD.ActualizarProductos();
                        break;
                    case 4:
                        productoCRUD.EliminarProducto();
                        break;
                }
            }

        }
    }
}
