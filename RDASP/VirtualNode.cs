using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDASP
{
    class VirtualNode : PhysicalNode
    {
        public double SAR { get; set; }
        public new HashSet<VirtualLink> AdjLinkSet;

        public VirtualNode(string id, int vCPU) : base(id, vCPU)
        {
            AdjLinkSet = new HashSet<VirtualLink>();
        }

        public override void CalculateRC()
        {
            double sum_bandwidth = 0.0;
            foreach (VirtualLink vl in AdjLinkSet)
            {
                sum_bandwidth += vl.Bandwidth;
            }
            RC = vCPU * sum_bandwidth;
        }

        public void CalculateSAR(Dictionary<string, VirtualNode> vnd)
        {
            SAR = RC;
            foreach (VirtualLink vl in AdjLinkSet)
            {
                string id = vl.StartNode;
                if (id == ID) id = vl.EndNode;

                SAR += vnd[id].RC;
            }
        }
    }
}
