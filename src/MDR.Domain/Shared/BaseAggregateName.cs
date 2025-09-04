using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDR.Domain
{
    public abstract class BaseAggregateName : Aggregate, IHasName
    {
        public string Name { get; private set; }

        protected BaseAggregateName() : base()
        {

        }

        protected BaseAggregateName(Guid id) : base(id)
        {

        }

        public void SetName(string name)
        {
            var anyChange = Name != name;
            if (!anyChange) return;
            Name = name;
        }
    }
}
