namespace GestionTienda.Tests;

public class TiendaTest
{
    private Tienda tiendaGlobal;

    public TiendaTest()
    {
        tiendaGlobal = new Tienda();
    }

    [Fact]
    public void AgregarProducto()
    {
        //Given
        var productoNuevo = new Producto("Arroz", 500, Categoria.NoPercedero);
        tiendaGlobal.AgregarProducto(productoNuevo);

        var productoBuscado = tiendaGlobal.BuscarProducto("Arroz");

        Assert.Equal(productoNuevo, productoBuscado);
    }

    [Fact]
    public void BuscarProducto()
    {
        var CocaCola = new Producto("Coca cola", 1800, Categoria.Bedidas);

        var productoBuscado = tiendaGlobal.BuscarProducto("Coca cola");

        Assert.Equal(CocaCola.Nombre, productoBuscado.Nombre);
        Assert.Equal(CocaCola.Precio, productoBuscado.Precio);
        Assert.Equal(CocaCola.Categoria, productoBuscado.Categoria);
    }

    [Fact]
    public void EliminarProducto()
    {
        string nombre = "Coca cola";
        int cantidadElimnados = tiendaGlobal.EliminarProducto(nombre);
        Assert.Equal(1, cantidadElimnados);
    }

    [Fact]
    public void ModificarPrecio()
    {

        double precioNuevo = 1500;
        string nombreProducto = "Coca cola";
        tiendaGlobal.ModificarPrecio(nombreProducto, precioNuevo);

        var productoModificado = tiendaGlobal.BuscarProducto(nombreProducto);
        double precioActual = productoModificado.Precio;

        Assert.Equal(precioNuevo, precioActual);
    }


    [Fact]
    public void AgregarProducto_Exception()
    {
        Producto productoNuevo = null;
        Assert.Throws<ArgumentNullException>(() => tiendaGlobal.AgregarProducto(productoNuevo));
    }

    [Fact]
    public void BuscarProducto_Exception()
    {
        var nombreProducto = "Fideos";
        Assert.Throws<KeyNotFoundException>(() => tiendaGlobal.BuscarProducto(nombreProducto));
    }


    [Fact]
    public void EliminarProducto_Exception()
    {
        string nombreProducto = "Shampoo";
        Assert.Throws<KeyNotFoundException>(() => tiendaGlobal.EliminarProducto(nombreProducto));
    }

    [Fact]
    public void ModificarPrecio_Exception()
    {
        string nombreProducto = "Coca cola";
        double nuevoPrecio = -1500;
        Assert.Throws<ArgumentException>(() => tiendaGlobal.ModificarPrecio(nombreProducto, nuevoPrecio));
    }

}