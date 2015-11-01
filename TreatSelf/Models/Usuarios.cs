using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreatSelf.Models
{
    public class Usuarios
    {
        public static ObservableCollection<Usuario> data;

        public ObservableCollection<Usuario> Data
        {
            get {
                if(data == null)
                {
                    data = new ObservableCollection<Usuario>();
                    Usuario usu1 = new Usuario() { Apellido = "Guzman", Nombre = "David", Cedula = 06102038, Correo = "davidguzman@unicauca.edu.co", Telefono = 3184105690};
                    Usuario usu2 = new Usuario() { Apellido = "Guzman", Nombre = "Karen", Cedula = 8302615, Correo = "karenguzman@unicauca.edu.co", Telefono = 31231232 };
                    Usuario usu3 = new Usuario() { Apellido = "Delgado", Nombre = "Fanny", Cedula = 3631212, Correo = "fannydelgado@unicauca.edu.co", Telefono = 312312312 };
                    data.Add(usu1);
                    data.Add(usu2);
                    data.Add(usu3);


                }
                return data; }
            set { data = value;
                  
            }
        }

        public static ObservableCollection<Usuario> data1;

        public ObservableCollection<Usuario> Data1
        {
            get {
                if (data1 == null)
                {
                    data1 = new ObservableCollection<Usuario>();
                    Usuario usu1 = new Usuario() { Apellido = "Guzman", Nombre = "Paola", Cedula = 5634534, Correo = "paolaguzman@unicauca.edu.co", Telefono = 645664654 };
                    Usuario usu2 = new Usuario() { Apellido = "Delgado", Nombre = "Isabela", Cedula = 543543, Correo = "isadelgado@unicauca.edu.co", Telefono = 654654546 };
                    Usuario usu3 = new Usuario() { Apellido = "Guzman", Nombre = "Oscar", Cedula = 432432, Correo = "oscarguzman@unicauca.edu.co", Telefono = 86876867 };
                    data1.Add(usu1);
                    data1.Add(usu2);
                    data1.Add(usu3);


                }

                return data1; }
            set { data1 = value; }
        }




    }
}
