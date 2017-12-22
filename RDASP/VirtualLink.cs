using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDASP
{
    class VirtualLink : Link
    {
        public VirtualLink(string id, double bandwidth, string start_node, string end_node) 
            : base(id, bandwidth, start_node, end_node) { }
    }
}
