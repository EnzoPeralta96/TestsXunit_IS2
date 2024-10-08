namespace GestionTienda.Tests;
public class TiendaServiceTest : IClassFixture<TiendaFixture>
{
    private readonly TiendaFixture _fixture;

    public TiendaServiceTest(TiendaFixture fixture)
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

        Assert.NotNull(productoBuscado);
        Assert.Equal(productoNuevo, productoBuscado);
    }

    [Fact]
    public void BuscarProducto()
    {
        var CocaCola = new Producto("Coca cola", 1800, Categoria.Bebidas);

        var productoBuscado = _fixture.Tienda.BuscarProducto("Coca cola");

        Assert.NotNull(productoBuscado);
        Assert.Equal(CocaCola.Nombre, productoBuscado.Nombre);
        Assert.Equal(CocaCola.Precio, productoBuscado.Precio);
        Assert.Equal(CocaCola.Categoria, productoBuscado.Categoria);
    }

    [Fact]
    public void EliminarProducto()
    {
        string nombre = "Salame Paladini";
        int cantidadElimnados = _fixture.Tienda.EliminarProducto(nombre);
        Assert.Equal(1, cantidadElimnados);
    }

    [Fact]
    public void ModificarPrecio()
    {

        double precioNuevo = 1500;
        string nombreProducto = "Papa";
        _fixture.Tienda.ModificarPrecio(nombreProducto, precioNuevo);

        var productoModificado = _fixture.Tienda.BuscarProducto(nombreProducto);
        double precioActual = productoModificado.Precio;

        Assert.NotNull(productoModificado);
        Assert.Equal(precioNuevo, precioActual);
    }

    [Fact]
    public void Aplicar_descuento()
    {
        string nombre = "Leche LaSerenisima";
        int descuento = 10;
        _fixture.Tienda.Aplicar_descuento(nombre, descuento);

        double precioEsperado = 1620;
        double precioActual = _fixture.Tienda.BuscarProducto(nombre).Precio;

        Assert.Equal(precioEsperado, precioActual);
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

        var mockRepositorio = new Mock<IProductoRepository>();
        mockRepositorio.Setup(r => r.BuscarProducto("Pepsi")).Returns(mockProducto.Object);

        var tienda = new TiendaService(mockRepositorio.Object);

        // Act
        int descuento = 10;
        tienda.Aplicar_descuento("Pepsi", descuento);

        // Assert

        double precioEsperado = 1800 - (1800 * descuento) / 100;
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
        var mockRepositorio = new Mock<IProductoRepository>();

        mockRepositorio.Setup(r => r.AgregarProducto(It.IsAny<IProducto>())).
                        Callback<IProducto>(producto => productos.Add(producto));

        mockRepositorio.Setup(r => r.BuscarProducto(It.IsAny<string>()))
                        .Returns<string>(nombre => productos.Find(p => p.Nombre == nombre));


        var tienda = new TiendaService(mockRepositorio.Object);
        tienda.AgregarProducto(mockProducto.Object);

        var productoAgregado = tienda.BuscarProducto("Pepsi");

        Assert.Equal(mockProducto.Object, productoAgregado);

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

        var mockRepositorio = new Mock<IProductoRepository>();

        mockRepositorio.Setup(r => r.EliminarProducto(It.IsAny<string>())).
                        Returns<string>(nombre => productos.RemoveAll(p => p.Nombre == nombre));


        string nombre = "Yogur";
        var tienda = new TiendaService(mockRepositorio.Object);
        int cantidadEliminado = tienda.EliminarProducto(nombre);

        Assert.Equal(1, cantidadEliminado);
    }

    [Fact]
    public void Calcular_total_carrito_SinDescuento()
    {
        var carrito = new List<string>()
        {
            "Coca cola",
            "Yogur",
        };

        double totalActual = _fixture.Tienda.Calcular_total_carrito(carrito);
        double totalEsperado = 2700;

        Assert.Equal(totalEsperado, totalActual);
    }

    [Fact]
    public void Calcular_total_carrito_ConDescuento()
    {
        var carrito = new List<string>()
        {
            "Coca cola",
            "Yogur",
            "Atun"
        };

        foreach (var nombre in carrito)
        {
            _fixture.Tienda.Aplicar_descuento(nombre, 10);
        }

        double descuentoCalculado = _fixture.Tienda.Calcular_total_carrito(carrito);
        double descuentoEsperado = 3330;

        Assert.Equal(descuentoEsperado, descuentoCalculado);

    }

    [Fact]
    public void IntegracionTienda()
    {

        var carrito = new List<string>()
        {
            "Pepsi",
            "Queso",
            "Pasta de mani",
            "Manzana",
        };

        _fixture.Tienda.AgregarProducto(new Producto("Pasta de mani", 2200, Categoria.NoPerecedero));
        _fixture.Tienda.AgregarProducto(new Producto("Manzana", 1000, Categoria.Verduras));

        foreach (var nombre in carrito)
        {
            _fixture.Tienda.Aplicar_descuento(nombre, 10);
        }

        _fixture.Tienda.EliminarProducto("Manzana");

        double descuentoCalculado = _fixture.Tienda.Calcular_total_carrito(carrito);
        double descuentoEsperado = 9900;

        Assert.Equal(descuentoEsperado, descuentoCalculado);
    }
}