namespace GestionTienda.Tests;
public class TiendaFixture : IDisposable
{
    public Tienda Tienda {get; private set; }
    public TiendaFixture()
    {
        var productoRepositorio = new ProductoRepositorio();
        Tienda = new Tienda(productoRepositorio);
    }
    
    public void Dispose()
    {
    }
}