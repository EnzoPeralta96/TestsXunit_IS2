namespace GestionTienda;
public interface IProductoRepositorio
{
    List<IProducto> obtenerProductos();
    void AgregarProducto(IProducto producto);
    int EliminarProducto(string nombre);
    IProducto BuscarProducto(string nombre);
}
