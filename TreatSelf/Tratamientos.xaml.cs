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
        public Tratamientos()
        {
            this.InitializeComponent();
        }

        private ObservableCollection<Tratamiento> tratas1;

        public ObservableCollection<Tratamiento> Tratas1
        {
            get
            {
                if (tratas1 == null)
                {
                    tratas1 = new ObservableCollection<Tratamiento>();
                    Tratamiento trata1 = new Tratamiento() { NomTratamiento = "Trata3", Descripcion = "Descripcion3", Fechainicio = DateTime.Now , Fechacontrol = DateTime.Now, Fechafin = DateTime.Now };
                    Tratamiento trata2 = new Tratamiento() { NomTratamiento = "Trata4", Descripcion = "Descripcion4", Fechainicio = DateTime.Now, Fechacontrol = DateTime.Now, Fechafin = DateTime.Now };
                    Tratamiento trata3 = new Tratamiento() { NomTratamiento = "Trata5", Descripcion = "Descripcion5", Fechainicio = DateTime.Now, Fechacontrol = DateTime.Now, Fechafin = DateTime.Now };

                    tratas1.Add(trata1);
                    tratas1.Add(trata2);
                    tratas1.Add(trata3);
                }

                return tratas1;
            }
            set { tratas1 = value; }
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

                    menulist.Add(item);
                    menulist.Add(item1);
                    menulist.Add(item2);


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
                    rootFrame.Navigate(typeof(Medico));
                    break;

                case "Mi información":

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
    }

    

    
}
