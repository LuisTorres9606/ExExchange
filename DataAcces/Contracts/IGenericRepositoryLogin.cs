using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Cache;

namespace DataAccess.Contracts
{
    public interface IGenericRepositoryLogin<Entity> where Entity:class
    {
        bool Add(Entity entity,PictureBox Picture);
        bool AddService(Servicio servicio);
        bool Edit(Entity entity);
        bool Remove(string AccountUser);
        bool Consult(string AccountUser, string Password);

    }
}
