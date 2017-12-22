using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDASP
{
    class Node
    {
        public string ID { get; set; }
        public int vCPU { get; set; }

        public Node() { }
        public Node(string id) : this(id, 1) { }
        public Node(string id, int vcpu)
        {
            ID = id;
            vCPU = vcpu;
        }

        public virtual void CalculateRC() { }
    }
}
