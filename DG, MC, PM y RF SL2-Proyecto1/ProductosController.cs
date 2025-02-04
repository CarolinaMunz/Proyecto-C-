using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG__MC__PM_y_RF_SL2_Proyecto1
{
    internal class ProductosController
    {
        public static void GuardarProducto(Producto unProducto)
        {
            List<Producto> productos = ProductosService.LeerProductos() ?? new List<Producto>();

            if (productos.Any(p => p.ID.Equals(unProducto.ID, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Ya existe un producto con el mismo ID.");
            }

            ProductosService.GuardarProducto(unProducto);
        }


        public static List<Producto> CargarMovimientos(List<Producto> productos)
        {
            foreach (Producto producto in productos)
            {
                List<Movimiento> listaMovimientos = MovimientosService.ObtenerMovimientosProducto(producto);
                producto.CargarMovimientos(listaMovimientos ?? new List<Movimiento>());
            }
            return productos;
        }


        public static List<Producto> LeerProductos()
        {
            List<Producto> productos = new List<Producto>();
            productos = ProductosService.LeerProductos();
            if (productos != null)
            {
                productos = CargarMovimientos(productos);
                return productos;
            }
            else
            {
                throw new Exception("No hay productos para mostrar");
            }
        }

        public static void AgregarMovimiento(Producto unProducto, Movimiento unMovimiento)
        {

            MovimientosService.GuardarMovimiento(unMovimiento, unProducto.ID);
        }
    }
}
