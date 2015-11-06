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
    public sealed partial class AddPacTra : Page
    {
        Usuario usu;
        ParseObject obj = new ParseObject("User");
        Usuario medico;
        Collection<Usuario> usus;
        Tratamiento tratar;
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
            usus = new Collection<Usuario>();
            usus = e.Parameter as Collection<Usuario>;
            usu = usus.ElementAt<Usuario>(1);
            medico = usus.ElementAt<Usuario>(0);
            llenarDatos();
            Nombre.Text = usu.Nombre + " " + usu.Apellido;
            Correo.Text = usu.Correo;
            Cedula.Text = ""+usu.Cedula;
            Telefono.Text = ""+usu.Telefono;              
        }

        

        private ObservableCollection<Tratamiento> tratas1;

        public ObservableCollection<Tratamiento> Tratas1
        {

            get {
                
                return tratas1; }
            set { tratas1 = value; }
        }


        public async void llenarDatos()
        {       
            tratas1 = new ObservableCollection<Tratamiento>();
            tratas2 = new ObservableCollection<Tratamiento>();
            //Tratamiento trata1 = new Tratamiento() { NomTratamiento = "Trata1", Descripcion = "Descripcion1", Fechainicio = DateTime.Now, Fechacontrol = DateTime.Now, Fechafin = DateTime.Now };
            //tratas2.Add(trata1);
            Esperar1.Visibility = Visibility.Visible;
            try {
                Tratamiento trata;
                var query = from UsuarioSelected in ParseObject.GetQuery("Tratamiento")
                            where UsuarioSelected.Get<string>("MedicoId") == medico.Id
                            select UsuarioSelected;
                var final = await query.FindAsync();
                foreach (var obj in final)
                {
                    trata = new Tratamiento();
                    trata.Id = obj.ObjectId;
                    trata.Fechainicio = (DateTime)obj.UpdatedAt;
                    trata.Fechafin = obj.Get<DateTime>("FechaFin");
                    trata.Fechacontrol = obj.Get<DateTime>("FechaControl");
                    trata.NomTratamiento = obj.Get<string>("Nomtratamiento");
                    trata.Descripcion = obj.Get<string>("Descripcion");
                    if (obj.Get<string>("paciente") == usu.Id)
                    {
                        tratas1.Add(trata);
                    }
                    else if (obj.Get<string>("paciente") == medico.Id)
                    {
                        tratas2.Add(trata);
                    }

                }
                Esperar1.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Esperar1.Visibility = Visibility.Collapsed;
                var dialog = new Windows.UI.Popups.MessageDialog("No ha sido posible cargar la informacion");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("OK") { });
                var result = await dialog.ShowAsync();
            }

        }
        

        private ObservableCollection<Tratamiento> tratas2;

        public ObservableCollection<Tratamiento> Tratas2
        {
            get {
                return tratas2; }
            set { tratas2 = value; }
        }

        private async void asociar(object sender, RoutedEventArgs e)
        {
            var trata = new ParseObject("Tratamiento");
            trata.ObjectId = tratar.Id;
            trata["Descripcion"] = desc.Text;
            trata["FechaFin"] = fini.Date.Date;
            trata["FechaControl"] = ffin.Date.Date;
            
            await trata.SaveAsync();
            //            nomtra.Text = tratar.Descripcion;
        }

        private void SeleccionarTrataAsociado(object sender, SelectionChangedEventArgs e)
        {
            
            tratar = ((sender as ListBox).SelectedItem as Tratamiento);
        }

        private void Asociar1(object sender, SelectionChangedEventArgs e)
        {
            tratar = ((sender as ListBox).SelectedItem as Tratamiento);
        }

        private async void Asoci(object sender, RoutedEventArgs e)
        {
            var trata = new ParseObject("Tratamiento");

            trata["Nomtratamiento"] = nomtra1.Text;
            trata["Descripcion"] = desc1.Text;
            trata["FechaFin"] = fini1.Date.Date;
            trata["FechaControl"] = ffin1.Date.Date;
            trata["MedicoId"] = medico.Id;
            trata["paciente"] = usu.Id;
            Tratamiento tratas = new Tratamiento();
            
            await trata.SaveAsync();
            tratas.Id = trata.ObjectId;
            tratas.NomTratamiento = nomtra1.Text;
            tratas.Descripcion = desc1.Text;
            tratas.Fechainicio = fini1.Date.Date;
            tratas.Fechacontrol= ffin1.Date.Date;
            tratas1.Add(tratas);
        }

        private async void borrarTratamientodePaci(object sender, RoutedEventArgs e)
        {
            try { 
            var trata = new ParseObject("Tratamiento");
            trata.ObjectId = tratar.Id;
            await trata.DeleteAsync();
            tratas1.Remove(tratar);
            }catch(Exception ex)
            {

            }
            
        }

        private async void BorrarPacTra(object sender, RoutedEventArgs e)
        {
            Esperar.Visibility = Visibility.Visible;
            try {
                var query = from UsuarioSelected in ParseObject.GetQuery("MedPac")
                            where UsuarioSelected.Get<string>("Medico") == medico.Id
                            where UsuarioSelected.Get<string>("Paciente") == usu.Id
                            select UsuarioSelected;
                ParseObject trata = await query.FirstAsync();
                await trata.DeleteAsync();
                Medico.data.Remove(usu);
                Medico.data1.Add(usu);
                for (int i=0; i<tratas1.Count; i++)
            {
                var trata1 = new ParseObject("Tratamiento");
                trata1.ObjectId = tratas1.ElementAt(i).Id;
                await trata1.DeleteAsync();
            }
                Esperar.Visibility = Visibility.Collapsed;
                rootFrame.GoBack();
            }
            catch (Exception ex)
            {
                Esperar.Visibility = Visibility.Collapsed;
                var dialog = new Windows.UI.Popups.MessageDialog("Ha ocurrido un error, no se ha borrado este paciente");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("OK") { });
                var result = await dialog.ShowAsync();
            }
        }
    }
}
