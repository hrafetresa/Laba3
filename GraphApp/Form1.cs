using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GraphLibrary;
using Microsoft.VisualBasic;   

namespace GraphApp
{
    public partial class Form1 : Form
    {
        private List<Graph> graphs = new List<Graph>();
        private ComboBox cmbType;
        private TextBox txtInput;
        private Button btnCreate;
        private ListBox lstGraphs;
        private Button btnConnectivity, btnComplete, btnBipartite, btnComplement, btnSources, btnSinks;
        private Button btnDijkstra, btnPrim, btnKruskal;
        private Button btnEquals, btnNotEquals, btnSort, btnDelete;
        private Label lblResult;

        public Form1() => InitializeComponent();

        private void InitializeComponent()
        {
            this.Text = "Графы";
            this.Width = 820;
            this.Height = 620;

            // Тип графа
            var lblType = new Label() { Text = "Тип:", Location = new Point(10, 10), Width = 50 };
            cmbType = new ComboBox() { Location = new Point(60, 10), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbType.Items.AddRange(new string[] { "Обычный граф", "Взвешенный граф" });
            cmbType.SelectedIndex = 0;

            // Ввод
            var lblInput = new Label() { Text = "Введите данные графа:", Location = new Point(10, 45), Width = 160 };
            txtInput = new TextBox() { Location = new Point(10, 70), Width = 300, Height = 60, Multiline = true, ScrollBars = ScrollBars.Vertical };
            btnCreate = new Button() { Text = "Создать", Location = new Point(320, 70), Size = new Size(90, 40) };
            btnCreate.Click += BtnCreate_Click;

            // Список
            lstGraphs = new ListBox() { Location = new Point(10, 130), Width = 400, Height = 180, SelectionMode = SelectionMode.MultiSimple };
            lstGraphs.SelectedIndexChanged += (s, e) =>
            {
                if (lstGraphs.SelectedIndex >= 0 && lstGraphs.SelectedIndex < graphs.Count)
                    lblResult.Text = graphs[lstGraphs.SelectedIndex].ToString();
            };

            // Кнопки операций (используем общий метод CreateButton с параметрами)
            btnConnectivity = CreateButton("Связность", 430, 130, (s, e) => Op(g => g.IsConnected().ToString()));
            btnComplete = CreateButton("Полнота", 540, 130, (s, e) => Op(g => g.IsComplete().ToString()));
            btnBipartite = CreateButton("Двудольность", 650, 130, (s, e) => Op(g => g.IsBipartite().ToString()));
            btnComplement = CreateButton("Дополнение", 430, 160, (s, e) =>
            {
                var g = GetGraph(); if (g == null) return;
                var comp = g.GetComplement(); graphs.Add(comp); RefreshList(); lblResult.Text = "Дополнение добавлено.";
            });
            btnSources = CreateButton("Источники", 540, 160, (s, e) => Op(g => string.Join(", ", g.GetSources())));
            btnSinks = CreateButton("Стоки", 650, 160, (s, e) => Op(g => string.Join(", ", g.GetSinks())));

            // Взвешенные
            btnDijkstra = CreateButton("Дейкстра", 430, 200, (s, e) =>
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
            });
            btnPrim = CreateButton("Прим", 540, 200, (s, e) =>
            {
                var wg = GetGraph() as WeightedGraph;
                if (wg == null) return;
                var mst = wg.PrimMST();
                lblResult.Text = "Рёбра: " + string.Join("; ", mst) + " | Вес: " + mst.Sum(x => x.weight);
            });
            btnKruskal = CreateButton("Краскал", 650, 200, (s, e) =>
            {
                var wg = GetGraph() as WeightedGraph;
                if (wg == null) return;
                var mst = wg.KruskalMST();
                lblResult.Text = "Рёбра: " + string.Join("; ", mst) + " | Вес: " + mst.Sum(x => x.weight);
            });

            // Сравнение и управление (используем перегрузку с width)
            btnEquals = CreateButton("==", 430, 250, (s, e) => Compare(true), 50);
            btnNotEquals = CreateButton("!=", 490, 250, (s, e) => Compare(false), 50);
            btnSort = CreateButton("Сортировать", 550, 250, (s, e) => { graphs.Sort(); RefreshList(); lblResult.Text = "Отсортировано."; }, 100);
            btnDelete = CreateButton("Удалить", 660, 250, (s, e) =>
            {
                var g = GetGraph(); if (g != null) { graphs.Remove(g); RefreshList(); lblResult.Text = "Удалён."; }
            }, 80);

            lblResult = new Label()
            {
                Location = new Point(10, 330),
                Size = new Size(700, 200),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            Controls.AddRange(new Control[] { lblType, cmbType, lblInput, txtInput, btnCreate, lstGraphs,
                btnConnectivity, btnComplete, btnBipartite, btnComplement, btnSources, btnSinks,
                btnDijkstra, btnPrim, btnKruskal, btnEquals, btnNotEquals, btnSort, btnDelete, lblResult });
        }

        // Единый метод создания кнопки с опциональной шириной
        private Button CreateButton(string text, int x, int y, EventHandler click, int width = 100)
        {
            var b = new Button { Text = text, Location = new Point(x, y), Width = width };
            b.Click += click;
            return b;
        }

        private void Op(Func<Graph, string> func)
        {
            var g = GetGraph(); if (g == null) return;
            try { lblResult.Text = func(g); }
            catch (Exception ex) { lblResult.Text = "Ошибка: " + ex.Message; }
        }

        private Graph GetGraph()
        {
            if (lstGraphs.SelectedIndex >= 0 && lstGraphs.SelectedIndex < graphs.Count)
                return graphs[lstGraphs.SelectedIndex];
            MessageBox.Show("Выделите граф в списке.");
            return null;
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

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtInput.Text.Trim();
                if (string.IsNullOrEmpty(input)) { MessageBox.Show("Введите данные."); return; }
                if (cmbType.SelectedIndex == 0)
                    graphs.Add(Graph.Parse(input));
                else
                    graphs.Add(WeightedGraph.Parse(input));
                RefreshList();
                lblResult.Text = "Граф создан.";
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
        }

        private void Compare(bool eq)
        {
            if (lstGraphs.SelectedIndices.Count != 2)
            { MessageBox.Show("Выделите ровно два графа (Ctrl+клик)."); return; }
            int i1 = lstGraphs.SelectedIndices[0], i2 = lstGraphs.SelectedIndices[1];
            bool res = eq ? (graphs[i1] == graphs[i2]) : (graphs[i1] != graphs[i2]);
            lblResult.Text = $"{i1} {(eq ? "==" : "!=")} {i2} = {res}";
        }
    }
}