using System;
using System.Collections.Generic;
using AppTaller.DAL;

namespace GestionTaller.BLL
{
    public interface IGestionTaller
    {
        void RegistrarCliente(string nombre, string telefono);
        void RegistrarVehiculo(string matricula, string marca, string modelo, int año, string clienteId);
        void CrearOrdenServicio(string codigo, string vehiculoMatricula, string descripcion, DateTime fechaIngreso);
        void GenerarFactura(string codigoOrden, decimal totalFactura);
        void MostrarClientes();
        void MostrarVehiculos();
        void MostrarOrdenes();
        Cliente BuscarCliente(string id);
        Vehiculo BuscarVehiculo(string matricula);
        OrdenServicio BuscarOrden(string codigo);
    }
}
