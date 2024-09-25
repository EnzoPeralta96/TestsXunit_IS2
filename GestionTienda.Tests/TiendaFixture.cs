namespace GestionTienda.Tests;
public class TiendaFixture : IDisposable
{
    public TiendaService Tienda {get; private set; }
    public TiendaFixture()
    {
        var productoRepositorio = new ProductoRepository();
        Tienda = new TiendaService(productoRepositorio);
    }
    
    public void Dispose()
    {
    }
}