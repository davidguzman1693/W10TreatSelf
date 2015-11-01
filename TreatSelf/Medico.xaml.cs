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
            Usuarios.data.Add(lbi1);
            Usuarios.data1.Remove(lbi1);
            
            //((ObservableCollection<Usuario>)listaNoPaci.ItemsSource).Remove(lbi1);
            //tb.Text = "Cantidad: " + i;
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
                rootFrame.Navigate(typeof(AddPacTra), Usuarios.data.ElementAt(listaPaci.SelectedIndex));
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
    }
}
