using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreatSelf.Models
{
    public class MiNotificacion : INotifyPropertyChanged
    {

        private String paciente;

        public String Paciente
        {
            get { return paciente; }
            set { paciente = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Paciente"));
            }
        }

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
            set { nombre = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Nombre"));
            }
        }

        private String descripcion;

        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Descripcion"));
            }
        }

        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Fecha"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;


    }
}
