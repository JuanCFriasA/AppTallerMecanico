namespace AppTaller.DAL
{
    public class Cliente : Base
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        public Cliente(string id, string nombre, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Telefono = telefono;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Numero Telefonico: {Telefono}";
        }
    }

}
