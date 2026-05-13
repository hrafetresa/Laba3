using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphLibrary;
using System.Collections.Generic;

namespace GraphLibraryTests
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void Constructor_WithVertices_CreatesCorrectCount()
        {
            var g = new Graph(3);
            Assert.AreEqual(3, g.VertexCount);
        }

        [TestMethod]
        public void AddEdge_Valid_Exists()
        {
            var g = new Graph(2);
            g.AddEdge(0, 1);
            Assert.IsTrue(g.HasEdge(0, 1));
            Assert.IsFalse(g.HasEdge(1, 0));
        }

        [TestMethod]
        public void IsConnected_Connected_True()
        {
            var g = new Graph(3);
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            Assert.IsTrue(g.IsConnected());
        }

        [TestMethod]
        public void IsConnected_Disconnected_False()
        {
            var g = new Graph(3);
            g.AddEdge(0, 1);
            Assert.IsFalse(g.IsConnected());
        }

        [TestMethod]
        public void IsComplete_Complete_True()
        {
            var g = new Graph(3);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (i != j) g.AddEdge(i, j);
            Assert.IsTrue(g.IsComplete());
        }

        [TestMethod]
        public void GetComplement_Correct()
        {
            var g = new Graph(3);
            g.AddEdge(0, 1);
            var comp = g.GetComplement();
            Assert.IsTrue(comp.HasEdge(1, 0));
            Assert.IsTrue(comp.HasEdge(0, 2));
            Assert.IsFalse(comp.HasEdge(0, 1));
        }

        [TestMethod]
        public void Parse_ValidString_Success()
        {
            var s = "3\n0 1 2\n1 0\n2";
            var g = Graph.Parse(s);
            Assert.AreEqual(3, g.VertexCount);
            Assert.IsTrue(g.HasEdge(0, 1));
            Assert.IsTrue(g.HasEdge(1, 0));
        }

        [TestMethod]
        public void Equals_SameGraph_True()
        {
            var a = new Graph(2); a.AddEdge(0, 1);
            var b = new Graph(2); b.AddEdge(0, 1);
            Assert.IsTrue(a == b);
        }

        [TestMethod]
        public void OperatorPlus_Combines()
        {
            var a = new Graph(2); a.AddEdge(0, 1);
            var b = new Graph(3); b.AddEdge(1, 0); b.AddEdge(2, 1);
            var sum = a + b;
            Assert.AreEqual(3, sum.VertexCount);
            Assert.IsTrue(sum.HasEdge(0, 1));
            Assert.IsTrue(sum.HasEdge(1, 0));
        }
    }

    [TestClass]
    public class WeightedGraphTests
    {
        [TestMethod]
        public void Dijkstra_CorrectPath()
        {
            var g = new WeightedGraph(4);
            g.AddEdge(0, 1, 2);
            g.AddEdge(0, 2, 5);
            g.AddEdge(1, 2, 1);
            g.AddEdge(1, 3, 7);
            g.AddEdge(2, 3, 3);
            var (dist, path) = g.Dijkstra(0, 3);
            Assert.AreEqual(6.0, dist, 1e-6);
            CollectionAssert.AreEqual(new List<int> { 0, 1, 2, 3 }, path);
        }

        [TestMethod]
        public void PrimMST_SumWeight()
        {
            var g = new WeightedGraph(3);
            g.AddEdge(0, 1, 1); g.AddEdge(1, 0, 1);
            g.AddEdge(0, 2, 2); g.AddEdge(2, 0, 2);
            g.AddEdge(1, 2, 3); g.AddEdge(2, 1, 3);
            var mst = g.PrimMST();
            double sum = 0;
            foreach (var e in mst) sum += e.weight;
            Assert.AreEqual(3.0, sum, 1e-6);
        }
    }
}