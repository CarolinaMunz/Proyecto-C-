using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG__MC__PM_y_RF_SL2_Proyecto1
{
    internal class Movimiento
    {
        public int Id { get; }
        public int Cantidad { get; }
        public DateTime Fecha { get; }

        public Movimiento(int id, int cantidad,
            DateTime fecha)
        {
            Id = id;
            Cantidad = cantidad;
            Fecha = fecha;
        }
    }
}
