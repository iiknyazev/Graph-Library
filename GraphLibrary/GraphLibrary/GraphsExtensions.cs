using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsLibrary
{
    public static class GraphExtensions
    {
        public static List<List<Node>> FindConnectedComponents(this Graph graph)
        {
            var result = new List<List<Node>>();
            var markedNodes = new HashSet<Node>();
            while (true)
            {
                var nextNode = graph.Nodes.Where(node => !markedNodes.Contains(node)).FirstOrDefault();
                if (nextNode == null) break;
                var breadthSearch = nextNode.BreadthSearch().ToList(); ;
                result.Add(breadthSearch.ToList());
                foreach (var node in breadthSearch)
                    markedNodes.Add(node);
            }
            return result;
        }

        public static List<Node> KahnAlgorithm(this Graph graph)
        {
            var topSort = new List<Node>();
            var nodes = graph.Nodes.ToList();
            while (nodes.Count != 0)
            {
                var nodeToDelete = nodes
                    .Where(node => !node.IncidentEdges.Any(edge => edge.To == node))
                    .FirstOrDefault();

                if (nodeToDelete == null) return null;
                nodes.Remove(nodeToDelete);
                topSort.Add(nodeToDelete);
                foreach (var edge in nodeToDelete.IncidentEdges.ToList())
                    graph.Delete(edge);
            }
            return topSort;
        }

        public enum State
        {
            White,
            Gray,
            Black
        }

        public static List<Node> TarjanAlgorithm(this Graph graph)
        {
            var topSort = new List<Node>();
            var states = graph.Nodes.ToDictionary(node => node, node => State.White);
            while (true)
            {
                var nodeToSearch = states
                    .Where(z => z.Value == State.White)
                    .Select(z => z.Key)
                    .FirstOrDefault();
                if (nodeToSearch == null) break;

                if (!TarjanDepthSearch(nodeToSearch, states, topSort))
                    return null;
            }
            topSort.Reverse();
            return topSort;
        }
        private static bool TarjanDepthSearch(Node node, Dictionary<Node, State> states, List<Node> topSort)
        {
            if (states[node] == State.Gray) return false;
            if (states[node] == State.Black) return true;
            states[node] = State.Gray;

            var outgoingNodes = node.IncidentEdges
                .Where(edge => edge.From == node)
                .Select(edge => edge.To);
            foreach (var nextNode in outgoingNodes)
                if (!TarjanDepthSearch(nextNode, states, topSort)) return false;

            states[node] = State.Black;
            topSort.Add(node);
            return true;
        }
    }
}
