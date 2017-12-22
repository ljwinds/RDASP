using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RDASP
{
    enum JsonStringType : byte
    {
        Virtual,
        Physical,
    };

    class RDASPAlgorithm
    {
        public VirtualNetworkGraph VNG { get; set; }
        public PhysicalNetworkGraph PNG { get; set; }
        public bool EnableDebugInfo { get; set; }
        public List<string> DeployedVirtualNodeIDList { get; set; }

        public RDASPAlgorithm()
        {
            VNG = new VirtualNetworkGraph();
            PNG = new PhysicalNetworkGraph();
            DeployedVirtualNodeIDList = new List<string>();
        }

        public void ParseInput(string json_str, JsonStringType json_str_type)
        {
            JObject json_obj = JObject.Parse(json_str);
            JObject input = (JObject)json_obj["input"];

            JArray nodes = (JArray)input["nodes"];
            JArray links = (JArray)input["links"];

            foreach (JObject node in nodes)
            {
                string id = node["id"].ToString();
                int vCPU = int.Parse(node["vCPU"].ToString());

                if (json_str_type == JsonStringType.Virtual)
                {
                    VNG.VirtualNodeDict.Add(id, new VirtualNode(id, vCPU));
                }
                else
                {
                    PNG.PhysicalNodeDict.Add(id, new PhysicalNode(id, vCPU));
                }
            }

            foreach (JObject link in links)
            {
                string id = link["id"].ToString();
                string start_node = link["start_node"].ToString();
                string end_node = link["end_node"].ToString();
                double bandwidth = double.Parse(link["bandwidth"].ToString());

                if (json_str_type == JsonStringType.Virtual)
                {
                    VNG.VirtualNodeDict[start_node].AdjLinkSet.Add(new VirtualLink(id, bandwidth, start_node, end_node));
                    VNG.VirtualNodeDict[end_node].AdjLinkSet.Add(new VirtualLink(id, bandwidth, start_node, end_node));
                }
                else
                {
                    PNG.PhysicalNodeDict[start_node].AdjLinkSet.Add(new PhysicalLink(id, bandwidth, start_node, end_node));
                    PNG.PhysicalNodeDict[end_node].AdjLinkSet.Add(new PhysicalLink(id, bandwidth, start_node, end_node));
                }
            }

        }

        public string ServicePlacement(string virtual_json_str, string physical_json_str)
        {
            ParseInput(virtual_json_str, JsonStringType.Virtual);
            ParseInput(physical_json_str, JsonStringType.Physical);

            VNG.CalculateAllRC();
            VNG.CalculateAllSAR();
            VNG.SortNodeBySAR();

            OutputMessage output = new OutputMessage();
            while (VNG.SortedVirtualNodeDict.Any())
            {
                PNG.CalculateAllRC();

                VirtualNode vn = VNG.SortedVirtualNodeDict.Values.First();
                PhysicalNode pn = PNG.GetMaxRCNode();

                if (vn.RC <= pn.RC)
                {
                    DeployVirtualNode(vn, pn);
                    output.NodeMap.Add(vn, pn);
                    VNG.SortedVirtualNodeDict.Remove(vn.ID);
                }
                else
                {
                    output.State = "failed";
                    output.NodeMap = null;
                    break;
                }
            }
            if (output.State != "failed")
            {
                output.State = "successful";
            }
            return EncapsulateOutput(output);
        }

        public string EncapsulateOutput(OutputMessage output)
        {
            return JsonConvert.SerializeObject(output);
        }

        public void DeployVirtualNode(VirtualNode vn, PhysicalNode pn)
        {

        }

    }
}
