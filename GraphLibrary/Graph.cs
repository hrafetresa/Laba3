using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLibrary
{
    public class Graph : IComparable<Graph>
    {
        protected List<List<int>> adjacencyList; // исходящие соседи для каждой вершины
        public int VertexCount => adjacencyList.Count;

        public Graph()
        {
            adjacencyList = new List<List<int>>();
        }

        public Graph(int vertices)
        {
            adjacencyList = new List<List<int>>(vertices);
            for (int i = 0; i < vertices; i++)
                adjacencyList.Add(new List<int>());
        }

        public Graph(int vertices, IEnumerable<(int from, int to)> edges) : this(vertices)
        {
            foreach (var e in edges)
                AddEdge(e.from, e.to);
        }

        public Graph(Graph other)
        {
            adjacencyList = new List<List<int>>(other.VertexCount);
            foreach (var list in other.adjacencyList)
                adjacencyList.Add(new List<int>(list));
        }

        ~Graph() { }

        public void AddEdge(int from, int to)
        {
            if (from < 0 || from >= VertexCount || to < 0 || to >= VertexCount)
                throw new ArgumentOutOfRangeException("Индекс вершины вне диапазона.");
            adjacencyList[from].Add(to);
        }

        public void RemoveEdge(int from, int to) => adjacencyList[from].Remove(to);

        public bool HasEdge(int from, int to) => adjacencyList[from].Contains(to);

        public bool IsConnected()
        {
            if (VertexCount == 0) return true;
            bool[] visited = new bool[VertexCount];
            DFS(0, visited);
            return visited.All(v => v);
        }

        public bool IsComplete()
        {
            for (int i = 0; i < VertexCount; i++)
                for (int j = 0; j < VertexCount; j++)
                    if (i != j && !HasEdge(i, j))
                        return false;
            return true;
        }

        public bool IsBipartite()
        {
            int[] color = new int[VertexCount];
            for (int i = 0; i < VertexCount; i++)
                if (color[i] == 0 && !BFSBipartite(i, color))
                    return false;
            return true;
        }

        private bool BFSBipartite(int start, int[] color)
        {
            var q = new Queue<int>();
            q.Enqueue(start);
            color[start] = 1;
            while (q.Count > 0)
            {
                int u = q.Dequeue();
                foreach (int v in adjacencyList[u])
                {
                    if (color[v] == 0)
                    {
                        color[v] = (color[u] == 1) ? 2 : 1;
                        q.Enqueue(v);
                    }
                    else if (color[v] == color[u])
                        return false;
                }
            }
            return true;
        }

        public Graph GetComplement()
        {
            Graph comp = new Graph(VertexCount);
            for (int i = 0; i < VertexCount; i++)
                for (int j = 0; j < VertexCount; j++)
                    if (i != j && !HasEdge(i, j))
                        comp.AddEdge(i, j);
            return comp;
        }

        public List<int> GetSources()
        {
            int[] inDegree = new int[VertexCount];
            for (int i = 0; i < VertexCount; i++)
                foreach (int v in adjacencyList[i])
                    inDegree[v]++;
            return Enumerable.Range(0, VertexCount).Where(i => inDegree[i] == 0).ToList();
        }

        public List<int> GetSinks()
        {
            return Enumerable.Range(0, VertexCount).Where(i => adjacencyList[i].Count == 0).ToList();
        }

        protected void DFS(int vertex, bool[] visited)
        {
            visited[vertex] = true;
            foreach (int n in adjacencyList[vertex])
                if (!visited[n]) DFS(n, visited);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(VertexCount.ToString());
            for (int i = 0; i < VertexCount; i++)
            {
                sb.Append(i.ToString());
                if (adjacencyList[i].Count > 0)
                    sb.Append(" " + string.Join(" ", adjacencyList[i]));
                sb.AppendLine();
            }
            return sb.ToString().TrimEnd();
        }

        public static Graph Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("Строка пуста.");
            var lines = s.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0) throw new FormatException("Нет данных.");
            if (!int.TryParse(lines[0].Trim(), out int v) || v < 0)
                throw new FormatException("Неверное число вершин.");
            var g = new Graph(v);
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2) continue;
                if (int.TryParse(parts[0], out int from))
                    for (int j = 1; j < parts.Length; j++)
                        if (int.TryParse(parts[j], out int to))
                            g.AddEdge(from, to);
            }
            return g;
        }

        public static Graph operator +(Graph a, Graph b)
        {
            int n = Math.Max(a.VertexCount, b.VertexCount);
            var res = new Graph(n);
            for (int i = 0; i < a.VertexCount; i++)
                foreach (int to in a.adjacencyList[i])
                    res.AddEdge(i, to);
            for (int i = 0; i < b.VertexCount; i++)
                foreach (int to in b.adjacencyList[i])
                    if (!res.HasEdge(i, to))
                        res.AddEdge(i, to);
            return res;
        }

        public override bool Equals(object obj)
        {
            if (obj is Graph other)
            {
                if (VertexCount != other.VertexCount) return false;
                for (int i = 0; i < VertexCount; i++)
                    if (!adjacencyList[i].SequenceEqual(other.adjacencyList[i]))
                        return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int h = VertexCount;
            foreach (var l in adjacencyList)
                h = h * 31 + l.Count;
            return h;
        }

        public static bool operator ==(Graph a, Graph b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }
        public static bool operator !=(Graph a, Graph b) => !(a == b);

        public int CompareTo(Graph other)
        {
            if (other == null) return 1;
            return VertexCount.CompareTo(other.VertexCount);
        }
    }
}