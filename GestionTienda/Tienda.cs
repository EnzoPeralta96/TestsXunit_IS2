namespace GestionTienda;
public class Tienda
{
    private readonly IProductoRepositorio productoRepositorio;

    public Tienda(IProductoRepositorio productoRepositorio)
    {
        this.productoRepositorio = productoRepositorio;
    }

    public void AgregarProducto(IProducto producto)
    {
        try
        {
            if (producto != null)
            {
                productoRepositorio.AgregarProducto(producto);
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
    public IProducto BuscarProducto(string nombre)
    {
        try
        {
            var productoBuscado = productoRepositorio.BuscarProducto(nombre);
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

    public void ModificarPrecio(string nombre, double nuevoPrecio)
    {
        try
        {
            var producto = productoRepositorio.BuscarProducto(nombre);
            if (nuevoPrecio > 0)
            {
                producto.ModificarPrecio(nuevoPrecio);
            }
            else
            {
                throw new ArgumentException("No se puede ingresar un precio negativo");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error:{ex.Message}");
            throw;
        }
    }

    //RemoveAll retorna la cantidad de elementos removidos de la lista
    public int EliminarProducto(string nombre)
    {
        try
        {
            int cantidadEliminados = productoRepositorio.EliminarProducto(nombre);
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

    public void Aplicar_descuento(string nombre, int porcentaje)
    {
        try
        {
            var producto = productoRepositorio.BuscarProducto(nombre);
            if (porcentaje > 0)
            {
                double nuevoPrecio = producto.Precio - (producto.Precio * porcentaje) / 100;
                producto.ModificarPrecio(nuevoPrecio);
            }
            else
            {
                throw new ArgumentException("No se puede ingresar un porcentaje negativo");
            }

        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error:{ex.Message}");
            throw;
        }
    }


}