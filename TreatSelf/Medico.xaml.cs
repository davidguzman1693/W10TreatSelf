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
        Usuario usu;
        public Medico()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            llenar();
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
                    Item item3 = new Item() { Name = "Notificaciones", Icon = "Comment" };
                    menulist.Add(item);
                    menulist.Add(item1);
                    menulist.Add(item2);
                    menulist.Add(item3);


                }
                return menulist; }
            set { menulist = value; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
              = AppViewBackButtonVisibility.Collapsed;
            listaPaci.SelectedIndex = -1;
            usu = e.Parameter as Usuario;
            tu.Text = usu.Nombre + " " +usu.Apellido;
            
        }

        

        private async void listaNoPaci_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Usuario lbi1 = new Usuario();
            try { 
            lbi1 = ((sender as ListBox).SelectedItem as Usuario);
            ParseObject appointment = new ParseObject("MedPac");
            appointment["Medico"] = usu.Id;
            appointment["Paciente"] = lbi1.Id;
            await appointment.SaveAsync();
            data.Add(lbi1);
            data1.Remove(lbi1);
            }
            catch (Exception ex)
            {

            }

            //((ObservableCollection<Usuario>)listaNoPaci.ItemsSource).Remove(lbi1);
            //tb.Text = "Cantidad: " + i;
        }

        public static ObservableCollection<Usuario> data;

        public ObservableCollection<Usuario> Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;

            }
        }

        public async void llenar()
        {
                data = new ObservableCollection<Usuario>();
                data1 = new ObservableCollection<Usuario>();
            try { 
            var query = from UsuarioSelected in ParseObject.GetQuery("User")
                        where UsuarioSelected.Get<string>("perfil") == "paciente"
                        select UsuarioSelected;
            var final = await query.FindAsync();

            var query1 = from PacMed in ParseObject.GetQuery("MedPac")
                         where PacMed.Get<string>("Medico") == usu.Id
                         select PacMed;
            var final1 = await query1.FindAsync();

            Usuario log;
            if (final1.Count() == 0)
            {
                foreach (var obj in final)
                {
                    log = new Usuario();
                    log.Id = obj.ObjectId;
                    log.Correo = obj.Get<string>("Nombre");
                    log.Nombre = obj.Get<string>("Nombre");
                    log.Apellido = obj.Get<string>("Apellido");
                    log.Cedula = obj.Get<string>("Cedula");
                    log.Password = obj.Get<string>("password");
                    log.Perfil = obj.Get<string>("perfil");
                    log.Username = obj.Get<string>("username");
                    data1.Add(log);
                }
            }
            else
            {
                foreach (var obj in final)
                {
                    log = new Usuario();
                    
                    log.Id = obj.ObjectId;
                    log.Correo = obj.Get<string>("Nombre");
                    log.Nombre = obj.Get<string>("Nombre");
                    log.Apellido = obj.Get<string>("Apellido");
                    log.Cedula = obj.Get<string>("Cedula");
                    log.Password = obj.Get<string>("password");
                    log.Telefono = obj.Get<uint>("telefono");
                    log.Perfil = obj.Get<string>("perfil");
                    log.Username = obj.Get<string>("username");
                    data1.Add(log);
                    foreach (var obj1 in final1)
                    {
                        
                        if (obj1.Get<string>("Paciente") == obj.ObjectId)
                        {
                            
                            data1.Remove(log);
                            log.MedPac = obj1.ObjectId; 
                            data.Add(log);
                        }
                       
                    }
                }

            }
            }
            catch (Exception e)
            {
                var panel = new StackPanel();

                panel.Children.Add(new TextBlock
                {
                    Text = "No se ha podido carga la información de paciente, inicia sesion nuevamente",
                    TextWrapping = TextWrapping.Wrap,
                });

                var dialog = new ContentDialog()
                {
                    Title = "ERROR",
                    MaxWidth = this.MaxWidth
                };

                dialog.Content = panel;
                dialog.PrimaryButtonText = "OK";
                dialog.IsPrimaryButtonEnabled = true;
                dialog.PrimaryButtonClick += delegate {
                    Frame rootFrame = Window.Current.Content as Frame;
                    rootFrame.Navigate(typeof(MainPage));
                };
                dialog.ShowAsync();
            }

        }
        

       public static ObservableCollection<Usuario> data1;

        public ObservableCollection<Usuario> Data1
        {
            get
            {
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
                //Usuario lbi1 = ((sender as ListBox).SelectedItem as Usuario);
                Collection<Usuario> usuarios = new Collection<Usuario>();
                usuarios.Add(usu);
                usuarios.Add(((sender as ListBox).SelectedItem as Usuario));
                rootFrame.Navigate(typeof(AddPacTra), usuarios);
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
                        rootFrame.Navigate(typeof(Tratamientos),usu);
                        break;

                    case "Mi información":
                        rootFrame.Navigate(typeof(MiInformacion), usu);
                        break;
                    case "Notificaciones":
                        rootFrame.Navigate(typeof(Notificacion), usu);
                        break;
                }
            }
        }

        private void toAddTratamiento(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(AddTratamiento),usu);
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }
    }
}
