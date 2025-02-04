using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG__MC__PM_y_RF_SL2_Proyecto1
{
    internal class ProductosService
    {
        private static string GetAppPath()
        {
            return AppContext.BaseDirectory;
        }

        public static void GuardarProducto(Producto unProducto)
        {
            string fileName = Path.Combine(GetAppPath(), "productos.txt");
            if (!File.Exists(fileName))
            {
                using (StreamWriter archivoSalida = new StreamWriter(fileName))
                {
                    string datos = $"{unProducto.ID};{unProducto.Nombre};{unProducto.StockActual}";
                    archivoSalida.WriteLine(datos);
                }
            }
            else
            {
                using (StreamWriter archivoSalida = new StreamWriter(fileName, true))
                {
                    string datos = $"{unProducto.ID};{unProducto.Nombre};{unProducto.StockActual}";
                    archivoSalida.WriteLine(datos);
                }
            }
        }

        public static List<Producto> LeerProductos()
        {
            string fileName = Path.Combine(GetAppPath(), "productos.txt");
            if (File.Exists(fileName))
            {
                List<Producto> productos = new List<Producto>();
                string[] lineas = File.ReadAllLines(fileName);
                foreach (string productoComoTexto in lineas)
                {
                    var datos = productoComoTexto.Split(';');
                    Producto unProducto = new Producto(datos[0], datos[1]);
                    productos.Add(unProducto);
                }
                return productos;
            }
            else
            {
                return null;
            }
        }

    }
}
