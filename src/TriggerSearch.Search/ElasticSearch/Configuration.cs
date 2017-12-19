using System;
using System.Collections.Generic;
using System.Text;

namespace TriggerSearch.Search.ElasticSearch
{
    public class Configuration
    {
        public string[] Nodes { get; set; }
        public Uri[] NodesUri
        {
            get{
                if(Nodes.Length >0)
                {
                    Uri[] uris = new Uri[Nodes.Length];
                    for (int index = 0; index < Nodes.Length; index++)
                    {
                        uris[index] = new Uri(Nodes[index]);
                    }
                    return uris;
                }
                return null;
            }
        }
        public string DefaultIndex { get; set; }
    }
}
