namespace GestionTienda;
public class TiendaService
{
    private readonly IProductoRepository productoRepositorio;

    public TiendaService(IProductoRepository productoRepositorio)
    {
        this.productoRepositorio = productoRepositorio;
    }

    public void AgregarProducto(IProducto producto)
    {
        try
        {
            if (producto == null) throw new ArgumentNullException(nameof(Producto), "No se puede agregar un producto null");
            productoRepositorio.AgregarProducto(producto);
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

            if (productoBuscado == null) throw new KeyNotFoundException("El produco " + nombre + " no se encontró en la tienda");

            return productoBuscado;

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
            if (nuevoPrecio < 0) throw new ArgumentException("No se puede ingresar un precio negativo");

            var producto = BuscarProducto(nombre);

            producto.ModificarPrecio(nuevoPrecio);

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

            if (cantidadEliminados == 0) throw new KeyNotFoundException("El produco " + nombre + " no se encontró en la tienda");

            return cantidadEliminados;
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
            if (porcentaje < 0) throw new ArgumentException("No se puede ingresar un porcentaje negativo");
            var producto = BuscarProducto(nombre);
            double nuevoPrecio = producto.Precio - ((producto.Precio * porcentaje) / 100);
            producto.ModificarPrecio(nuevoPrecio);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error:{ex.Message}");
            throw;
        }
    }

    public double Calcular_total_carrito(List<string> carrito)
    {
        double total_carrito = 0;
        foreach (string nombre in carrito)
        {
            try
            {
                var producto = BuscarProducto(nombre);
                total_carrito += producto.Precio;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                continue;
            }
        }

        return total_carrito;
    }


}