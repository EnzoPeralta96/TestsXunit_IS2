namespace GestionTienda;
public enum Categoria
{
    Carnes,
    Lacteos,
    Verduras,
    Fiambres,
    Bedidas,
    Limpieza,
    NoPercedero
}
public class Producto
{
    private string nombre;
    private double precio;
    private Categoria categoria;

    public Producto(string nombre, double precio, Categoria categoria)
    {
        this.nombre = nombre;
        this.precio = precio;
        this.categoria = categoria;
    }
    public string Nombre { get => nombre;  }
    public double Precio { get => precio;  }
    public Categoria Categoria { get => categoria; }
}


