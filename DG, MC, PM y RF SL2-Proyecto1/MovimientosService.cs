using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG__MC__PM_y_RF_SL2_Proyecto1
{
    internal class MovimientosService
    {
        private static string GetAppPath()
        {
            return AppContext.BaseDirectory;
        }

        public static void GuardarMovimiento(Movimiento unMovimiento, string idProducto)
        {
            string fileName = Path.Combine(GetAppPath(), "movimientos.txt");
            if (!File.Exists(fileName))
            {
                using (StreamWriter archivoSalida = new StreamWriter(fileName))
                {
                    string datos = $"{unMovimiento.Id};{unMovimiento.Cantidad};{unMovimiento.Fecha};{idProducto}";
                    archivoSalida.WriteLine(datos);
                }
            }
            else
            {
                using (StreamWriter archivoSalida = new StreamWriter(fileName, true))
                {
                    string datos = $"{unMovimiento.Id};{unMovimiento.Cantidad};{unMovimiento.Fecha};{idProducto}";
                    archivoSalida.WriteLine(datos);
                }
            }
        }

        public static List<Movimiento> ObtenerMovimientosProducto(Producto unProducto)
        {
            string fileName = Path.Combine(GetAppPath(), "movimientos.txt");
            List<Movimiento> movimientos = new List<Movimiento>();

            if (File.Exists(fileName))
            {
                string[] lineas = File.ReadAllLines(fileName);
                foreach (string movimientoComoTexto in lineas)
                {
                    var datos = movimientoComoTexto.Split(';');
                    if (unProducto.ID == datos[datos.Length - 1])
                    {
                        Movimiento unMovimiento = new Movimiento(
                            int.Parse(datos[0]),
                            int.Parse(datos[1]),
                            DateTime.Parse(datos[2])
                        );
                        movimientos.Add(unMovimiento);
                        Console.WriteLine($"Movimiento cargado: {datos[0]} - {datos[1]} - {datos[2]} - {datos[3]}");
                    }
                }
            }

            Console.WriteLine($"Total de movimientos para producto {unProducto.ID}: {movimientos.Count}");
            return movimientos;
        }


    }
}
