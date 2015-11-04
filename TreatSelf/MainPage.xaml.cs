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
        //StorageFile;
        public Usuario log;
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private async void Register(object sender, RoutedEventArgs e)
        {
            Esperar1.Visibility = Visibility.Visible;
            
            if (username.Text=="" || password.Password=="" || name.Text=="" || lastname.Text=="" || cedula.Text == ""  || telefono.Text == "" || mail.Text=="") {
                var dialog = new Windows.UI.Popups.MessageDialog("Todos los campos son obligatorios");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("OK") { });
                var result = await dialog.ShowAsync();
            }
            else
            {
                try {
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
                    Esperar1.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    Esperar1.Visibility = Visibility.Collapsed;
                    var dialog = new Windows.UI.Popups.MessageDialog("Error al ingresar los datos");
                    dialog.Commands.Add(new Windows.UI.Popups.UICommand("OK") { });
                    var result = await dialog.ShowAsync();
                }
            }
        }

        private async void login(object sender, RoutedEventArgs e)
        {
            Esperar.Visibility = Visibility.Visible;
            try { 
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
            log.Cedula = obj.Get<string>("Cedula");
            log.Password = obj.Get<string>("password");
            log.Perfil = obj.Get<string>("perfil");
            log.Username = obj.Get<string>("username");
            log.Telefono = obj.Get<uint>("telefono");
                Esperar.Visibility = Visibility.Collapsed;

            if (log.Perfil == "medico")
            {
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(Medico), log);
            }
            else if (log.Perfil == "paciente")
            {
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(Paciente), log);
            }
        }catch(Exception ex){
                Esperar.Visibility = Visibility.Collapsed;
                var dialog = new Windows.UI.Popups.MessageDialog("Revisa tus datos");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("OK") {  });
                var result = await dialog.ShowAsync();
        }
            /*Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Medico));*/

        }

        public void subirImagen()
        {
            //GetByteFromFile(file);
        }

    }
}
