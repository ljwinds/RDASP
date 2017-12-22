using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDASP
{
    class PhysicalNode : Node
    {
        public double RC { get; set; }
        public HashSet<PhysicalLink> AdjLinkSet;

        public PhysicalNode(string id, int vCPU) : base(id, vCPU)
        {
            AdjLinkSet = new HashSet<PhysicalLink>();
        }

        public override void CalculateRC()
        {
            double sum_bandwidth = 0.0;
            foreach (PhysicalLink pl in AdjLinkSet)
            {
                sum_bandwidth += pl.Bandwidth;
            }
            RC = vCPU * sum_bandwidth;
        }
    }
}
