using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class ModelBase
    {
        public bool Saved { get; protected set; }
        public void Save()
        {
            Saved = true;
            //self
        }
    }
}
