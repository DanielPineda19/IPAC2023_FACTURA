using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        //Estoy llamando el Formulario de Usuario para mostrarlo al dar clic en el boton Usuario
        private void UsuariosToolStripButton_Click(object sender, EventArgs e)
        {
            UsuariosForm userForm = new UsuariosForm(); //Instancio un objeto de la clase del formulario de usuario
            userForm.MdiParent = this; //El  formulario usuario se acopla al formulario de menú (se ejecuta dentro del formulario padre como un formulario hijo) 
            userForm.Show(); //Muestro el formulario para eso utilizo.Show
        }

        private void ProductosToolStripButton_Click(object sender, EventArgs e)
        {
            ProductosForm productosForm = new ProductosForm();
            productosForm.MdiParent = this;
            productosForm.Show();
        }
    }
}
