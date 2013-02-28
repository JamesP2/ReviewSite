using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review_Site.Data
{
    public interface IEntity
    {
        Guid ID { get; set; }
    }
}
