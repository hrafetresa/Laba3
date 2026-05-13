using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLibrary
{
    public class WeightedGraph : Graph
    {
        private List<Dictionary<int, double>> weights; // для каждой вершины словарь сосед->вес

        public WeightedGraph() : base() => weights = new List<Dictionary<int, double>>();
        public WeightedGraph(int vertices) : base(vertices)
        {
            weights = new List<Dictionary<int, double>>(vertices);
            for (int i = 0; i < vertices; i++) weights.Add(new Dictionary<int, double>());
        }
        public WeightedGraph(int vertices, IEnumerable<(int from, int to, double weight)> edges) : this(vertices)
        {
            foreach (var e in edges) AddEdge(e.from, e.to, e.weight);
        }

        public void AddEdge(int from, int to, double weight)
        {
            base.AddEdge(from, to);
            weights[from][to] = weight;
        }

        public double GetWeight(int from, int to) => weights[from][to];

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(VertexCount.ToString());
            for (int i = 0; i < VertexCount; i++)
            {
                var list = new List<string>();
                foreach (int n in adjacencyList[i])
                    list.Add($"{n}({weights[i][n]})");
                sb.Append(i.ToString());
                if (list.Count > 0) sb.Append(" " + string.Join(" ", list));
                sb.AppendLine();
            }
            return sb.ToString().TrimEnd();
        }

        public static new WeightedGraph Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("Строка пуста.");
            var lines = s.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0) throw new FormatException("Нет данных.");
            if (!int.TryParse(lines[0].Trim(), out int v) || v < 0)
                throw new FormatException("Неверное число вершин.");
            var g = new WeightedGraph(v);
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 3 &&
                    int.TryParse(parts[0], out int from) &&
                    int.TryParse(parts[1], out int to) &&
                    double.TryParse(parts[2], out double w))
                    g.AddEdge(from, to, w);
            }
            return g;
        }

        public (double distance, List<int> path) Dijkstra(int start, int end)
        {
            double[] dist = new double[VertexCount];
            int[] prev = new int[VertexCount];
            bool[] visited = new bool[VertexCount];
            for (int i = 0; i < VertexCount; i++) dist[i] = double.PositiveInfinity;
            dist[start] = 0;

            for (int i = 0; i < VertexCount; i++)
            {
                int u = -1;
                double min = double.PositiveInfinity;
                for (int j = 0; j < VertexCount; j++)
                    if (!visited[j] && dist[j] < min) { min = dist[j]; u = j; }
                if (u == -1 || u == end) break;
                visited[u] = true;
                foreach (int v in adjacencyList[u])
                {
                    double alt = dist[u] + weights[u][v];
                    if (alt < dist[v]) { dist[v] = alt; prev[v] = u; }
                }
            }
            if (double.IsInfinity(dist[end])) return (double.PositiveInfinity, null);
            var path = new List<int>();
            for (int at = end; at != start; at = prev[at]) path.Add(at);
            path.Add(start);
            path.Reverse();
            return (dist[end], path);
        }

        public List<(int from, int to, double weight)> PrimMST()
        {
            if (VertexCount == 0) return new List<(int, int, double)>();
            bool[] inMST = new bool[VertexCount];
            double[] minWeight = new double[VertexCount];
            int[] parent = new int[VertexCount];
            for (int i = 0; i < VertexCount; i++) { minWeight[i] = double.PositiveInfinity; parent[i] = -1; }
            minWeight[0] = 0;

            for (int i = 0; i < VertexCount; i++)
            {
                int u = -1;
                double min = double.PositiveInfinity;
                for (int j = 0; j < VertexCount; j++)
                    if (!inMST[j] && minWeight[j] < min) { min = minWeight[j]; u = j; }
                if (u == -1) break;
                inMST[u] = true;
                foreach (int v in adjacencyList[u])
                {
                    double w = weights[u][v];
                    if (!inMST[v] && w < minWeight[v]) { minWeight[v] = w; parent[v] = u; }
                }
            }
            var result = new List<(int, int, double)>();
            for (int i = 1; i < VertexCount; i++)
                if (parent[i] != -1) result.Add((parent[i], i, weights[parent[i]][i]));
            return result;
        }

        public List<(int from, int to, double weight)> KruskalMST()
        {
            var edges = new List<(int from, int to, double weight)>();
            for (int i = 0; i < VertexCount; i++)
                foreach (int j in adjacencyList[i])
                    if (i < j) edges.Add((i, j, weights[i][j]));
            edges.Sort((a, b) => a.weight.CompareTo(b.weight));

            var dsu = new DisjointSetUnion(VertexCount);
            var result = new List<(int from, int to, double weight)>();
            foreach (var e in edges)
            {
                if (dsu.Find(e.from) != dsu.Find(e.to))
                {
                    dsu.Union(e.from, e.to);
                    result.Add(e);
                }
            }
            return result;
        }

        private class DisjointSetUnion
        {
            private int[] parent, rank;
            public DisjointSetUnion(int n)
            {
                parent = new int[n]; rank = new int[n];
                for (int i = 0; i < n; i++) parent[i] = i;
            }
            public int Find(int x) => parent[x] == x ? x : parent[x] = Find(parent[x]);
            public void Union(int x, int y)
            {
                int rx = Find(x), ry = Find(y);
                if (rx == ry) return;
                if (rank[rx] < rank[ry]) parent[rx] = ry;
                else if (rank[rx] > rank[ry]) parent[ry] = rx;
                else { parent[ry] = rx; rank[rx]++; }
            }
        }
    }
}