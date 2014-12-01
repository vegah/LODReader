using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasista.Archive.LOD.Exception
{
    public class IllegalHeaderException:System.Exception
    {
        public IllegalHeaderException()
            : base("The file headers are wrong.  This is probably not a LOD file.")
        {

        }
    }
}
