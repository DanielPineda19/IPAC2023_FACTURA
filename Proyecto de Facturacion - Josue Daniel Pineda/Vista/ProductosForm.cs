using System.Windows.Forms;

namespace Vista
{
    public partial class ProductosForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ProductosForm()
        {
            InitializeComponent();
        }

        //Al igual que en el formulario de usuario, voy a tener una variable global operación, ya que el boton "Guardar" me sirve para crear un nuevo producto como para modificar uno que ya existe
        //Entonces con esta varaible capturo si el usuario, si "Nuevo" o "Modificar"
        string operacion;

        //BOTON NUEVO
        private void NuevoButton_Click(object sender, System.EventArgs e)
        {
            operacion = "Nuevo"; //le asigno "Nuevo" a la variable operación, ya que presiono el boton nuevo
            HabilitarControles();
        }

        //Metodo que habilita los controles
        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            DescripcionTextBox.Enabled = true;
            ExistenciaTextBox.Enabled = true;
            PrecioTextBox.Enabled = true;
            AdjuntarImagenButton.Enabled = true;
            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;
            NuevoButton.Enabled = false; //Cuando de clic en el boton "Nuevo" se va bloquear el boton "Nuevo" se habilitan los anteriores
        }


        //BOTON MODIFICAR
        private void ModificarButton_Click(object sender, System.EventArgs e)
        {
            operacion = "Modificar"; //Si el usuario la clic en Modificar a la variable operación le asigna "Modificar" 
        }


        //BOTON CANCELAR
        private void CancelarButton_Click(object sender, System.EventArgs e)
        {
            DeshabilitarControles(); //Llamado a procedimiento para deshabilitar los controles
        }

        //Metodo que deshabilita los controles
        private void DeshabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            DescripcionTextBox.Enabled = false;
            ExistenciaTextBox.Enabled = false;
            PrecioTextBox.Enabled = false;
            AdjuntarImagenButton.Enabled = false;
            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            NuevoButton.Enabled = true; //Cuando deshabilita los controles al dar "Cancelar" queda habilitado el boton "Nuevo" nuevamente
        }


        //BOTON GUARDAR
        private void GuardarButton_Click(object sender, System.EventArgs e)
        {
            if (operacion == "Nuevo") //Cuando le de clic en "Guardar" y en la variable operación este "Nuevo", significa que el suario crea un nuevo producto 
            {
                //Aqui valido que el usuario ingrese los datos
                if (string.IsNullOrEmpty(CodigoTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un código");
                    CodigoTextBox.Focus(); //Devuelve al control donde se encontro el error
                    return;
                }
                errorProvider1.Clear(); //Limpia el error cuando ya no exista

                if (string.IsNullOrEmpty(DescripcionTextBox.Text))
                {
                    errorProvider1.SetError(DescripcionTextBox, "Ingrese una descripción");
                    DescripcionTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(ExistenciaTextBox.Text))
                {
                    errorProvider1.SetError(ExistenciaTextBox, "Ingrese una existencia");
                    ExistenciaTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(PrecioTextBox.Text))
                {
                    errorProvider1.SetError(PrecioTextBox, "Ingrese un precio");
                    PrecioTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

            }
        }


        //VALIDO LA EXISTENCIA, CON EL EVENTO KEYPRESS
        private void ExistenciaTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Si "e" es un numero no pasa nada
            if (char.IsNumber(e.KeyChar))
            {

            }
            //Sino es un numero,le mando True al Handled para que bloquee el caracter y no lo coloque en el TextBox
            else
            {
                e.Handled = true; //Handled: cancela lo que trae el caracter (e) y no lo coloca en el TextBox
            }
        }


        //VALIDO EL PRECIO, CON EL EVENTO KEYPRESS
        private void PrecioTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Si no es un numero y es dintinto de punto (.): entoces me bloquea
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            //Para no ingresar mas de  un punto (.)
            //Si ya tengo un punto ya no me va dejar agregar otro punto
            //Valido que solo me permita ingresar un punto nada mas
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

        }
    }
}
