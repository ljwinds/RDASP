using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDASP
{
    class PhysicalNetworkGraph
    {
        public Dictionary<string, PhysicalNode> PhysicalNodeDict;

        public PhysicalNetworkGraph()
        {
            PhysicalNodeDict = new Dictionary<string, PhysicalNode>();
        }

        public void CalculateAllRC()
        {
            foreach (var item in PhysicalNodeDict)
            {
                item.Value.CalculateRC();
            }
        }

        public PhysicalNode GetMaxRCNode()
        {
            double max_rc = 0.0;
            PhysicalNode pn = null;
            foreach (var item in PhysicalNodeDict)
            {
                if (item.Value.RC > max_rc)
                {
                    max_rc = item.Value.RC;
                    pn = item.Value;
                }
            }
            return pn;
        }
    }
}
