using Moq;

namespace GestionTienda.Tests;

public class TiendaTest:IClassFixture<TiendaFixture>
{
    private readonly TiendaFixture _fixture;

    public TiendaTest(TiendaFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void AgregarProducto()
    {
        //Given
        var productoNuevo = new Producto("Arroz", 500, Categoria.NoPerecedero);
        _fixture.Tienda.AgregarProducto(productoNuevo);

        var productoBuscado = _fixture.Tienda.BuscarProducto("Arroz");

        Assert.Equal(productoNuevo, productoBuscado);
    }

    [Fact]
    public void BuscarProducto()
    {
        var CocaCola = new Producto("Coca cola", 1800, Categoria.Bebidas);

        var productoBuscado = _fixture.Tienda.BuscarProducto("Coca cola");

        Assert.Equal(CocaCola.Nombre, productoBuscado.Nombre);
        Assert.Equal(CocaCola.Precio, productoBuscado.Precio);
        Assert.Equal(CocaCola.Categoria, productoBuscado.Categoria);
    }

    [Fact]
    public void EliminarProducto()
    {
        string nombre = "Coca cola";
        int cantidadElimnados = _fixture.Tienda.EliminarProducto(nombre);
        Assert.Equal(1, cantidadElimnados);
    }

    [Fact]
    public void ModificarPrecio()
    {

        double precioNuevo = 1500;
        string nombreProducto = "Arroz";
        _fixture.Tienda.ModificarPrecio(nombreProducto, precioNuevo);

        var productoModificado = _fixture.Tienda.BuscarProducto(nombreProducto);
        double precioActual = productoModificado.Precio;

        Assert.Equal(precioNuevo, precioActual);
    }


    [Fact]
    public void AgregarProducto_Exception()
    {
        Producto productoNuevo = null;
        Assert.Throws<ArgumentNullException>(() => _fixture.Tienda.AgregarProducto(productoNuevo));
    }

    [Fact]
    public void BuscarProducto_Exception()
    {
        var nombreProducto = "Fideos";
        Assert.Throws<KeyNotFoundException>(() => _fixture.Tienda.BuscarProducto(nombreProducto));
    }


    [Fact]
    public void EliminarProducto_Exception()
    {
        string nombreProducto = "Shampoo";
        Assert.Throws<KeyNotFoundException>(() => _fixture.Tienda.EliminarProducto(nombreProducto));
    }

    [Fact]
    public void ModificarPrecio_Exception()
    {
        string nombreProducto = "Coca cola";
        double nuevoPrecio = -1500;
        Assert.Throws<ArgumentException>(() => _fixture.Tienda.ModificarPrecio(nombreProducto, nuevoPrecio));
    }

    [Fact]
    public void Aplicar_descuento_UtilizandoMock()
    {
        var mockProducto = new Mock<IProducto>();
        mockProducto.Setup(p => p.Nombre).Returns("Pepsi");
        mockProducto.Setup(p => p.Precio).Returns(1800);
        mockProducto.Setup(p => p.Categoria).Returns(Categoria.Bebidas);

        mockProducto.Setup(p => p.ModificarPrecio(It.IsAny<double>()))
                       .Callback<double>(nuevoPrecio => mockProducto.Setup(p => p.Precio).Returns(nuevoPrecio));

        var mockRepositorio = new Mock<IProductoRepositorio>();
        mockRepositorio.Setup(r => r.BuscarProducto("Pepsi")).Returns(mockProducto.Object);

        var tienda = new Tienda(mockRepositorio.Object);

        // Act
        int descuento = 10;
        tienda.Aplicar_descuento("Pepsi", descuento);

        // Assert

        double precioEsperado = 1800 - (1800*descuento) / 100;
        Assert.Equal(precioEsperado, mockProducto.Object.Precio);
        mockProducto.Verify(p => p.ModificarPrecio(precioEsperado), Times.Once);
    }

    [Fact]
    public void AgregarProducto_UtilizandoMock()
    {

        var mockProducto = new Mock<IProducto>();
        mockProducto.Setup(p => p.Nombre).Returns("Pepsi");
        mockProducto.Setup(p => p.Precio).Returns(1200);
        mockProducto.Setup(p => p.Categoria).Returns(Categoria.Bebidas);

        var productos = new List<IProducto>();
        var mockRepositorio = new Mock<IProductoRepositorio>();

        mockRepositorio.Setup(r => r.AgregarProducto(It.IsAny<IProducto>())).
                        Callback<IProducto>(producto => productos.Add(producto));
        
        mockRepositorio.Setup(r => r.BuscarProducto(It.IsAny<string>()))
                        .Returns<string>(nombre => productos.Find(p => p.Nombre == nombre));
    

        var tienda = new Tienda(mockRepositorio.Object);
        tienda.AgregarProducto(mockProducto.Object);

        var productoAgregado = tienda.BuscarProducto("Pepsi");

        Assert.Equal(mockProducto.Object,productoAgregado);
    }

    [Fact]
    public void EliminarProducto_UtilizandoMock()
    {
        
        var productos = new List<IProducto>()
        {
           
            new Producto("Coca cola",1800,Categoria.Bebidas),
            new Producto("Leche LaSerenisima",1200,Categoria.Lacteos),
            new Producto("Yogur",900,Categoria.Lacteos),
        };

        var mockRepositorio = new Mock<IProductoRepositorio>();

        mockRepositorio.Setup(r => r.EliminarProducto(It.IsAny<string>())).
                        Returns<string>(nombre => productos.RemoveAll(p => p.Nombre == nombre));
    

        string nombre = "Yogur";
        var tienda = new Tienda(mockRepositorio.Object);
        int cantidadEliminado = tienda.EliminarProducto(nombre);

        Assert.Equal(1,cantidadEliminado);
    }



}