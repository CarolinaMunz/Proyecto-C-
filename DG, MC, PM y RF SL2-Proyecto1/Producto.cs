using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG__MC__PM_y_RF_SL2_Proyecto1
{
    internal class Producto
    {
        private string _id;
        private string _nombre;
        private readonly List<Movimiento> _movimientos;

        public Producto(string id, string nombre)
        {
            _id = id;
            _nombre = nombre;
            _movimientos = new List<Movimiento>();
        }

        public string ID
        {
            get { return _id; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _id = value;
                }
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _nombre = value;
                }
            }
        }

        public int StockActual
        {
            get
            {
                int stockActual = 0;

                if (_movimientos.Count > 0) 
                {
                    foreach (var movimiento in _movimientos)
                    {
                        stockActual += movimiento.Cantidad;
                    }
                }

                return stockActual;
            }
        }

        public void CargarMovimientos(List<Movimiento> listaMovimientos)
        {
            if (listaMovimientos == null)
            {
                listaMovimientos = new List<Movimiento>();
            }

            _movimientos.AddRange(listaMovimientos);
        }


        public Movimiento AgregarUnidades(int idMovimiento, int cantidadUnidades, DateTime fecha)
        {
            if (cantidadUnidades <= 0)
            {
                throw new ArgumentException("La cantidad de unidades debe ser mayor a 0.");
            }

            Movimiento carga = new Movimiento(idMovimiento, cantidadUnidades, fecha);

            _movimientos.Add(carga);
            return carga;
        }

        public Movimiento RestarUnidades(int idMovimiento, int cantidadUnidades, DateTime fecha)
        {
            if (cantidadUnidades <= 0)
            {
                throw new ArgumentException("La cantidad de unidades debe ser mayor a 0.");
            }

            if (StockActual - cantidadUnidades < 0)
            {
                throw new InvalidOperationException("El stock no puede ser negativo.");
            }

            Movimiento descarga = new Movimiento(idMovimiento, -cantidadUnidades, fecha);

            _movimientos.Add(descarga);
            return descarga;
        }

        public override string ToString()
        {
            return $"{_nombre} [{_id}] | Stock actual: {StockActual}";
        }
    }
}
