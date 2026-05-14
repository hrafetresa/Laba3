namespace GraphApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lstGraphs = new System.Windows.Forms.ListBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnConnectivity = new System.Windows.Forms.Button();
            this.btnComplete = new System.Windows.Forms.Button();
            this.btnBipartite = new System.Windows.Forms.Button();
            this.btnSources = new System.Windows.Forms.Button();
            this.btnSinks = new System.Windows.Forms.Button();
            this.btnDijkstra = new System.Windows.Forms.Button();
            this.btnPrim = new System.Windows.Forms.Button();
            this.btnKruskal = new System.Windows.Forms.Button();
            this.btnNotEquals = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnComplement = new System.Windows.Forms.Button();
            this.btnEquals = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Обычный граф",
            "Взвешенный граф"});
            this.cmbType.Location = new System.Drawing.Point(36, 13);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(121, 21);
            this.cmbType.TabIndex = 0;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(36, 55);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput.Size = new System.Drawing.Size(160, 63);
            this.txtInput.TabIndex = 1;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(36, 134);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Создать";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lstGraphs
            // 
            this.lstGraphs.FormattingEnabled = true;
            this.lstGraphs.Location = new System.Drawing.Point(36, 177);
            this.lstGraphs.Name = "lstGraphs";
            this.lstGraphs.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstGraphs.Size = new System.Drawing.Size(259, 108);
            this.lstGraphs.TabIndex = 3;
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(36, 330);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(343, 25);
            this.lblResult.TabIndex = 4;
            // 
            // btnConnectivity
            // 
            this.btnConnectivity.Location = new System.Drawing.Point(318, 183);
            this.btnConnectivity.Name = "btnConnectivity";
            this.btnConnectivity.Size = new System.Drawing.Size(75, 23);
            this.btnConnectivity.TabIndex = 5;
            this.btnConnectivity.Text = "Связность";
            this.btnConnectivity.UseVisualStyleBackColor = true;
            this.btnConnectivity.Click += new System.EventHandler(this.btnConnectivity_Click);
            // 
            // btnComplete
            // 
            this.btnComplete.Location = new System.Drawing.Point(400, 183);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(75, 23);
            this.btnComplete.TabIndex = 5;
            this.btnComplete.Text = "Полнота";
            this.btnComplete.UseVisualStyleBackColor = true;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnBipartite
            // 
            this.btnBipartite.Location = new System.Drawing.Point(481, 183);
            this.btnBipartite.Name = "btnBipartite";
            this.btnBipartite.Size = new System.Drawing.Size(95, 23);
            this.btnBipartite.TabIndex = 5;
            this.btnBipartite.Text = "Двудольность";
            this.btnBipartite.UseVisualStyleBackColor = true;
            this.btnBipartite.Click += new System.EventHandler(this.btnBipartite_Click);
            // 
            // btnSources
            // 
            this.btnSources.Location = new System.Drawing.Point(318, 212);
            this.btnSources.Name = "btnSources";
            this.btnSources.Size = new System.Drawing.Size(75, 23);
            this.btnSources.TabIndex = 5;
            this.btnSources.Text = "Источники";
            this.btnSources.UseVisualStyleBackColor = true;
            this.btnSources.Click += new System.EventHandler(this.btnSources_Click);
            // 
            // btnSinks
            // 
            this.btnSinks.Location = new System.Drawing.Point(399, 212);
            this.btnSinks.Name = "btnSinks";
            this.btnSinks.Size = new System.Drawing.Size(75, 23);
            this.btnSinks.TabIndex = 5;
            this.btnSinks.Text = "Стоки";
            this.btnSinks.UseVisualStyleBackColor = true;
            this.btnSinks.Click += new System.EventHandler(this.btnSinks_Click);
            // 
            // btnDijkstra
            // 
            this.btnDijkstra.Location = new System.Drawing.Point(608, 183);
            this.btnDijkstra.Name = "btnDijkstra";
            this.btnDijkstra.Size = new System.Drawing.Size(75, 23);
            this.btnDijkstra.TabIndex = 5;
            this.btnDijkstra.Text = "Дейкстра";
            this.btnDijkstra.UseVisualStyleBackColor = true;
            this.btnDijkstra.Click += new System.EventHandler(this.btnDijkstra_Click);
            // 
            // btnPrim
            // 
            this.btnPrim.Location = new System.Drawing.Point(608, 217);
            this.btnPrim.Name = "btnPrim";
            this.btnPrim.Size = new System.Drawing.Size(75, 23);
            this.btnPrim.TabIndex = 5;
            this.btnPrim.Text = "Прим";
            this.btnPrim.UseVisualStyleBackColor = true;
            this.btnPrim.Click += new System.EventHandler(this.btnPrim_Click);
            // 
            // btnKruskal
            // 
            this.btnKruskal.Location = new System.Drawing.Point(608, 251);
            this.btnKruskal.Name = "btnKruskal";
            this.btnKruskal.Size = new System.Drawing.Size(75, 23);
            this.btnKruskal.TabIndex = 5;
            this.btnKruskal.Text = "Краскал";
            this.btnKruskal.UseVisualStyleBackColor = true;
            this.btnKruskal.Click += new System.EventHandler(this.btnKruskal_Click);
            // 
            // btnNotEquals
            // 
            this.btnNotEquals.Location = new System.Drawing.Point(400, 251);
            this.btnNotEquals.Name = "btnNotEquals";
            this.btnNotEquals.Size = new System.Drawing.Size(75, 23);
            this.btnNotEquals.TabIndex = 5;
            this.btnNotEquals.Text = "!=";
            this.btnNotEquals.UseVisualStyleBackColor = true;
            this.btnNotEquals.Click += new System.EventHandler(this.btnNotEquals_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(78, 294);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(87, 23);
            this.btnSort.TabIndex = 5;
            this.btnSort.Text = "Сортировать";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // btnComplement
            // 
            this.btnComplement.Location = new System.Drawing.Point(481, 212);
            this.btnComplement.Name = "btnComplement";
            this.btnComplement.Size = new System.Drawing.Size(86, 23);
            this.btnComplement.TabIndex = 5;
            this.btnComplement.Text = "Дополнение";
            this.btnComplement.UseVisualStyleBackColor = true;
            this.btnComplement.Click += new System.EventHandler(this.btnComplement_Click);
            // 
            // btnEquals
            // 
            this.btnEquals.Location = new System.Drawing.Point(318, 251);
            this.btnEquals.Name = "btnEquals";
            this.btnEquals.Size = new System.Drawing.Size(75, 23);
            this.btnEquals.TabIndex = 5;
            this.btnEquals.Text = "==";
            this.btnEquals.UseVisualStyleBackColor = true;
            this.btnEquals.Click += new System.EventHandler(this.btnEquals_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(171, 294);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 369);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEquals);
            this.Controls.Add(this.btnComplement);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnNotEquals);
            this.Controls.Add(this.btnKruskal);
            this.Controls.Add(this.btnPrim);
            this.Controls.Add(this.btnDijkstra);
            this.Controls.Add(this.btnSinks);
            this.Controls.Add(this.btnSources);
            this.Controls.Add(this.btnBipartite);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.btnConnectivity);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lstGraphs);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.cmbType);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ListBox lstGraphs;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnConnectivity;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnBipartite;
        private System.Windows.Forms.Button btnSources;
        private System.Windows.Forms.Button btnSinks;
        private System.Windows.Forms.Button btnDijkstra;
        private System.Windows.Forms.Button btnPrim;
        private System.Windows.Forms.Button btnKruskal;
        private System.Windows.Forms.Button btnNotEquals;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnComplement;
        private System.Windows.Forms.Button btnEquals;
        private System.Windows.Forms.Button btnDelete;
    }
}

