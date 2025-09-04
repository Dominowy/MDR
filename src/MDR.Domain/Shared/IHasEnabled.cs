using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDR.Domain
{
    public interface IHasEnabled
    {
        bool Enabled { get; }
    }
}
