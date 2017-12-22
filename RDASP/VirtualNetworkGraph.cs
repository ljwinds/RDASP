using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDASP
{
    class VirtualNetworkGraph
    {
        public Dictionary<string, VirtualNode> VirtualNodeDict;
        public Dictionary<string, VirtualNode> SortedVirtualNodeDict;

        public VirtualNetworkGraph()
        {
            VirtualNodeDict = new Dictionary<string, VirtualNode>();
            SortedVirtualNodeDict = new Dictionary<string, VirtualNode>();
        }

        public void CalculateAllRC()
        {
            foreach (var item in VirtualNodeDict)
            {
                item.Value.CalculateRC();
            }
        }

        public void CalculateAllSAR()
        {
            foreach (var item in VirtualNodeDict)
            {
                item.Value.CalculateSAR(VirtualNodeDict);
            }
        }

        public void SortNodeBySAR()
        {
            SortedVirtualNodeDict =
                VirtualNodeDict.OrderByDescending(o => o.Value.SAR).ToDictionary(p => p.Key, o => o.Value);
        }
    }
}
