using Common.Cache;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DataAccess.Repositorios
{
    public class MasterRepository : Repository
    {
        protected List<SqlParameter> parameters;

        protected bool ExecuteNonQuery(string transactSql)
        {
            bool log = false;

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = transactSql;
                        command.CommandType = CommandType.Text;
                        foreach (SqlParameter item in parameters)
                        {
                            command.Parameters.Add(item);
                        }
                        if (command.ExecuteNonQuery() != 0)
                        {
                            log =  true;
                        }
                        else
                            log =  false;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

                return log;
            }
        }

        //INSERTAMOS AL USUARIO DENTRO DE LA BASE DE DATOS
        byte[] Picture = null;
        protected bool AddUser(Usuario Entity, PictureBox FPerfil)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "ADDUSER";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@USERACCOUNT", Entity.UserAccount);
                        command.Parameters.AddWithValue("@PASSWORD", Entity.Password);
                        command.Parameters.AddWithValue("@CEDULA", Entity.Cedula);
                        /*******************************************************************/
                        MemoryStream ms = new MemoryStream();
                        FPerfil.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        Picture = ms.ToArray();
                        command.Parameters.AddWithValue("@PROFILEPICTURE", Picture);
                        /*******************************************************************/
                        command.Parameters.AddWithValue("@NOMBRE", Entity.Nombre);
                        command.Parameters.AddWithValue("@APELLIDOS", Entity.Apellidos);
                        Console.WriteLine(Entity.Apellidos);
                        command.Parameters.AddWithValue("@SEXO", Entity.Sexo);
                        command.Parameters.AddWithValue("@EMAIL", Entity.Email);
                        command.Parameters.AddWithValue("@TELEFONO", Entity.Telefono);
                        command.Parameters.AddWithValue("@AREALAB", Entity.AreaLaboral);

                        command.ExecuteNonQuery();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        return false;
                    };

                }
            }
        }

        //ACTUALIZAMOS LA FOTO DE PERFIL DEL USUARIO
        protected bool UpdatePictureProfile(string User, PictureBox FPerfil)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "UPDPROFILE";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@USERACCOUNT", User);
                        /*******************************************************************/
                        MemoryStream ms = new MemoryStream();
                        FPerfil.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        Picture = ms.ToArray();
                        command.Parameters.AddWithValue("@PROFILEPICTURE", Picture);
                        /*******************************************************************/

                        command.ExecuteNonQuery();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        return false;
                    };

                }
            }
        }

        //VERIFICAMOS SI EXISTE O NO EL USUARIO
        protected bool Login(string User, string Pass)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM USUARIO WHERE USUARIO.USERACCOUNT = @User and USUARIO.PASSWORD = @Pass";
                    command.Parameters.AddWithValue("@User", User);
                    command.Parameters.AddWithValue("@Pass", Pass);
                    command.CommandType = CommandType.Text;
                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            UserLoginCache.UserId = Reader.GetString(0);
                            UserLoginCache.Cedula = Reader.GetString(3);

                            /********************************************/
                            var Picture = (byte[])Reader["PROFILEPICTURE"];
                            MemoryStream ms = new MemoryStream(Picture);
                            PictureBox pictureBox = new PictureBox();
                            pictureBox.Image = Bitmap.FromStream(ms);
                            UserLoginCache.Picture = pictureBox;
                            /********************************************/
                            UserLoginCache.Nombre = Reader.GetString(4);
                            UserLoginCache.Apellidos = Reader.GetString(5);
                            UserLoginCache.Sexo = Reader.GetString(6);
                            UserLoginCache.Email = Reader.GetString(7);
                            UserLoginCache.Telefono = Reader.GetString(8);
                            UserLoginCache.AreaLab = Reader.GetString(9);
                        }
                        return true;
                    }
                    else
                        return false;
                }
            }
        }

        protected List<Carrera> ItemsCarrera = new List<Carrera>();
        //TRAEMOS LAS CARRERAS DE LA BASE DE DATOS
        protected List<Carrera> Carreras()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM CARRERAS ORDER BY CARRERAS.NOMBRE ASC";
                    command.CommandType = CommandType.Text;
                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            Carrera Carrera = new Carrera();

                            Carrera.NombreCarrera = Reader["NOMBRE"].ToString();
                            ItemsCarrera.Add(Carrera);
                        }

                        return ItemsCarrera;

                    }
                    else
                    {
                        return ItemsCarrera;
                    }

                        
                }
            }
        }


        //TRAEMOS EL LISTADO DE SERVICIOS DISPONIBLES
        protected List<Servicio> ItemServicio = new List<Servicio>();
        protected List<Servicio> servicios()
        {
            using (var Connection = GetConnection())
            {
                Connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = Connection;
                    command.CommandText = "SELECT * FROM SERVICIO";
                    command.CommandType = CommandType.Text;
                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            Servicio servicio = new Servicio();

                            servicio.Id_Servicio = Reader.GetString(0);
                            servicio.User = Reader.GetString(1);
                            servicio.Fecha = Reader.GetString(2);
                            servicio.AreaBrin = Reader.GetString(3);
                            servicio.NombreBrin = Reader.GetString(4);
                            servicio.Description1 = Reader.GetString(5);
                            servicio.AreaBusq = Reader.GetString(6);
                            servicio.NombreBusq = Reader.GetString(7);
                            servicio.Description2 = Reader.GetString(8);
                            servicio.ValorPromedio = Reader.GetDouble(9);

                            ItemServicio.Add(servicio);
                        }
                        return ItemServicio;
                    }
                    else
                        return ItemServicio;
                }
            }
        }

        //CONSULTAMOS EL SERVICIO POR MEDIO DE UN ID_SERVICIO
        protected Servicio ConsultService(string Id_Servicio)
        {
            Servicio servicio = new Servicio();

            using (var Connection = GetConnection())
            {
                Connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = Connection;
                    command.CommandText = "SELECT * FROM SERVICIO WHERE Id_Servicio = @Id_Servicio";
                    command.Parameters.AddWithValue("@Id_Servicio",Id_Servicio);
                    command.CommandType = CommandType.Text;
                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            servicio.Id_Servicio = Reader.GetString(0);
                            servicio.User = Reader.GetString(1);
                            servicio.Fecha = Reader.GetString(2);
                            servicio.AreaBrin = Reader.GetString(3);
                            servicio.NombreBrin = Reader.GetString(4);
                            servicio.Description1 = Reader.GetString(5);
                            servicio.AreaBusq = Reader.GetString(6);
                            servicio.NombreBusq = Reader.GetString(7);
                            servicio.Description2 = Reader.GetString(8);
                            servicio.ValorPromedio = Reader.GetDouble(9);

                            ItemServicio.Add(servicio);
                        }                        
                    }
                    return servicio;
                }
            }
        }

        //CONSULTAMOS A UN USUARIO POR MEDIO DE UN ID_USUARIO
        protected bool UserConsult(string User)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM USUARIO WHERE USERACCOUNT = @User";
                    command.Parameters.AddWithValue("@User", User);
                    command.CommandType = CommandType.Text;
                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            Consulta.UserId = Reader.GetString(0);
                            Consulta.Cedula = Reader.GetString(3);

                            /********************************************/
                            var Picture = (byte[])Reader["PROFILEPICTURE"];
                            MemoryStream ms = new MemoryStream(Picture);
                            PictureBox pictureBox = new PictureBox();
                            pictureBox.Image = Bitmap.FromStream(ms);
                            Consulta.Picture = pictureBox;
                            /********************************************/

                            Consulta.Nombre = Reader.GetString(4);
                            Consulta.Apellidos = Reader.GetString(5);
                            Consulta.Sexo = Reader.GetString(6);
                            Consulta.Email = Reader.GetString(7);
                            Consulta.Telefono = Reader.GetString(8);
                            Consulta.AreaLab = Reader.GetString(9);
                        }
                        return true;
                    }else
                        return false;                 
                }
            }
        }

        //ELIMINAMOS EL SERVICIO
        protected bool EliminarService(string ID_SERVICIO)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM SERVICIO WHERE ID_SERVICIO = @ID_SERVICIO";
                    command.Parameters.AddWithValue("@ID_SERVICIO", ID_SERVICIO);
                    command.CommandType = CommandType.Text;
                    
                    if(command.ExecuteNonQuery() != 0) 
                    { 
                        return true;
                    }
                    else
                        return false;
                }
            }
        }

        //ACTUALIZAMOS EL SERVICIO
        protected bool UpService(Servicio Upservicio)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPSERVICE";
                    command.Parameters.AddWithValue("@ID_SERVICIO", Upservicio.Id_Servicio);
                    command.Parameters.AddWithValue("@NOMBRE_BRIN", Upservicio.NombreBrin);
                    command.Parameters.AddWithValue("@DESCRIPCION1", Upservicio.Description1);
                    command.Parameters.AddWithValue("@AREA_BUSQ", Upservicio.AreaBusq);
                    command.Parameters.AddWithValue("@NOMBRE_BUSQ", Upservicio.NombreBusq);
                    command.Parameters.AddWithValue("@DESCRIPCION2", Upservicio.Description2);
                    command.Parameters.AddWithValue("@VALORPROME", Upservicio.ValorPromedio);
                    command.CommandType = CommandType.StoredProcedure;

                    if (command.ExecuteNonQuery() != 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
        }
    }
}
