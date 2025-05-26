namespace AppTaller.DAL
{
public class OrdenServicio : Base
    {
        public string VehiculoMatricula { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal Total { get; set; }

        public OrdenServicio(string codigo, string vehiculoMatricula, string descripcion, DateTime fechaIngreso)
        {
            Id = codigo;
            VehiculoMatricula = vehiculoMatricula;
            Descripcion = descripcion;
            FechaIngreso = fechaIngreso;

            Total = 0;
        }

        public override string ToString()
        {
            return $"Codigo: {Id}, Vehiculo: {VehiculoMatricula}, Servicio a realizar: {Descripcion}, Fecha: {FechaIngreso.ToShortDateString()}, Total: {Total:C}";
        }
    }
}
