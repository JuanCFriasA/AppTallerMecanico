using System;
using System.Collections.Generic;
using AppTaller.DAL;
using System.Linq; 

namespace GestionTaller.BLL
{
    public class GestionTaller : IGestionTaller
    {
        private List<Cliente> clientes = GestionArchivos.CargarClientes();
        private List<Vehiculo> vehiculos = GestionArchivos.CargarVehiculos();
        private List<OrdenServicio> ordenes = GestionArchivos.CargarOrdenes();

        private int _nextClienteId;

        public GestionTaller()
        {
            _nextClienteId = clientes.Any() ? clientes.Max(c =>
            {
                if (int.TryParse(c.Id, out int idNum))
                {
                    return idNum;
                }
                return 0; 
            }) + 1 : 1; 
        }

        public void RegistrarCliente(string nombre, string telefono)
        {
            string newId = _nextClienteId.ToString(); 
            clientes.Add(new Cliente(newId, nombre, telefono));
            GestionArchivos.GuardarClientes(clientes);
            _nextClienteId++; 
            Console.WriteLine($"Cliente registrado con ID: {newId}.");
        }

        public void RegistrarVehiculo(string matricula, string marca, string modelo, int año, string clienteId)
        {
            if (BuscarCliente(clienteId) == null)
            {
                Console.WriteLine("No existe cliente registrado con ese ID.");
                return;
            }

            vehiculos.Add(new Vehiculo(matricula, marca, modelo, año, clienteId));
            GestionArchivos.GuardarVehiculos(vehiculos);
            Console.WriteLine("Vehiculo registrado.");
        }

        public void CrearOrdenServicio(string codigo, string vehiculoMatricula, string descripcion, DateTime fechaIngreso)
        {
            if (BuscarVehiculo(vehiculoMatricula) == null)
            {
                Console.WriteLine("No hay vehiculo registrado con esa matricula.");
                return;
            }

            ordenes.Add(new OrdenServicio(codigo, vehiculoMatricula, descripcion, fechaIngreso));
            GestionArchivos.GuardarOrdenes(ordenes);
            Console.WriteLine("Orden de servicio creada correctamente.");
        }

        public void GenerarFactura(string codigoOrden, decimal totalFactura)
        {
            var orden = BuscarOrden(codigoOrden);
            if (orden == null)
            {
                Console.WriteLine("No existe una orden registrada con ese codigo.");
                return;
            }

            var vehiculo = BuscarVehiculo(orden.VehiculoMatricula);
            if (vehiculo == null)
            {
                Console.WriteLine("Vehiculo vinculado a la orden no encontrado.");
                return;
            }

            var cliente = BuscarCliente(vehiculo.ClienteId);
            if (cliente == null)
            {
                Console.WriteLine("Cliente propietario del vehiculo no encontrado.");
                return;
            }

            orden.Total = totalFactura;
            GestionArchivos.GuardarOrdenes(ordenes);

            Console.WriteLine("Factura generada:");
            Console.WriteLine("==================================");
            Console.WriteLine($"Cliente: {cliente.Nombre} ({cliente.Id})");
            Console.WriteLine($"Vehiculo: {vehiculo.Marca} {vehiculo.Modelo} - Matricula: {vehiculo.Id}");
            Console.WriteLine($"Servicios Prestados: {orden.Descripcion}");
            Console.WriteLine($"Total a pagar: {orden.Total:C}");
            Console.WriteLine("==================================");
        }

        public void MostrarClientes()
        {
            Console.WriteLine("Clientes registrados:");
            foreach (var cliente in clientes)
            {
                Console.WriteLine(cliente);
            }
        }

        public void MostrarVehiculos()
        {
            Console.WriteLine("Vehiculos registrados:");
            foreach (var vehiculo in vehiculos)
            {
                Console.WriteLine(vehiculo);
            }
        }

        public void MostrarOrdenes()
        {
            Console.WriteLine("Ordenes registradas:");
            foreach (var orden in ordenes)
            {
                Console.WriteLine(orden);
            }
        }

        public Cliente BuscarCliente(string id)
        {
            return clientes.Find(c => c.Id == id);
        }

        public Vehiculo BuscarVehiculo(string matricula)
        {
            return vehiculos.Find(v => v.Id == matricula);
        }

        public OrdenServicio BuscarOrden(string codigo)
        {
            return ordenes.Find(o => o.Id == codigo);
        }
    }
}
