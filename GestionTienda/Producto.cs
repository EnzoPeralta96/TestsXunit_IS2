namespace GestionTienda;
public enum Categoria
{
    Carnes,
    Lacteos,
    Verduras,
    Fiambres,
    Bebidas,
    Limpieza,
    NoPercedero
}
public class Producto: IProducto
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

    public void ModificarPrecio(double precio)
    {
        this.precio = precio;
    }
}


