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
    public void AgregarProductoNull()
    {
        Producto productoNuevo = null;

        Assert.Throws<ArgumentNullException>(() => tiendaGlobal.AgregarProducto(productoNuevo));
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
    public void BuscarProductoException()
    {
        var productoBuscado = tiendaGlobal.BuscarProducto("Fideos");

        Assert.Null(productoBuscado);
    }

    [Fact]
    public void EliminarProducto()
    {
        string nombre = "Coca cola";

        int cantidadElimnados = tiendaGlobal.EliminarProducto(nombre);

        Assert.Equal(1, cantidadElimnados);
    }

    [Fact]
    public void EliminarProductoNoExistente()
    {
        string nombre = "Shampoo";

        int cantidadElimnados = tiendaGlobal.EliminarProducto(nombre);

        Assert.Equal(0, cantidadElimnados);
    }
}