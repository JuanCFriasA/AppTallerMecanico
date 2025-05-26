namespace AppTaller.DAL
{
public class Vehiculo : Base
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public string ClienteId { get; set; }

        public Vehiculo(string matricula, string marca, string modelo, int año, string clienteId)
        {
            Id = matricula;
            Marca = marca;
            Modelo = modelo;
            Año = año;
            ClienteId = clienteId;
        }

        public override string ToString()
        {
            return $"Matricula: {Id}, Marca: {Marca}, Modelo: {Modelo}, Año: {Año}, Cliente ID: {ClienteId}";
        }
    }
}
