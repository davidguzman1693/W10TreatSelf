using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreatSelf.Models
{
    public class Tratamiento: INotifyPropertyChanged
    {

        private String medico;

        public String Medico
        {
            get { return medico; }
            set { medico = value; }
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


        private String nomTratamiento;

        public String NomTratamiento
        {
            get { return nomTratamiento; }
            set { nomTratamiento = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NomTratamiento"));
            }
        }

        private String descripción;

        public String Descripcion
        {
            get { return descripción; }
            set { descripción = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Descripcion"));
            }
        }

        private DateTime fechainicio;

        public DateTime Fechainicio
        {
            get { return fechainicio; }
            set { fechainicio = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Fechainicio"));
            }
        }

        private DateTime fechafin;  

        public DateTime Fechafin
        {
            get { return fechafin; }
            set { fechafin = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Fechafin"));
            }
        }

        private DateTime fechacontrol;

        public DateTime Fechacontrol
        {
            get { return fechacontrol; }
            set { fechacontrol = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Fechacontrol"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
