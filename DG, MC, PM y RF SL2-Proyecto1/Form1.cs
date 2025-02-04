using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace DG__MC__PM_y_RF_SL2_Proyecto1
{
    public partial class v : Form
    {
        public v()
        {
            InitializeComponent();
        }
        
        private List<Producto> _listaProductos = new List<Producto>();
        private Producto _unProducto;
        int IDMov = 1;

        private void sincronizarListado()
        {
            this.lbProductos.Items.Clear();;
            try
            {
                _listaProductos = ProductosController.LeerProductos();
                foreach (var producto in _listaProductos)
                {
                    this.lbProductos.Items.Add(producto);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hubo un error  en la lectura de productos. {ex.Message}");
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string idProducto = this.txtID.Text.Trim();
            string nombreProducto = this.txtNombre.Text.Trim();

            if (string.IsNullOrEmpty(idProducto) || string.IsNullOrEmpty(nombreProducto))
            {
                MessageBox.Show("Debe ingresar tanto el ID como el nombre del producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool idExiste = _listaProductos.Any(p => p.ID.Equals(idProducto, StringComparison.OrdinalIgnoreCase));
            if (idExiste)
            {
                MessageBox.Show("Ya existe un producto con el mismo ID. Por favor, ingrese un ID diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Producto unProducto = new Producto(idProducto, nombreProducto);
            ProductosController.GuardarProducto(unProducto);

            sincronizarListado();

            this.txtID.Clear();
            this.txtNombre.Clear();
            this.txtID.Focus();
        }


        private void v_Load(object sender, EventArgs e)
        {
            sincronizarListado();
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            sincronizarListado();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DateTime.Now.ToString(), "Fecha y hora de modificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
            if (this.lbProductos.SelectedIndex != -1)
            {
                _unProducto = this._listaProductos[this.lbProductos.SelectedIndex];

                int cantidadIngresada = (int)this.nudCantMov.Value;
                if (this.cbTipoMov.SelectedIndex == 0)
                {
                    Movimiento unMovimiento = _unProducto.AgregarUnidades(IDMov, cantidadIngresada, DateTime.Now);
                    ProductosController.AgregarMovimiento(_unProducto, unMovimiento);
                    IDMov++;
                }
                else
                {
                    try
                    {
                        Movimiento unMovimiento = _unProducto.RestarUnidades(IDMov, cantidadIngresada, DateTime.Now);
                        ProductosController.AgregarMovimiento(_unProducto, unMovimiento);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                this.sincronizarListado();
            }
            else
            {
                MessageBox.Show("Se tiene que seleccionar un Producto desde la lista.");
            }
        }
    }
}
