using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccess.Entities
{
    public class Usuario
    {
        public string UserAccount { get; set; }
        public string Password { get; set; }
        public string Cedula { get; set; }
        public static PictureBox Picture { get; set; }
        public string Nombre { get; set; }
        public string Apellidos{ get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string AreaLaboral { get; set; }
    }
}
