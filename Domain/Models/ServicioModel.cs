using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ServicioModel
    {
        public string Id_servicio { get; set; }
        public string User { get; set; }
        public string Fecha { get; set; }
        public string Area_Bri { get; set; }
        public string Nombre_Brin { get; set; }
        public string Description1 { get; set; }
        public string Area_Busq { get; set; }
        public string Nombre_Busq { get; set; }
        public string Description2 { get; set; }
        public double ValorPromedio { get; set; }
    }
}
