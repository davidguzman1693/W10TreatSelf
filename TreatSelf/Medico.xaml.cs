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
using Windows.UI.Core;
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
    public sealed partial class Medico : Page
    {
        public Medico()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            //tu.Text = MainPage.log.Nombre + " " + MainPage.log.Apellido;
        }

        private ObservableCollection<Item> menulist;

        public ObservableCollection<Item> Menulist
        {
            get {
                if (menulist == null)
                {
                    menulist = new ObservableCollection<Item>();

                    Item item = new Item() { Name = "Pacientes", Icon = "OtherUser" };
                    Item item1 = new Item() { Name = "Tratamientos", Icon = "Paste" };
                    Item item2 = new Item() { Name = "Mi información", Icon = "ContactPresence" };
                    
                    menulist.Add(item);
                    menulist.Add(item1);
                    menulist.Add(item2);
                    

                }
                return menulist; }
            set { menulist = value; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
              = AppViewBackButtonVisibility.Collapsed;
            listaPaci.SelectedIndex = -1;
        }

        private void listaNoPaci_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Usuario lbi1 = ((sender as ListBox).SelectedItem as Usuario);
            int i = listaNoPaci.SelectedIndex;
            data.Add(lbi1);
            data1.Remove(lbi1);
            
            //((ObservableCollection<Usuario>)listaNoPaci.ItemsSource).Remove(lbi1);
            //tb.Text = "Cantidad: " + i;
        }

        public static ObservableCollection<Usuario> data;

        public ObservableCollection<Usuario> Data
        {
            get
            {
                llenar();
                return data;
            }
            set
            {
                data = value;

            }
        }

        public async void llenar()
        {
            if (data == null)
            {
                data = new ObservableCollection<Usuario>();
                /*Usuario usu1 = new Usuario() { Apellido = "Guzman", Nombre = "David", Cedula = "06102038", Correo = "davidguzman@unicauca.edu.co", Telefono = 3184105690 };
                Usuario usu2 = new Usuario() { Apellido = "Guzman", Nombre = "Karen", Cedula = "8302615", Correo = "karenguzman@unicauca.edu.co", Telefono = 31231232 };
                Usuario usu3 = new Usuario() { Apellido = "Delgado", Nombre = "Fanny", Cedula = "3631212", Correo = "fannydelgado@unicauca.edu.co", Telefono = 312312312 };
                data.Add(usu1);
                data.Add(usu2);
                data.Add(usu3);*/
                var query = from UsuarioSelected in ParseObject.GetQuery("User")
                            where UsuarioSelected.Get<string>("username") != ""
                            select UsuarioSelected;
                var final = await query.FindAsync();
                Usuario log;
                foreach (var obj in final)
                {
                    log = new Usuario();
                    log.Id = obj.ObjectId;
                    log.Correo = obj.Get<string>("username");
                    log.Nombre = obj.Get<string>("Nombre");
                    log.Apellido = obj.Get<string>("Apellido");
                    log.Cedula = obj.Get<string>("Cedula");
                    log.Password = obj.Get<string>("password");
                    log.Perfil = obj.Get<string>("perfil");
                    log.Username = obj.Get<string>("username");
                    data.Add(log);
                }


            }
        }

        public static ObservableCollection<Usuario> data1;

        public ObservableCollection<Usuario> Data1
        {
            get
            {
                if (data1 == null)
                {
                    data1 = new ObservableCollection<Usuario>();
                    Usuario usu1 = new Usuario() { Apellido = "Guzman", Nombre = "Paola", Cedula = "5634534", Correo = "paolaguzman@unicauca.edu.co", Telefono = 645664654 };
                    Usuario usu2 = new Usuario() { Apellido = "Delgado", Nombre = "Isabela", Cedula = "543543", Correo = "isadelgado@unicauca.edu.co", Telefono = 654654546 };
                    Usuario usu3 = new Usuario() { Apellido = "Guzman", Nombre = "Oscar", Cedula = "432432", Correo = "oscarguzman@unicauca.edu.co", Telefono = 86876867 };
                    data1.Add(usu1);
                    data1.Add(usu2);
                    data1.Add(usu3);


                }

                return data1;
            }
            set { data1 = value; }
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

        private void goToSelectedPaciente(object sender, SelectionChangedEventArgs e)
        {
            if (listaPaci.SelectedIndex != -1)
            {
                Frame rootFrame = Window.Current.Content as Frame;
                Usuario lbi1 = ((sender as ListBox).SelectedItem as Usuario);
                rootFrame.Navigate(typeof(AddPacTra), data.ElementAt(listaPaci.SelectedIndex));
            }
        }

        private void putContent(object sender, SelectionChangedEventArgs e)
        {
            if(menu.SelectedIndex != -1)
            {
                Item it = ((sender as ListBox).SelectedItem as Item);
                Frame rootFrame = Window.Current.Content as Frame;
                switch (it.Name)
                {
                    case "Tratamientos":
                        rootFrame.Navigate(typeof(Tratamientos));
                        break;

                    case "Mi información":

                        break;
                }
            }
        }

        private void toAddTratamiento(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(AddTratamiento));
        }
    }
}
