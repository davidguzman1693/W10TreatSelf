using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreatSelf.Models
{
    public class Usuario : INotifyPropertyChanged
    {

        private String id;

        public String Id
        {
            get { return id; }
            set { id = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }


        private String nombre;

        public String Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Nombre"));
            }
        }
        private String apellido;

        public String Apellido
        {
            get { return apellido; }
            set { apellido = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Apellido"));
            }
        }

        private String cedula;

        public String Cedula
        {
            get { return cedula; }
            set { cedula = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Cedula"));
            }
        }

        private String correo;

        public String Correo
        {
            get { return correo; }
            set { correo = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Correo"));
            }
        }

        private uint telefono;

        public uint Telefono
        {
            get { return telefono; }
            set { telefono = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Telefono"));
            }
        }


        private String perfil;

        public String Perfil
        {
            get { return perfil; }
            set { perfil = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Perfil"));
            }
        }

        private String username;

        public String Username
        {
            get { return username; }
            set { username = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }
        }

        private String password;

        public String Password
        {
            get { return password; }
            set { password = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        private String medPac;

        public String MedPac
        {
            get { return medPac; }
            set { medPac = value;

            }
        }





        public event PropertyChangedEventHandler PropertyChanged;
    }
}
