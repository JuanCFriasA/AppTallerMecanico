using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 

namespace AppTaller.DAL
{
    public abstract class Base
    {
        public string Id { get; set; }
    }

    public static class GestionArchivos
    {
        private static string rutaClientes = "Clientes_Registrados.txt";
        private static string rutaVehiculos = "Vehiculos_Registrados.txt";
        private static string rutaOrdenes = "Ordenes_Registradas.txt";

        public static void GuardarClientes(List<Cliente> clientes)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(rutaClientes))
                {
                    sw.WriteLine($"ID;Nombre;Telefono");
                    foreach (var cliente in clientes)
                    {
                        sw.WriteLine($"{cliente.Id};{cliente.Nombre};{cliente.Telefono}");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al guardar clientes: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un error al guardar clientes: {ex.Message}");
            }
        }

        public static List<Cliente> CargarClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                if (File.Exists(rutaClientes))
                {
                    string[] lineas = File.ReadAllLines(rutaClientes).Skip(1).ToArray();
                    foreach (var linea in lineas)
                    {
                        if (string.IsNullOrWhiteSpace(linea)) continue;

                        var datos = linea.Split(';');

                        if (datos.Length == 3)
                        {
                            clientes.Add(new Cliente(datos[0].Trim(), datos[1].Trim(), datos[2].Trim()));
                        }
                        else
                        {
                            Console.WriteLine($"Linea de cliente con formato incorrecto, saltando: {linea}");
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al cargar clientes: {ex.Message}. ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un error al cargar clientes: {ex.Message}");
            }
            return clientes;
        }

        public static void GuardarVehiculos(List<Vehiculo> vehiculos)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(rutaVehiculos))
                {
                    sw.WriteLine($"Matricula;Marca;Modelo;Año;ClienteID");
                    foreach (var vehiculo in vehiculos)
                    {
                        sw.WriteLine($"{vehiculo.Id};{vehiculo.Marca};{vehiculo.Modelo};{vehiculo.Año};{vehiculo.ClienteId}");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al guardar vehiculos: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un error al guardar vehículos: {ex.Message}");
            }
        }

        public static List<Vehiculo> CargarVehiculos()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            try
            {
                if (File.Exists(rutaVehiculos))
                {
                    string[] lineas = File.ReadAllLines(rutaVehiculos).Skip(1).ToArray();
                    foreach (var linea in lineas)
                    {
                        if (string.IsNullOrWhiteSpace(linea)) continue;

                        var datos = linea.Split(';');
                        if (datos.Length == 5)
                        {
                            if (int.TryParse(datos[3].Trim(), out int año))
                            {
                                vehiculos.Add(new Vehiculo(datos[0].Trim(), datos[1].Trim(), datos[2].Trim(), año, datos[4].Trim()));
                            }
                            else
                            {
                                Console.WriteLine($"Año invalido en linea de vehiculo, omitiendo: {linea}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Linea de vehiculo con formato incorrecto, omitiendo: {linea}");
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al cargar vehiculos: {ex.Message}.");
            }
            catch (FormatException ex) 
            {
                Console.WriteLine($"Error de formato al cargar vehiculos: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un error al cargar vehículos: {ex.Message}");
            }
            return vehiculos;
        }

        public static void GuardarOrdenes(List<OrdenServicio> ordenes)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(rutaOrdenes))
                {
                    sw.WriteLine($"ID;VehiculoMatricula;Descripcion;FechaIngreso;Total");
                    foreach (var orden in ordenes)
                    {
                        sw.WriteLine($"{orden.Id};{orden.VehiculoMatricula};{orden.Descripcion};{orden.FechaIngreso.ToString("yyyy-MM-dd HH:mm:ss")};{orden.Total}");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al guardar ordenes: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un error al guardar ordenes: {ex.Message}");
            }
        }

        public static List<OrdenServicio> CargarOrdenes()
        {
            List<OrdenServicio> ordenes = new List<OrdenServicio>();
            try
            {
                if (File.Exists(rutaOrdenes))
                {
                    string[] lineas = File.ReadAllLines(rutaOrdenes).Skip(1).ToArray();
                    foreach (var linea in lineas)
                    {
                        if (string.IsNullOrWhiteSpace(linea)) continue;

                        var datos = linea.Split(';');
                        if (datos.Length == 5)
                        {
                            if (DateTime.TryParse(datos[3].Trim(), out DateTime fechaIngreso) &&
                                decimal.TryParse(datos[4].Trim(), out decimal totalValue))
                            {
                                var orden = new OrdenServicio(datos[0].Trim(), datos[1].Trim(), datos[2].Trim(), fechaIngreso);
                                orden.Total = totalValue;
                                ordenes.Add(orden);
                            }
                            else
                            {
                                Console.WriteLine($"Error de formato en fecha o total de orden, omitiendo: {linea}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Linea de orden con formato incorrecto, omitiendo: {linea}");
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al cargar ordenes: {ex.Message}.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error de formato al cargar ordenes: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrio un error al cargar órdenes: {ex.Message}");
            }
            return ordenes;
        }
    }
}
