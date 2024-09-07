namespace GestionTienda;
public class Tienda
{
    private List<Producto> productos;
    public Tienda()
    {
        productos = new List<Producto>
        {
            new Producto("Coca cola",1800,Categoria.Bedidas),
            new Producto("Leche LaSerenisima",1200,Categoria.Lacteos),
            new Producto("Yogur",900,Categoria.Lacteos),
            new Producto("Salame Paladini",11000,Categoria.Fiambres),
            new Producto("Papa",1000,Categoria.Verduras)
        };

    }

    public void AgregarProducto(Producto producto)
    {
        try
        {
            if (producto != null)
            {
                productos.Add(producto);
            }else
            {
               throw new ArgumentNullException(nameof(Producto),"No se puede agregar un producto null"); 
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error:{ex.Message}");
            throw;
        }
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