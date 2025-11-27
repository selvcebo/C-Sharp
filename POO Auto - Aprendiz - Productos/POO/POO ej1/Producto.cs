using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_ej1
{
    internal class Producto


    {

        public string _nombre { get; set; }
        public decimal _precio { get; set; }

        public int _id { get; set; }
        public Producto(string nombre, decimal precio, int id)
        {

            _nombre = nombre;
            _precio = precio;
            _id = id;

        }
        public class ProductoCRUD
        {
            public List<Producto> productos { get; set; } = new List<Producto>();
            public int sigId = 1;

            public void CrearProducto()
            {
                Console.WriteLine("Ingrese el nombre del producto:");
                string nombre = Console.ReadLine();
                Console.WriteLine("Ingrese el precio del producto:");
                decimal precio = decimal.Parse(Console.ReadLine());

                Producto nuevoProducto = new Producto(nombre, precio, sigId++);
                productos.Add(nuevoProducto);

            }
            public void LeerProductos()
            {
                foreach (var producto in productos)
                {
                    Console.WriteLine($"ID {producto._id}\n Nombre: {producto._nombre}\n Precio: {producto._precio}");
                }
            }
            public void ActualizarProductos()
            {
                Console.WriteLine("Ingrese el ID del producto a actualizar");
                int id = int.Parse(Console.ReadLine());
                Producto producto = productos.Find(p => p._id == id);

                if (producto != null)
                {
                    Console.WriteLine("Ingrese el nuevo nombre:");
                    string nombre = Console.ReadLine();
                    Console.WriteLine("Ingrese el nuevo precio:");
                    decimal precio = decimal.Parse(Console.ReadLine());

                    producto._nombre = nombre;
                    producto._precio = precio;

                    Console.WriteLine("Producto actualizado exitosamente.");


                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }
            }
            public void EliminarProducto()
            {
                Console.WriteLine("Ingrese el ID del producto a eliminar:");
                int id = int.Parse(Console.ReadLine());
                Producto producto = productos.Find(p => p._id == id);
                if (producto != null)
                {
                    productos.Remove(producto);
                    Console.WriteLine("Producto eliminado exitosamente");

                }
                else
                {
                    Console.WriteLine("Producto no encontrado");
                }
            }

        }
    }

}
