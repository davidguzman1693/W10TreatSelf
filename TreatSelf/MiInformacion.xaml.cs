using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TreatSelf
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MiInformacion : Page
    {
        Usuario usu;
        public MiInformacion()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            usu = e.Parameter as Usuario;
            nombre.Text = usu.Nombre;
            apellido.Text = usu.Apellido;
            correo.Text = usu.Correo;
            telefono.Text = ""+usu.Telefono;
            cedula.Text = usu.Cedula;
            username.Text = usu.Username;
            password.Password = usu.Password;
            tu.Text = usu.Nombre+" "+usu.Apellido;

        }

        private async void cambiarMiInfo(object sender, RoutedEventArgs e)
        {
            Esperar1.Visibility = Visibility.Visible;
            try
            {
                var trata = new ParseObject("User");
                trata.ObjectId = usu.Id;
                trata["Nombre"] = nombre.Text;
                trata["Apellido"] = apellido.Text;
                trata["email"] = correo.Text;
                trata["telefono"] = int.Parse(telefono.Text);
                trata["cedula"] = cedula.Text;
                trata["username"] = username.Text;
                trata["password"] = password.Password;

                usu.Nombre = nombre.Text;
                usu.Apellido = apellido.Text;
                usu.Correo = correo.Text;
                usu.Telefono = uint.Parse(telefono.Text);
                usu.Cedula = cedula.Text;
                usu.Username = username.Text;
                usu.Password = password.Password;

                await trata.SaveAsync();
                Esperar1.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Esperar1.Visibility = Visibility.Collapsed;
                var dialog = new Windows.UI.Popups.MessageDialog("Tu información no ha podido ser editada");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("OK") { });
                var result = await dialog.ShowAsync();
            }
        }

        private ObservableCollection<Item> menulist;

        public ObservableCollection<Item> Menulist
        {
            get
            {
                if (menulist == null)
                {
                    menulist = new ObservableCollection<Item>();

                    Item item = new Item() { Name = "Pacientes", Icon = "OtherUser" };
                    Item item1 = new Item() { Name = "Tratamientos", Icon = "Paste" };
                    Item item2 = new Item() { Name = "Mi información", Icon = "ContactPresence" };
                    Item item3 = new Item() { Name = "Notificaciones", Icon = "Comment" };
                    menulist.Add(item);
                    menulist.Add(item1);
                    menulist.Add(item2);
                    menulist.Add(item3);


                }
                return menulist;
            }
            set { menulist = value; }
        }

        private void putContent(object sender, SelectionChangedEventArgs e)
        {
               Item it = ((sender as ListBox).SelectedItem as Item);
                Frame rootFrame = Window.Current.Content as Frame;
                switch (it.Name)
                {
                    case "Tratamientos":
                        rootFrame.Navigate(typeof(Tratamientos), usu);
                        break;

                    case "Pacientes":
                        rootFrame.Navigate(typeof(Medico), usu);
                        break;
                     case "Notificaciones":
                        rootFrame.Navigate(typeof(Notificacion), usu);
                        break;
                
                }
           
        }

        private void showMenu(object sender, RoutedEventArgs e)
        {
            if (panel1.IsPaneOpen.Equals(true))
            {
                panel1.IsPaneOpen = false;
            }
            else
            {
                panel1.IsPaneOpen = true;
            }
        }

        private void toAddTratamiento(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(AddTratamiento), usu);
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }
    }
}
