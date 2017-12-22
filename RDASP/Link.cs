using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDASP
{
    class Link
    {
        public string ID { get; set; }
        public double Bandwidth { get; set; }
        public string StartNode { get; set; }
        public string EndNode { get; set; }

        public Link() { }
        public Link(string id, double bandwidth, string start_node, string end_node)
        {
            ID = id;
            Bandwidth = bandwidth;
            StartNode = start_node;
            EndNode = end_node;
        }
    }
}
