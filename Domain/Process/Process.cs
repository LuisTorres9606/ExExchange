using System.Collections.Generic;
using Domain.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Common.Cache;
using System.Windows.Forms;

namespace Domain.Process
{
    public class Process
    {
        QueriesRepository Us = new QueriesRepository();

        public bool InsertarUsuario(UsuarioModel model, PictureBox picture)
        {
            var User = new Usuario();

            User.UserAccount = model._UserAccount;
            User.Password = model._Password;
            User.Cedula = model._Cedula;
            User.Nombre = model._Nombre;
            User.Apellidos = model._Apellidos;
            User.Sexo = model._Sexo;
            User.Email = model._Email;
            User.Telefono = model._Telefono;
            User.AreaLaboral = model._AreaLaboral;

            if (Us.Add(User, picture))
            {
                return true;
            }
            else
                return false;
        }

        public bool InsertarServicio(ServicioModel model)
        {
            var Servicio = new Servicio();

            Servicio.Id_Servicio = model.Id_servicio;
            Servicio.User = model.User;
            Servicio.Fecha = model.Fecha;
            Servicio.AreaBrin = model.Area_Bri;
            Servicio.NombreBrin = model.Nombre_Brin;
            Servicio.Description1 = model.Description1;
            Servicio.AreaBusq = model.Area_Busq;
            Servicio.NombreBusq = model.Nombre_Busq;
            Servicio.Description2 = model.Description2;
            Servicio.ValorPromedio = model.ValorPromedio;

            if (Us.AddService(Servicio))
            {
                return true;
            }
            else
                return false;
        }

        public bool UpServicio(ServicioModel model)
        {
            var Servicio = new Servicio();

            Servicio.Id_Servicio = model.Id_servicio;
            Servicio.NombreBrin = model.Nombre_Brin;
            Servicio.Description1 = model.Description1;
            Servicio.AreaBusq = model.Area_Busq;
            Servicio.NombreBusq = model.Nombre_Busq;
            Servicio.Description2 = model.Description2;
            Servicio.ValorPromedio = model.ValorPromedio;

            if (Us.UpServicio(Servicio))
            {
                return true;
            }
            else
                return false;
        }

        public bool LoginUser(string UserAccount, string Password)
        {
            return Us.Consult(UserAccount, Password);
        }

        public bool UpdateProfilePicture(string User, PictureBox Picture)
        {
            return Us.UpdatePicture(User, Picture);
        }

        public bool UserConsult(string User)
        {
            return Us.ConsultaUsuario(User);
        }

        public List<Carrera>Carreras()
        {
            return Us.Carrera();
        }

        public List<Servicio> Servicios()
        {
            return Us.Servicio();
        }

        public Servicio ConsultServicio(string Id_Servicio)
        {
            return Us.ConsultarServicio(Id_Servicio);
        }        

        public bool EliminarService(string ID_SERVICIO)
        {
            return Us.EliminarServicio(ID_SERVICIO);
        }

    }
}
