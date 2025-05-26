using System;
using GestionTaller.BLL;

namespace AppTallerMecanico.UI
{
    class Program
    {
        static void Main()
        {
            IGestionTaller taller = new GestionTaller.BLL.GestionTaller();
            int opcion;

            do
            {
                MostrarMenu();
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        RegistrarCliente(taller); 
                        break;
                    case 2:
                        RegistrarVehiculo(taller);
                        break;
                    case 3:
                        CrearOrdenServicio(taller);
                        break;
                    case 4:
                        GenerarFactura(taller);
                        break;
                    case 5:
                        taller.MostrarClientes();
                        break;
                    case 6:
                        taller.MostrarVehiculos();
                        break;
                    case 7:
                        taller.MostrarOrdenes();
                        break;
                    case 8:
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opcion no valida. Intente de nuevo.");
                        break;
                }

                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();

            } while (opcion != 8);
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n===================================");
            Console.WriteLine("\nSISTEMA DE GESTION DE TALLER\n");
            Console.WriteLine("1. Registrar cliente");
            Console.WriteLine("2. Registrar vehiculo");
            Console.WriteLine("3. Crear orden de servicio");
            Console.WriteLine("4. Generar factura");
            Console.WriteLine("5. Mostrar clientes");
            Console.WriteLine("6. Mostrar vehiculos");
            Console.WriteLine("7. Mostrar ordenes guardadas");
            Console.WriteLine("8. Salir\n");
            Console.WriteLine("===================================\n");
            Console.Write("Seleccione una opcion: ");
        }

        static int LeerOpcion()
        {
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch
            {
                return -1;
            }
        }

        static void RegistrarCliente(IGestionTaller taller)
        {
            Console.Write("Ingrese nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese telefono/celular: ");
            string telefono = Console.ReadLine();

            taller.RegistrarCliente(nombre, telefono);
        }

        static void RegistrarVehiculo(IGestionTaller taller)
        {
            Console.Write("Ingrese matricula del vehiculo: ");
            string matricula = Console.ReadLine();
            Console.Write("Ingrese marca: ");
            string marca = Console.ReadLine();
            Console.Write("Ingrese modelo: ");
            string modelo = Console.ReadLine();
            Console.Write("Ingrese año de fabricacion: ");
            int año;
            while (!int.TryParse(Console.ReadLine(), out año))
            {
                Console.WriteLine("Ingrese un número valido.");
                Console.Write("Ingrese año de fabricacion: ");
            }
            Console.Write("Ingrese ID del cliente propietario: ");
            string clienteId = Console.ReadLine();

            taller.RegistrarVehiculo(matricula, marca, modelo, año, clienteId);
        }

        static void CrearOrdenServicio(IGestionTaller taller)
        {
            Console.Write("Ingrese codigo de orden: ");
            string codigo = Console.ReadLine();
            Console.Write("Ingrese matricula del vehiculo: ");
            string matricula = Console.ReadLine();
            Console.Write("Ingrese servicio a realizar al vehiculo: ");
            string descripcion = Console.ReadLine();
            Console.Write("Ingrese fecha de entrada (DD/MM/AA): ");
            DateTime fecha;
            while (!DateTime.TryParse(Console.ReadLine(), out fecha))
            {
                Console.WriteLine("Por favor, use el formato DD/MM/AA.");
                Console.Write("Ingrese fecha de entrada (DD/MM/AA): ");
            }

            taller.CrearOrdenServicio(codigo, matricula, descripcion, fecha);
        }

        static void GenerarFactura(IGestionTaller taller)
        {
            Console.Write("Ingrese codigo de orden para facturar: ");
            string codigo = Console.ReadLine();

            Console.Write("Ingrese el monto total de la factura: ");
            decimal totalFactura;
            while (!decimal.TryParse(Console.ReadLine(), out totalFactura))
            {
                Console.WriteLine("Por favor, ingrese un número valido.");
                Console.Write("Ingrese el monto total de la factura: ");
            }

            taller.GenerarFactura(codigo, totalFactura);
        }
    }
}
