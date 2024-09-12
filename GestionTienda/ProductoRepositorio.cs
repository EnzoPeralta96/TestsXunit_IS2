namespace GestionTienda;
public class ProductoRepositorio : IProductoRepositorio
{
    private List<IProducto> productos;
    public ProductoRepositorio()
    {
        productos = new List<IProducto>
        {
            new Producto("Leche LaSerenisima",1800,Categoria.Lacteos),
            new Producto("Yogur",900,Categoria.Lacteos),
            new Producto("Coca cola",1800,Categoria.Bebidas),
            new Producto("Pepsi",1800,Categoria.Bebidas),
            new Producto("Salame Paladini",11000,Categoria.Fiambres),
            new Producto("Queso",7000,Categoria.Fiambres),
            new Producto("Atun",1000,Categoria.NoPerecedero),
            new Producto("Papa",1000,Categoria.Verduras),
            new Producto("Banana",1000,Categoria.NoPerecedero)
        };
    }

    public List<IProducto> obtenerProductos()
    {
        return productos;
    }

    public void AgregarProducto(IProducto producto)
    {
        productos.Add(producto);
    }

    public int EliminarProducto(string nombre)
    {
        return productos.RemoveAll(p => p.Nombre == nombre);
    }

    public IProducto BuscarProducto(string nombre)
    {
        return productos.Find(p => p.Nombre == nombre);
    }



}