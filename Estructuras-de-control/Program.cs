using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programación_Orientada_a_Objetos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int[] numeros = new int[3];
            //for (int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine($"Ingrese un numero:  { i + 1}");
            //    numeros[i] = int.Parse(Console.ReadLine());

            //}
            //for (int i = 0; i < 3; i++)
            //{
            //    Console.WriteLine($"Numero Ingresado: {i + 1}: {numeros[i]}");

            //}

            //List<int> numeros = new List<int>();

            //numeros.Add(10);
            //numeros.Add(20);
            //numeros.Add(30);

            //Console.WriteLine("Listas despues de añadir elementos.");
            //foreach (int i in numeros)
            //{
            //    Console.WriteLine(i);
            //}
            //numeros[1] = 25;
            //Console.WriteLine("Lista después de modificar el segundo elemento:");
            //foreach (int i in numeros)
            //{
            //    Console.WriteLine(i);
            //}
            //numeros.Insert(1, 15);
            //Console.WriteLine("Lista después de insertar 15 en la posición 1:");
            //foreach (int i in numeros)
            //{
            //    Console.WriteLine(i);
            //}
            //numeros.Remove(30);
            //Console.WriteLine("Lista después de remover el elemento 30: ");
            //foreach (int i in numeros)
            //{
            //    Console.WriteLine(i);
            //}

            List<string> productos = new List<string>();
            List<float> precios = new List<float>();
            bool seguir = true;
            while (seguir == true)
            {
                Console.WriteLine("Ingrese una opción (1. Añadir productos con nombre, 2. Mostrar lista de productos, 3. Actualizar un producto existente, 4. Eliminar un producto existente, 5. Salir)");
                int opc = int.Parse(Console.ReadLine());
                switch (opc)
                {
                    case 1:
                        Console.WriteLine("Ingrese el nombre del producto:");
                        string nombreP = Console.ReadLine();
                        Console.WriteLine("Ingrese el precio del producto:");
                        float precioP = float.Parse(Console.ReadLine());
                        foreach (var nombre in productos)
                        {
                            if (nombre == nombreP)
                            {
                                Console.WriteLine("El producto ya existe.");
                            }
                            else
                            {
                                if (precioP < 0)
                                {
                                    Console.WriteLine("Ingrese un precio válido.");
                                }

                                else
                                {
                                    productos.Add(nombreP);
                                    precios.Add(precioP);
                                }
                            }
                                
                        }
                        
                            

                        break;
                case 2:
                        foreach (string nombre in productos)
                        {
                            int contador = 0;
                            Console.WriteLine($"{contador}.nombre");
                        }
                        break;
                }
            }

        }
    }
}
