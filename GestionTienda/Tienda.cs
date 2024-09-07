namespace GestionTienda;

public class Tienda
{
    private List<Producto> productos;
    public Tienda()
    {
        productos = new List<Producto>();
    }
    
    public void AgregarProducto(Producto producto)
    {
        productos.Add(producto);
    }

    //Find() : Si no encuentra el objeto, retorna null
    public Producto BuscarProducto(string nombre)
    {
        return productos.Find(p => p.Nombre == nombre);
    }

    //RemoveAll retorna la cantidad de elementos removidos de la lista
    public int EliminarProducto(string nombre)
    {
        return productos.RemoveAll(p => p.Nombre == nombre);
    }


}