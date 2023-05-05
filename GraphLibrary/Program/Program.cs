using GraphsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Добавить:
// Алгоритм Дейкстры
// Алгоритм Прима/Краскала 
// Нахождение цикла
// Эйлеров цикл
// Поток минимальной стоимости

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var graph = Graph.MakeGraph(
            0, 1,
            0, 2,
            1, 3,
            1, 4,
            2, 3,
            3, 4);

            Console.WriteLine(graph[0]
                .DepthSearch()
                .Select(z => z.NodeNumber.ToString())
                .Aggregate((a, b) => a + " " + b));

            Console.WriteLine(graph[0]
                .BreadthSearch()
                .Select(z => z.NodeNumber.ToString())
                .Aggregate((a, b) => a + " " + b));

            var connected = graph.FindConnectedComponents();
            Console.WriteLine(connected
                .Select(component => component
                    .Select(node => node.NodeNumber.ToString())
                    .Aggregate((a, b) => a + " " + b))
                .Aggregate((a, b) => a + "\n" + b));

            var path = graph[0].FindPath(graph[4]);
            Console.WriteLine(path
                .Select(z => z.NodeNumber.ToString())
                .Aggregate((a, b) => a + " " + b));

            Console.WriteLine(graph.KahnAlgorithm()
                .Select(z => z.NodeNumber.ToString())
                .Aggregate((a, b) => a + " " + b));

            Console.WriteLine(graph.TarjanAlgorithm()
                .Select(z => z.NodeNumber.ToString())
                .Aggregate((a, b) => a + " " + b));

            // Хранение дополнительной информации о графе выносим в отдельные сущности
            graph = new Graph(10);
            var captions = new string[graph.Length];
            captions[0] = "A";

            var captions1 = new Dictionary<Node, string>();
            captions1[graph[0]] = "A";
        }
    }
}
