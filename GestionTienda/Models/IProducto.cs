namespace GestionTienda;
public interface IProducto
{
    string Nombre {get;}
    double Precio {get;}
    Categoria Categoria {get;}
    void ModificarPrecio(double precio);
}


