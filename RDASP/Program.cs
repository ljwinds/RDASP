using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RDASP
{
    class OutputMessage
    {
        public string State { get; set; }
        public string Message { get; set; }
        public Dictionary<VirtualNode, PhysicalNode> NodeMap;

        public OutputMessage()
        {
            NodeMap = new Dictionary<VirtualNode, PhysicalNode>();
        }
    }

    class FileProcess
    {
        public string ReadFileToString(string path)
        {
            return File.ReadAllText(path);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input parameters: ");
            string input = Console.ReadLine();
            string[] input.Split()
            FileProcess fp = new FileProcess();
            string path_virtual = @"C:\Users\Legion\Desktop\毕业相关\RDASP-input-virtual.json";
            string path_physical = @"C:\Users\Legion\Desktop\毕业相关\RDASP-input-physical.json";

            string virtual_json_str = fp.ReadFileToString(path_virtual);
            string physical_json_str = fp.ReadFileToString(path_physical);

            RDASPAlgorithm ra = new RDASPAlgorithm();
            ra.ServicePlacement(virtual_json_str, physical_json_str);
        }
    }
}
