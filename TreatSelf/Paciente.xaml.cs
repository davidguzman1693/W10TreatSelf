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
using Windows.UI.Popups;
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
    public sealed partial class Paciente : Page
    {
        Usuario usu;
        public Paciente()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            usu = e.Parameter as Usuario;
            tu.Text = usu.Nombre + " " + usu.Apellido;
            listarTratas();
        }

        private ObservableCollection<Tratamiento> tratas1;

        public ObservableCollection<Tratamiento> Tratas1
        {
            get { return tratas1; }
            set { tratas1 = value; }
        }

        public async void listarTratas()
        {
            
            DateTime fecha = DateTime.Now;
            
            tratas1 = new ObservableCollection<Tratamiento>();
            var query = from UsuarioSelected in ParseObject.GetQuery("Tratamiento")
                        where UsuarioSelected.Get<string>("paciente") == usu.Id
                        select UsuarioSelected;
            var final = await query.FindAsync();
            Tratamiento trata;
            foreach (var obj in final)
            {
             
                trata = new Tratamiento();
                trata.Id = obj.ObjectId;
                trata.Medico = obj.Get<string>("MedicoId");
                trata.Fechainicio = (DateTime)obj.UpdatedAt;
                trata.Fechafin = obj.Get<DateTime>("FechaFin");
                trata.Fechacontrol = obj.Get<DateTime>("FechaControl");
                trata.NomTratamiento = obj.Get<string>("Nomtratamiento");
                trata.Descripcion = obj.Get<string>("Descripcion");
                tratas1.Add(trata);
                if (trata.Fechacontrol.Month == fecha.Month && trata.Fechacontrol.Year == fecha.Year && trata.Fechacontrol.Day == fecha.Day)
                {

                    var dialog = new ContentDialog()
                    {
                        Title = "TIENES CONTROL",
                        Content = "Tienes control medico del tratamiento "+trata.Fechacontrol.Date,
                        
                        MaxWidth = this.MaxWidth
                    };
                    dialog.PrimaryButtonText = "OK";
                    dialog.IsPrimaryButtonEnabled = true;
                    dialog.PrimaryButtonClick += delegate {
                        notificar();
                    };
                    dialog.ShowAsync();



                }
                
        }
        }

        public async void notificar()
        {
            var dialog = new Windows.UI.Popups.MessageDialog("Le ha dado ok");
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("OK") { });
            var result = await dialog.ShowAsync();
        }

        private ObservableCollection<Item> menulist;

        public ObservableCollection<Item> Menulist
        {
            get
            {
                if (menulist == null)
                {
                    menulist = new ObservableCollection<Item>();

                    Item item1 = new Item() { Name = "Tratamientos", Icon = "Paste" };
                    Item item2 = new Item() { Name = "Mi información", Icon = "ContactPresence" };

                    menulist.Add(item1);
                    menulist.Add(item2);


                }
                return menulist;
            }
            set { menulist = value; }
        }

        private void putContenr(object sender, SelectionChangedEventArgs e)
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
                }
            }

        }

       
        private void logout(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }

        private async void BuscarElMedico(object sender, SelectionChangedEventArgs e)
        {
            Esperar.Visibility = Visibility.Visible;
            Tratamiento tratar;
            tratar = ((sender as ListBox).SelectedItem as Tratamiento);
            try
            {
                var query = from UsuarioSelected in ParseObject.GetQuery("User")
                            where UsuarioSelected.ObjectId == "DD3vNtDn8o"
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
    }
}
