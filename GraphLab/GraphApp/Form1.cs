using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GraphLibrary;
using Microsoft.VisualBasic;

namespace GraphApp
{
    public partial class Form1 : Form
    {
        private List<Graph> graphs = new List<Graph>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtInput.Text.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    MessageBox.Show("Введите данные.");
                    return;
                }

                if (cmbType.SelectedIndex == 0)
                    graphs.Add(Graph.Parse(input));
                else
                    graphs.Add(WeightedGraph.Parse(input));

                RefreshList();
                lblResult.Text = "Граф создан.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void RefreshList()
        {
            lstGraphs.Items.Clear();
            for (int i = 0; i < graphs.Count; i++)
            {
                string type = graphs[i] is WeightedGraph ? "[Взвеш.]" : "[Обыч.]";
                lstGraphs.Items.Add($"{i}: {type} {graphs[i].ToString().Replace(Environment.NewLine, " | ")}");
            }
        }

        private void lstGraphs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGraphs.SelectedIndex >= 0 && lstGraphs.SelectedIndex < graphs.Count)
                lblResult.Text = graphs[lstGraphs.SelectedIndex].ToString();
        }

        private Graph GetGraph()
        {
            if (lstGraphs.SelectedIndex >= 0 && lstGraphs.SelectedIndex < graphs.Count)
                return graphs[lstGraphs.SelectedIndex];

            MessageBox.Show("Выделите граф в списке.");
            return null;
        }

        private void Op(Func<Graph, string> func)
        {
            var g = GetGraph();
            if (g == null) return;

            try { lblResult.Text = func(g); }
            catch (Exception ex) { lblResult.Text = "Ошибка: " + ex.Message; }
        }

        private void btnConnectivity_Click(object sender, EventArgs e) => Op(g => g.IsConnected().ToString());
        private void btnComplete_Click(object sender, EventArgs e) => Op(g => g.IsComplete().ToString());
        private void btnBipartite_Click(object sender, EventArgs e) => Op(g => g.IsBipartite().ToString());
        private void btnSources_Click(object sender, EventArgs e) => Op(g => string.Join(", ", g.GetSources()));
        private void btnSinks_Click(object sender, EventArgs e) => Op(g => string.Join(", ", g.GetSinks()));

        private void btnComplement_Click(object sender, EventArgs e)
        {
            var g = GetGraph();
            if (g == null) return;

            var comp = g.GetComplement();
            graphs.Add(comp);
            RefreshList();
            lblResult.Text = "Дополнение добавлено.";
        }

        private void btnDijkstra_Click(object sender, EventArgs e)
        {
            var wg = GetGraph() as WeightedGraph;
            if (wg == null) { MessageBox.Show("Выберите взвешенный граф."); return; }

            string st = Interaction.InputBox("Начальная вершина:", "Дейкстра", "0");
            string en = Interaction.InputBox("Конечная вершина:", "Дейкстра", "1");
            if (int.TryParse(st, out int sv) && int.TryParse(en, out int ev))
            {
                var (d, p) = wg.Dijkstra(sv, ev);
                lblResult.Text = (p == null) ? "Путь не найден" : $"Расстояние: {d}, Путь: {string.Join(" → ", p)}";
            }
        }

        private void btnPrim_Click(object sender, EventArgs e)
        {
            var wg = GetGraph() as WeightedGraph;
            if (wg == null) { MessageBox.Show("Выберите взвешенный граф."); return; }

            var mst = wg.PrimMST();
            lblResult.Text = "Рёбра: " + string.Join("; ", mst) + " | Вес: " + mst.Sum(x => x.weight);
        }

        private void btnKruskal_Click(object sender, EventArgs e)
        {
            var wg = GetGraph() as WeightedGraph;
            if (wg == null) { MessageBox.Show("Выберите взвешенный граф."); return; }

            var mst = wg.KruskalMST();
            lblResult.Text = "Рёбра: " + string.Join("; ", mst) + " | Вес: " + mst.Sum(x => x.weight);
        }

        private void btnEquals_Click(object sender, EventArgs e) => Compare(true);
        private void btnNotEquals_Click(object sender, EventArgs e) => Compare(false);

        private void Compare(bool eq)
        {
            if (lstGraphs.SelectedIndices.Count != 2)
            {
                MessageBox.Show("Выделите ровно два графа (Ctrl+клик).");
                return;
            }
            int i1 = lstGraphs.SelectedIndices[0];
            int i2 = lstGraphs.SelectedIndices[1];
            bool res = eq ? (graphs[i1] == graphs[i2]) : (graphs[i1] != graphs[i2]);
            lblResult.Text = $"{i1} {(eq ? "==" : "!=")} {i2} = {res}";
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            graphs.Sort();
            RefreshList();
            lblResult.Text = "Отсортировано.";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var g = GetGraph();
            if (g != null)
            {
                graphs.Remove(g);
                RefreshList();
                lblResult.Text = "Удалён.";
            }
        }
    }
}