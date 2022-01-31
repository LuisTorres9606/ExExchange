using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;
using System.Data.SqlClient;

namespace Domain.Models
{
    public class UsuarioModel
    {
        private IUserRepository QueriesRepository;

        private string UserAccount;
        private string Password;
        private string Cedula;
        private string Nombre;
        private string Apellidos;
        private string Sexo;
        private string Email;
        private string Telefono;
        private string AreaLaboral;

        public UsuarioModel(string userAccount, string password, string cedula, string nombre, string apellidos, string sexo,string email, string telefono, string areaLaboral)
        {
            UserAccount = userAccount;
            Password = password;
            Cedula = cedula;
            Nombre = nombre;
            Apellidos = apellidos;
            Sexo = sexo;
            Email = email;
            Telefono = telefono;
            AreaLaboral = areaLaboral;
        }


        //PROPIEDAD /MODEO DE VISTA/ VALIDAD DATOS
        public string _UserAccount { get => UserAccount; set => UserAccount = value; }
        public string _Password { get => Password; set => Password = value; }        
        public string _Cedula { get => Cedula; set => Cedula = value; }        
        public string _Nombre { get => Nombre; set => Nombre = value; }       
        public string _Apellidos { get => Apellidos; set => Apellidos = value; }
        public string _Sexo { get => Sexo; set => Sexo = value; }
        public string _Email { get => Email; set => Email = value; }
        public string _Telefono { get => Telefono; set => Telefono = value; }
        public string _AreaLaboral { get => AreaLaboral; set => AreaLaboral = value; }
        
        
        public UsuarioModel()
        {
            QueriesRepository = new QueriesRepository();
        }

        
    }
}
