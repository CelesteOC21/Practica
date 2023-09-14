using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Manejador;

namespace Tienda1
{
    public partial class FrmProductos : Form
    {
        private ManejadorProductos _manejadorProductos;
        private bool guardando = true;
        private int ID = 0;
        public FrmProductos()
        {
            InitializeComponent();
            _manejadorProductos = new ManejadorProductos();

        }


        private void LlenarProducto(string valor)
        {
            dtgProductos.DataSource = _manejadorProductos.ObtenerProducto(valor);
        }

        public void GuardarProducto()
        {

            EntidadesProducto entidadesProducto = new EntidadesProducto();
            
            entidadesProducto.Id = 0;
            entidadesProducto.Nombre = txtNombre.Text;
            entidadesProducto.Descripcion = txtDescripcion.Text;
            entidadesProducto.Precio = double.Parse(txtPrecio.Text);

            var validar = _manejadorProductos.ValidarDatosProducto(entidadesProducto);

            if (validar.Item1)
            {
                _manejadorProductos.GuardarProducto(entidadesProducto);
                LlenarProducto("");
                LimpiarCuadro();
                ControlarBotones(false, true, true, false);
                ControlCuadros(false);
            }
            else
            {
                MessageBox.Show(validar.Item2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       

        private void ControlarBotones(Boolean nuevo, Boolean guardar, Boolean eliminar, Boolean cancelar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnEliminar.Enabled = eliminar;
            btnCancelar.Enabled = cancelar;
        }

        private void ControlCuadros(Boolean estado)
        {
            txtNombre.Enabled = estado;
            txtDescripcion.Enabled = estado;
            txtPrecio.Enabled = estado;
        }

        private void LimpiarCuadro()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (guardando)
            {
                GuardarProducto();
            }
            else
            {

            }
            
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            ControlarBotones(true, true, true, true);
            ControlCuadros(true);
            LlenarProducto("");
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ControlarBotones(false, true, false, true);
            ControlCuadros(true);
            txtNombre.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ControlarBotones(true, false, true, false);
            ControlCuadros(true);
            LimpiarCuadro();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarProducto();
            LlenarProducto("");
        }

        private void EliminarProducto()
        {
            if (MessageBox.Show("Desea eliminar el producto seleccionado?", "Eliminar producto",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var idProducto = int.Parse(dtgProductos.CurrentRow.Cells["id"].Value.ToString());
                _manejadorProductos.EliminarProducto(idProducto);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
