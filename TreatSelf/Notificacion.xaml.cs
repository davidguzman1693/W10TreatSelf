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
    public sealed partial class Notificacion : Page
    {
        Usuario usu;
        Notificacion noti;
        public Notificacion()
        {
            this.InitializeComponent();
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
                    Menulist.Add(item);
                    menulist.Add(item1);
                    menulist.Add(item2);
                    menulist.Add(item3);


                }
                return menulist;
            }
            set { menulist = value; }
        }

        private ObservableCollection<MiNotificacion> notis1;

        public ObservableCollection<MiNotificacion> Notis1
        {
            get { return notis1; }
            set { notis1 = value; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            usu = e.Parameter as Usuario;
            tu.Text = usu.Nombre + " " + usu.Apellido;
            listarNotificacinoes();
        }

        private async void listarNotificacinoes()
        {
            notis1 = new ObservableCollection<MiNotificacion>();

            var query = from UsuarioSelected in ParseObject.GetQuery("Notificacion")
                        where UsuarioSelected.Get<string>("Medico") == usu.Id
                        select UsuarioSelected;
            var final = await query.FindAsync();
            MiNotificacion trata;
            int i = 0;
            string nom="";
            foreach (var obj in final)
            {
                i++;
                nom = "Notificacion " + i;
                trata = new MiNotificacion();
                trata.Id = obj.ObjectId;
                trata.Descripcion = obj.Get<string>("Descripcion");
                trata.Nombre = nom;
                trata.Paciente = obj.Get<string>("Paciente");
                trata.Fecha = (DateTime)obj.CreatedAt;
                notis1.Add(trata);
            }
            }

        private async void buscarPaciente(object sender, SelectionChangedEventArgs e)
        {
            Esperar.Visibility = Visibility.Visible;
            MiNotificacion tratar;
            tratar = ((sender as ListBox).SelectedItem as MiNotificacion);
            try
            {
                var query = from UsuarioSelected in ParseObject.GetQuery("User")
                            where UsuarioSelected.ObjectId == tratar.Paciente
                            select UsuarioSelected;
                ParseObject obj = await query.FirstAsync();
                Nombre.Text = obj.Get<string>("Nombre") + " " + obj.Get<string>("Apellido");
                Correo.Text = obj.Get<string>("email");
                Telefono.Text = "" + obj.Get<uint>("telefono");
                Cedula.Text = obj.Get<string>("Cedula");
                Esperar.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Esperar.Visibility = Visibility.Collapsed;

            }
        }

        private void putContent(object sender, SelectionChangedEventArgs e)
        {
            if (menu.SelectedIndex != -1)
            {
                Item it = ((sender as ListBox).SelectedItem as Item);
                Frame rootFrame = Window.Current.Content as Frame;
                switch (it.Name)
                {
                    case "Tratamientos":
                        rootFrame.Navigate(typeof(Tratamientos), usu);
                        break;

                    case "Mi información":
                        rootFrame.Navigate(typeof(MiInformacion), usu);
                        break;

                    case "Pacientes":
                        rootFrame.Navigate(typeof(Paciente), usu);
                        break;
                }
            }
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(AddTratamiento),usu);
        }
    }
    
}
