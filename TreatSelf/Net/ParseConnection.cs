using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreatSelf.Models;

namespace TreatSelf.Net
{
    public class ParseConnection
    {
        public async void register(Usuario usu)
        {
            ParseObject appointment = new ParseObject("User");
            appointment["username"] = usu.Username;
            appointment["password"] = usu.Password;
            appointment["Nombre"] = usu.Nombre;
            appointment["Apellido"] = usu.Apellido;
            appointment["Cedula"] = usu.Cedula;
            appointment["telefono"] = usu.Telefono;
            appointment["email"] = usu.Correo;
            appointment["perfil"] = usu.Perfil;
             
            await appointment.SaveAsync();
        }

    }
}
