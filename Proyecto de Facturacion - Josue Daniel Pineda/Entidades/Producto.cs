namespace Entidades
{
    public class Producto
    {
        //PROPÍEDADES
        public string Codigo { get; set; } //Llave primaria en el campo de la base de datos
        public string Descripcion { get; set; }
        public int Existencia { get; set; }
        public decimal Precio { get; set; }
        public byte[] Imagen { get; set; }


        //CONSTRUCTORES
        public Producto()
        {
        }

        public Producto(string codigo, string descripcion, int existencia, decimal precio, byte[] imagen)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            Existencia = existencia;
            Precio = precio;
            Imagen = imagen;
        }
    }
}
