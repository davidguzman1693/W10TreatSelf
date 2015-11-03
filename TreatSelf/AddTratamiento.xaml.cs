using Parse;
using System;
using System.Collections.Generic;
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
    public sealed partial class AddTratamiento : Page
    {
        Usuario usu;
        Frame rootFrame;
        public AddTratamiento()
        {
            this.InitializeComponent();
            rootFrame = Window.Current.Content as Frame;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
               = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += AddTratamiento_BackRequested;
        }

        private void AddTratamiento_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            usu = e.Parameter as Usuario;
        }

        private async void AgregarTratamiento(object sender, RoutedEventArgs e)
        {
            ParseObject appointment = new ParseObject("Tratamiento");
            
            appointment["Nomtratamiento"] = nomtra.Text;
            appointment["Descripcion"] = desc.Text;
            appointment["FechaControl"] = fini.Date.Date;
            appointment["FechaFin"] = ffin.Date.Date;
            appointment["MedicoId"] = usu.Id;
            appointment["paciente"] = usu.Id;
            await appointment.SaveAsync();
            
        }
    }
}
