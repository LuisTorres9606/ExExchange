using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositorios;
using Common.Cache;
using System.Windows.Forms;
using System.Data;

namespace DataAccess.Repositories
{
    public class QueriesRepository : MasterRepository, IUserRepository
    {
        private string InsertService;
        private string Update;
        private string Delete;

        public QueriesRepository()
        {
            InsertService = "INSERT INTO SERVICIO VALUES (@ID_SERVICIO,@USERACCOUNT,@FECHA,@AREA_BRIN,@NOMBRE_BRIN,@DESCRIPCION1,@AREA_BUSQ,@NOMBRE_BUSQ,@DESCRIPCION2,@VALORPROME)";
            Update = "UPDATE LOGIN SET PASSWORD = @PASSWORD,CEDULA = @CEDULA,,NOMBRE = @NOMBRE,APELLIDOS = @APELLIDOS,SEXO = @SEXO,EMAIL = @EMAIL,TELEFONO = @TELEFONO, AREALAB = @AREALAB WHERE ACCOUNTUSER = @ACCOUNTUSER";
            Delete = "DELETE FROM LOGIN WHERE ACCOUNTUSER = @ACCOUNTUSER";
        }

        public bool Edit(Usuario entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ACCOUNTUSER", entity.UserAccount));
            parameters.Add(new SqlParameter("@PASSWORD", entity.Password));
            parameters.Add(new SqlParameter("@CEDULA", entity.Cedula));
            parameters.Add(new SqlParameter("@NOMBRE", entity.Nombre));
            parameters.Add(new SqlParameter("@APELLIDOS", entity.Apellidos));
            parameters.Add(new SqlParameter("@SEXO", entity.Sexo));
            parameters.Add(new SqlParameter("@EMAIL", entity.Email));
            parameters.Add(new SqlParameter("@TELEFONO", entity.Telefono));
            parameters.Add(new SqlParameter("@AREALAB", entity.AreaLaboral));
            return ExecuteNonQuery(Update);
        }

        public bool AddService(Servicio servicio)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID_SERVICIO", servicio.Id_Servicio));
            parameters.Add(new SqlParameter("@USERACCOUNT", servicio.User));
            parameters.Add(new SqlParameter("@FECHA", servicio.Fecha));
            parameters.Add(new SqlParameter("@AREA_BRIN", servicio.AreaBrin));
            parameters.Add(new SqlParameter("@NOMBRE_BRIN", servicio.NombreBrin));
            parameters.Add(new SqlParameter("@DESCRIPCION1", servicio.Description1));
            parameters.Add(new SqlParameter("@AREA_BUSQ", servicio.AreaBusq));
            parameters.Add(new SqlParameter("@NOMBRE_BUSQ", servicio.NombreBusq));
            parameters.Add(new SqlParameter("@DESCRIPCION2", servicio.Description2));
            parameters.Add(new SqlParameter("@VALORPROME", servicio.ValorPromedio));
            return ExecuteNonQuery(InsertService);
        }

        public List<Carrera> Carrera()
        {
            return Carreras();
        }

        public bool Add(Usuario entity,PictureBox Picture)
        {
            return AddUser(entity,Picture);
        }

        public bool UpdatePicture(string User, PictureBox Picture)
        {
            return UpdatePictureProfile(User, Picture);
        }

        public bool Consult(string AccountUser, string Password)
        {
            return Login(AccountUser, Password);
        }

        public bool ConsultaUsuario(string User)
        {
            return UserConsult(User);
        }

        public bool Remove(string UserAccount)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ACCOUNTUSER", UserAccount));
            return ExecuteNonQuery(Delete);
        }

        public List<Servicio> Servicio()
        {
            return servicios();
        }

        public Servicio ConsultarServicio(string Id_Servicio)
        {
            return ConsultService(Id_Servicio);
        }

        public bool UpServicio(Servicio Servicio)
        {
            return UpService(Servicio);
        } 

        public bool EliminarServicio(string ID_SERVICIO)
        {
            return EliminarService(ID_SERVICIO);
        }
    }
}
