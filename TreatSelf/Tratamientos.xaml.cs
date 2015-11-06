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
    public sealed partial class Tratamientos : Page
    {
        Usuario usu;
        public Tratamientos()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            usu = e.Parameter as Usuario;
            tu.Text = usu.Nombre+" " +usu.Apellido;
        }
        private ObservableCollection<Tratamiento> tratas1;

        public ObservableCollection<Tratamiento> Tratas1
        {
            get
            {
                listarTratamientos();
               

                return tratas1;
            }
            set { tratas1 = value; }
        }

        public async void listarTratamientos()
        {
            if (tratas1 == null)
            {
                tratas1 = new ObservableCollection<Tratamiento>();
                Tratamiento trata = new Tratamiento();
                var query = from UsuarioSelected in ParseObject.GetQuery("Tratamiento")
                            where UsuarioSelected.Get<string>("MedicoId") == usu.Id
                            where UsuarioSelected.Get<string>("paciente") == usu.Id
                            select UsuarioSelected;
                var final = await query.FindAsync();
                foreach (var obj in final) {
                    
                        trata = new Tratamiento();
                        trata.Id = obj.ObjectId;
                        trata.Fechainicio = (DateTime) obj.UpdatedAt;
                        trata.Fechafin = obj.Get<DateTime>("FechaFin");
                        trata.Fechacontrol = obj.Get<DateTime>("FechaControl");
                        trata.NomTratamiento = obj.Get<string>("Nomtratamiento");
                        trata.Descripcion = obj.Get<string>("Descripcion");
                        tratas1.Add(trata);
                    
                }
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
                case "Pacientes":
                    rootFrame.Navigate(typeof(Medico),usu);
                    break;

                case "Mi información":
                    rootFrame.Navigate(typeof(MiInformacion), usu);
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

        private void goToAddTratamiento(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(AddTratamiento), usu);
        }
    }

    

    
}
