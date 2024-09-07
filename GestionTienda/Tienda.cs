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
            }
            else
            {
                throw new ArgumentNullException(nameof(Producto), "No se puede agregar un producto null");
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
        try
        {
            var productoBuscado = productos.Find(p => p.Nombre == nombre);
            if (productoBuscado != null)
            {
                return productoBuscado;
            }
            else
            {
                throw new KeyNotFoundException("El produco " + nombre + " no se encontró en la tienda");
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public void  ModificarPrecio(string nombre, double nuevoPrecio)
    {
        try
        {
            var producto = BuscarProducto(nombre);
            if (nuevoPrecio > 0)
            {
                producto.ModificarPrecio(nuevoPrecio);
            }else
            {
                throw new ArgumentException("No se puede ingresar un precio negativo");
            }
        }
        catch (Exception Ex)
        {
            Console.WriteLine($"Error:{Ex.Message}");
            throw;
        }
    }

    //RemoveAll retorna la cantidad de elementos removidos de la lista
    public int EliminarProducto(string nombre)
    {
        try
        {
            int cantidadEliminados = productos.RemoveAll(p => p.Nombre == nombre);
            if (cantidadEliminados > 0)
            {
                return cantidadEliminados;
            }
            else
            {
                throw new KeyNotFoundException("El produco " + nombre + " no se encontró en la tienda");
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }


}