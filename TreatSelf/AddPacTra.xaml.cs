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
    public sealed partial class AddPacTra : Page
    {
        Usuario usu;
        Frame rootFrame;
        public AddPacTra()
        {
            this.InitializeComponent();
            rootFrame = Window.Current.Content as Frame;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
                = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += AddPacTra_BackRequested;
        }

        private void AddPacTra_BackRequested(object sender, BackRequestedEventArgs e)
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

            Nombre.Text = usu.Nombre+" "+usu.Apellido;
            
            Correo.Text = usu.Correo;
            Cedula.Text = ""+usu.Cedula;
            Telefono.Text = ""+usu.Telefono;              
        }

        private ObservableCollection<Tratamiento> tratas1;

        public ObservableCollection<Tratamiento> Tratas1
        {
            get {
                if (tratas1 == null)
                {
                    tratas1 = new ObservableCollection<Tratamiento>();
                    Tratamiento trata1 = new Tratamiento() { NomTratamiento = "Trata3", Descripcion = "Descripcion3", Fechainicio = DateTime.Now, Fechacontrol = DateTime.Now, Fechafin = DateTime.Now };
                    Tratamiento trata2 = new Tratamiento() { NomTratamiento = "Trata4", Descripcion = "Descripcion4", Fechainicio = DateTime.Now, Fechacontrol = DateTime.Now, Fechafin = DateTime.Now };
                    Tratamiento trata3 = new Tratamiento() { NomTratamiento = "Trata5", Descripcion = "Descripcion5", Fechainicio = DateTime.Now, Fechacontrol = DateTime.Now, Fechafin = DateTime.Now };

                    tratas1.Add(trata1);
                    tratas1.Add(trata2);
                    tratas1.Add(trata3);
                }

                return tratas1; }
            set { tratas1 = value; }
        }

        private ObservableCollection<Tratamiento> tratas2;

        public ObservableCollection<Tratamiento> Tratas2
        {
            get {
                    if(tratas2 == null)
                {
                    tratas2 = new ObservableCollection<Tratamiento>();
                    Tratamiento trata1 = new Tratamiento() {NomTratamiento="Trata1",Descripcion="Descripcion1",Fechainicio= DateTime.Now, Fechacontrol= DateTime.Now, Fechafin= DateTime.Now };
                    Tratamiento trata2 = new Tratamiento() { NomTratamiento = "Trata2", Descripcion = "Descripcion2", Fechainicio = DateTime.Now, Fechacontrol = DateTime.Now, Fechafin = DateTime.Now };
                    Tratamiento trata3 = new Tratamiento() { NomTratamiento = "Trata3", Descripcion = "Descripcion3", Fechainicio = DateTime.Now, Fechacontrol = DateTime.Now, Fechafin = DateTime.Now };

                    tratas2.Add(trata1);
                    tratas2.Add(trata2);
                    tratas2.Add(trata3);
                }
                return tratas2; }
            set { tratas2 = value; }
        }


    }
}
