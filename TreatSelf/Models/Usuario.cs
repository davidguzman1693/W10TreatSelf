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

        private uint cedula;

        public uint Cedula
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





        public event PropertyChangedEventHandler PropertyChanged;
    }
}
