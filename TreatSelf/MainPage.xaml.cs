using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TreatSelf.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TreatSelf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Usuario log;
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private async void Register(object sender, RoutedEventArgs e)
        {
            
            ParseObject appointment = new ParseObject("User");
            appointment["username"] = username.Text;
            appointment["password"] = password.Password;
            appointment["Nombre"] = name.Text;
            appointment["Apellido"] = lastname.Text;
            appointment["Cedula"] = cedula.Text;
            appointment["telefono"] = int.Parse(telefono.Text);
            appointment["email"] = mail.Text;
            
            switch (perfil.IsOn)
            {
                case false:
                    appointment["perfil"] = "paciente";
                    break;

                case true:
                    appointment["perfil"] = "medico";
                    break;
            }
            await appointment.SaveAsync();
        }

        private async void login(object sender, RoutedEventArgs e)
        {
            var query = from UsuarioSelected in ParseObject.GetQuery("User")
                        where UsuarioSelected.Get<string>("username") == user.Text
                        where UsuarioSelected.Get<string>("password") == pass.Password 
                        select UsuarioSelected;
            ParseObject obj = await query.FirstAsync();
            
                log = new Usuario();
                log.Id = obj.ObjectId;
                log.Correo = obj.Get<string>("email");
                log.Nombre = obj.Get<string>("Nombre");
                log.Apellido = obj.Get<string>("Apellido");
                log.Cedula= obj.Get<string>("Cedula");
                log.Password= obj.Get<string>("password");
                log.Perfil= obj.Get<string>("perfil");
                log.Username= obj.Get<string>("username");
                log.Telefono = obj.Get<uint>("telefono");

            if (log.Perfil=="medico")
            {
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(Medico), log);
            }
            else if (log.Perfil=="paciente")
            {
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(Paciente),log);
            }
            else
            {

            }
            /*Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Medico));*/

        }
    }
}
