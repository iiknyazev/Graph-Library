using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsLibrary
{
    public static class NodeExtensions
    {
        public static IEnumerable<Node> DepthSearch(this Node startNode)
        {
            // Внимание! Перед использованием этого кода, прочитайте следующий слайд «Использование памяти»
            var visited = new HashSet<Node>();
            var stack = new Stack<Node>();
            visited.Add(startNode);
            stack.Push(startNode);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                yield return node;
                foreach (var nextNode in node.IncidentNodes.Where(n => !visited.Contains(n)))
                {
                    visited.Add(nextNode);
                    stack.Push(nextNode);
                }
            }
        }

        public static IEnumerable<Node> BreadthSearch(this Node startNode)
        {
            // Внимание! Перед использованием этого кода, прочитайте следующий слайд «Использование памяти»
            var visited = new HashSet<Node>();
            var queue = new Queue<Node>();
            visited.Add(startNode);
            queue.Enqueue(startNode);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                yield return node;
                foreach (var nextNode in node.IncidentNodes.Where(n => !visited.Contains(n)))
                {
                    visited.Add(nextNode);
                    queue.Enqueue(nextNode);
                }
            }
        }

        public static List<Node> FindPath(this Node startNode, Node endNode)
        {
            var track = new Dictionary<Node, Node>();
            track[startNode] = null;
            var queue = new Queue<Node>();
            queue.Enqueue(startNode);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                foreach (var nextNode in node.IncidentNodes)
                {
                    if (track.ContainsKey(nextNode)) continue;
                    track[nextNode] = node;
                    queue.Enqueue(nextNode);
                }
                if (track.ContainsKey(endNode)) break;
            }
            var pathItem = endNode;
            var result = new List<Node>();
            while (pathItem != null)
            {
                result.Add(pathItem);
                pathItem = track[pathItem];
            }
            result.Reverse();
            return result;
        }
    }
}
