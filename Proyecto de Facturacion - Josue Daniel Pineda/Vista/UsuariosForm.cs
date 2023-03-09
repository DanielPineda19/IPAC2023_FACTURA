using Datos;
using Entidades;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Vista
{
    public partial class UsuariosForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public UsuariosForm()
        {
            InitializeComponent();
        }

        //Variale global (TipoOperacion): la alimento cuando de clic en el boton de nuevo o cancelar
        string tipoOperacion;

        DataTable dt = new DataTable();
        UsuarioDB UsuarioDB = new UsuarioDB();

        //Instancio objeto de la clase Usuario (tambien en este objeto utilizare el metodo que va insertar en la BD, va viajar hasta el proyecto Datos )
        Usuario user = new Usuario();


        //BOTON NUEVO
        private void NuevoButton_Click(object sender, System.EventArgs e)
        {
            CodigoTextBox.Focus(); //Cuando clic en Nuevo se va poner el cursor en la primera casilla
            HabilitarControles(); //Llama a procedimiento para habilitar los controles
            tipoOperacion = "Nuevo"; //Cuando clic en Nuevo va ser igual a nuevo para saber si es un usuario nuevo
        }

        //Metodo para habilitar los controles al dar clic en el boton nuevo
        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            NombreTextBox.Enabled = true;
            ContraseñaTextBox.Enabled = true;
            CorreoTextBox.Enabled = true;
            RolComboBox.Enabled = true;
            EstaActivoCheckBox.Enabled = true;
            AdjuntarFotoButton.Enabled = true;
            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;
            ModificarButton.Enabled = false;
        }


        //BOTON MODIFICAR
        private void ModificarButton_Click(object sender, System.EventArgs e)
        {
            tipoOperacion = "Modificar"; //Para saber si el usuario existe y lo va modificar
        }


        //BOTON CANCELAR
        private void CancelarButton_Click(object sender, System.EventArgs e)
        {
            DeshabilitarControles(); //Llama procedimiento para deshabilitar los controles
            LimpiarControles(); //Llama procedimiento para limpiar los controles cuando le de clic en Cancelar
        }

        //Metodo para deshabilitar los controles al dar clic en el boton cancelar
        private void DeshabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            NombreTextBox.Enabled = false;
            ContraseñaTextBox.Enabled = false;
            CorreoTextBox.Enabled = false;
            RolComboBox.Enabled = false;
            EstaActivoCheckBox.Enabled = false;
            AdjuntarFotoButton.Enabled = false;
            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            ModificarButton.Enabled = true;
        }

        //Metodo para limpiar los controles cuando le de clic en Cancelar
        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            NombreTextBox.Clear();
            ContraseñaTextBox.Clear();
            CorreoTextBox.Clear();
            RolComboBox.Text = "";
            EstaActivoCheckBox.Checked = false;
            FotoPictureBox.Image = null;
        }


        //BOTON GUARDAR
        private void GuardarButton_Click(object sender, System.EventArgs e)
        {
            //Si la presiono el boton Nuevo (insertar un nuevo usuario) dentro va el para crear un nuevo usuario
            if (tipoOperacion == "Nuevo")
            {
                //VALIDO QUE EL USUARIO HAYA INGRESADO LOS DATOS CON UN ERRORPROVIDER (OJO --> EL CORREO ES OPCIONAL QUE LO INGRESE)
                if (string.IsNullOrEmpty(CodigoTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese un código");
                    CodigoTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(NombreTextBox.Text))
                {
                    errorProvider1.SetError(NombreTextBox, "Ingrese un nombre");
                    NombreTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(ContraseñaTextBox.Text))
                {
                    errorProvider1.SetError(ContraseñaTextBox, "Ingrese una contraseña");
                    ContraseñaTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(RolComboBox.Text))
                {
                    errorProvider1.SetError(RolComboBox, "Seleccione un rol");
                    RolComboBox.Focus();
                    return;
                }
                errorProvider1.Clear();


                //OBJETO PARA INSERTAR EN LA BASE DE DATOS
                //Al objeto le paso las cajas de texto
                user.CodigoUsuario = CodigoTextBox.Text;
                user.Nombre = NombreTextBox.Text;
                user.Contraseña = ContraseñaTextBox.Text;
                user.Rol = RolComboBox.Text;
                user.Correo = CorreoTextBox.Text;
                user.EstaActivo = EstaActivoCheckBox.Checked; //(Checked, verifica si esta activo el ChechBox, si esta activo le mando un True y sino, le mando un False

                //Valido si el PictureBox tiene la foto (puede que no la haya ingresado)
                //Si la propiedad .Image es diferente de Null, significa que si tiene foto, entonces ahi si le paso a la propiedad de la clase Usuario (Foto) lo que tiene el PictureBox
                if (FotoPictureBox.Image != null)
                {
                    //Esta clase sirve para manejar archivos, instancio el objeto (ms)
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();

                    //al objeto (ms) le paso la foto que tiene el PictureBox
                    FotoPictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    
                    //Al ms (Memory Stream) le paso la  propiedad user.foto
                    user.Foto = ms.GetBuffer(); //El GetBuffer devuelve el arreglo del Memory Stream y le paso al arreglo de Bytes de la foto
                }

                //Insertar en la base de datos
                bool inserto = UsuarioDB.Insertar(user);

                if (inserto)
                {
                    LimpiarControles();
                    DeshabilitarControles();
                    TraerUsuarios();
                    MessageBox.Show("Registro Guardado");
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el registro");
                }
            }
            else if (tipoOperacion == "Modificar")
            {

            }
        }


        //BOTON ADJUNTAR FOTO DEL USUARIO DESDE EL EQUIPO LOCAL
        private void AdjuntarFotoButton_Click(object sender, System.EventArgs e)
        {
            //Instancio un Objeto de la clase OpenFileDialog, lo cual me ayuda abrir un ventana para seleccionar una foto de nuestro equipo
            OpenFileDialog dialog = new OpenFileDialog();

            //Instancio un objeto de DialogResult para capturar la imagen que el usuario selecciono
            //Lo que le usuario seleccione de la ventana se va almcenar en el objeto RESULTADO
            DialogResult resultado = dialog.ShowDialog(); //Muesto el dialogo y lo capturo en el objeto de la clase DialogResult


            //Si el usuario ingreso la imagen, en el Picture Box se mostrara la imagen
            if (resultado == DialogResult.OK)
            {
                FotoPictureBox.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void UsuariosForm_Load(object sender, System.EventArgs e)
        {
            TraerUsuarios();
        }

        private void TraerUsuarios()
        {
            dt = UsuarioDB.DevolverUsuarios();

            UsuariosDataGridView.DataSource = dt;

        }

    }
}
