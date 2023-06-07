
namespace EVAL.ConnectFour.View
{
    partial class ConnectFourSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanelOuter = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanelInner = new System.Windows.Forms.TableLayoutPanel();
            this.labelP1Colour = new System.Windows.Forms.Label();
            this.comboBoxBoardSize = new System.Windows.Forms.ComboBox();
            this.buttonP1Colour = new System.Windows.Forms.Button();
            this.buttonP2Colour = new System.Windows.Forms.Button();
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.labelP2Colour = new System.Windows.Forms.Label();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanelOuter.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.tableLayoutPanelInner.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelOuter
            // 
            this.tableLayoutPanelOuter.ColumnCount = 3;
            this.tableLayoutPanelOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelOuter.Controls.Add(this.flowLayoutPanel, 1, 1);
            this.tableLayoutPanelOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelOuter.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelOuter.Name = "tableLayoutPanelOuter";
            this.tableLayoutPanelOuter.RowCount = 3;
            this.tableLayoutPanelOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelOuter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelOuter.Size = new System.Drawing.Size(369, 240);
            this.tableLayoutPanelOuter.TabIndex = 0;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel.Controls.Add(this.tableLayoutPanelInner);
            this.flowLayoutPanel.Controls.Add(this.buttonConfirm);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(21, 21);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(326, 197);
            this.flowLayoutPanel.TabIndex = 0;
            this.flowLayoutPanel.WrapContents = false;
            // 
            // tableLayoutPanelInner
            // 
            this.tableLayoutPanelInner.ColumnCount = 2;
            this.tableLayoutPanelInner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanelInner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanelInner.Controls.Add(this.labelP1Colour, 0, 1);
            this.tableLayoutPanelInner.Controls.Add(this.comboBoxBoardSize, 1, 0);
            this.tableLayoutPanelInner.Controls.Add(this.buttonP1Colour, 1, 1);
            this.tableLayoutPanelInner.Controls.Add(this.buttonP2Colour, 1, 2);
            this.tableLayoutPanelInner.Controls.Add(this.labelBoardSize, 0, 0);
            this.tableLayoutPanelInner.Controls.Add(this.labelP2Colour, 0, 2);
            this.tableLayoutPanelInner.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelInner.Name = "tableLayoutPanelInner";
            this.tableLayoutPanelInner.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanelInner.RowCount = 3;
            this.tableLayoutPanelInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelInner.Size = new System.Drawing.Size(320, 144);
            this.tableLayoutPanelInner.TabIndex = 1;
            // 
            // labelP1Colour
            // 
            this.labelP1Colour.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelP1Colour.AutoSize = true;
            this.labelP1Colour.Location = new System.Drawing.Point(33, 61);
            this.labelP1Colour.Name = "labelP1Colour";
            this.labelP1Colour.Size = new System.Drawing.Size(109, 20);
            this.labelP1Colour.TabIndex = 2;
            this.labelP1Colour.Text = "1. Játékos színe";
            // 
            // comboBoxBoardSize
            // 
            this.comboBoxBoardSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxBoardSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBoardSize.FormattingEnabled = true;
            this.comboBoxBoardSize.Location = new System.Drawing.Point(150, 16);
            this.comboBoxBoardSize.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.comboBoxBoardSize.Name = "comboBoxBoardSize";
            this.comboBoxBoardSize.Size = new System.Drawing.Size(150, 28);
            this.comboBoxBoardSize.TabIndex = 0;
            this.comboBoxBoardSize.SelectionChangeCommitted += new System.EventHandler(this.comboBoxBoardSize_SelectionChangeCommitted);
            // 
            // buttonP1Colour
            // 
            this.buttonP1Colour.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonP1Colour.Location = new System.Drawing.Point(150, 57);
            this.buttonP1Colour.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.buttonP1Colour.Name = "buttonP1Colour";
            this.buttonP1Colour.Size = new System.Drawing.Size(111, 28);
            this.buttonP1Colour.TabIndex = 3;
            this.buttonP1Colour.Text = "Módosítás";
            this.buttonP1Colour.UseVisualStyleBackColor = true;
            this.buttonP1Colour.Click += new System.EventHandler(this.buttonP1Colour_Click);
            // 
            // buttonP2Colour
            // 
            this.buttonP2Colour.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonP2Colour.Location = new System.Drawing.Point(150, 99);
            this.buttonP2Colour.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.buttonP2Colour.Name = "buttonP2Colour";
            this.buttonP2Colour.Size = new System.Drawing.Size(111, 28);
            this.buttonP2Colour.TabIndex = 4;
            this.buttonP2Colour.Text = "Módosítás";
            this.buttonP2Colour.UseVisualStyleBackColor = true;
            this.buttonP2Colour.Click += new System.EventHandler(this.buttonP2Colour_Click);
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Location = new System.Drawing.Point(47, 20);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(95, 20);
            this.labelBoardSize.TabIndex = 0;
            this.labelBoardSize.Text = "Tábla mérete";
            // 
            // labelP2Colour
            // 
            this.labelP2Colour.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelP2Colour.AutoSize = true;
            this.labelP2Colour.Location = new System.Drawing.Point(33, 103);
            this.labelP2Colour.Name = "labelP2Colour";
            this.labelP2Colour.Size = new System.Drawing.Size(109, 20);
            this.labelP2Colour.TabIndex = 1;
            this.labelP2Colour.Text = "2. Játékos színe";
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonConfirm.Location = new System.Drawing.Point(107, 153);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(111, 41);
            this.buttonConfirm.TabIndex = 2;
            this.buttonConfirm.Text = "Mentés";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // ConnectFourSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 240);
            this.Controls.Add(this.tableLayoutPanelOuter);
            this.Name = "ConnectFourSettings";
            this.Text = "Potyogós Amőba - Beállítások";
            this.tableLayoutPanelOuter.ResumeLayout(false);
            this.tableLayoutPanelOuter.PerformLayout();
            this.flowLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanelInner.ResumeLayout(false);
            this.tableLayoutPanelInner.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanelOuter;
        private FlowLayoutPanel flowLayoutPanel;
        private Button buttonConfirm;
        private ColorDialog colorDialog;
        private TableLayoutPanel tableLayoutPanelInner;
        private Label labelP2Colour;
        private Button buttonP2Colour;
        private Label labelP1Colour;
        private ComboBox comboBoxBoardSize;
        private Label labelBoardSize;
        private Button buttonP1Colour;
    }
}