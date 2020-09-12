using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountryGWP.DataAccess.Layer.Repositories.Interfaces
{
    public interface ICrudRepository<Entity>
    {
        Task<IEnumerable<Entity>> GetAsync();
    }
}
