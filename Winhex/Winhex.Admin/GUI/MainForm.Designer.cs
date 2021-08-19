namespace Winhex.Admin
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.установитьКличкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMeniGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.обновитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMeniGrid.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.ContextMenuStrip = this.contextMeniGrid;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView1;
            this.gridControl.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(775, 394);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 1331;
            this.gridView1.GridControl = this.gridControl;
            this.gridView1.Name = "gridView1";
            // 
            // comboBox
            // 
            this.comboBox.ContextMenuStrip = this.contextMenuStrip1;
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(13, 14);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(539, 24);
            this.comboBox.TabIndex = 1;
            this.comboBox.SelectedValueChanged += new System.EventHandler(this.comboBox_ValueChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.установитьКличкуToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(208, 28);
            // 
            // установитьКличкуToolStripMenuItem
            // 
            this.установитьКличкуToolStripMenuItem.Name = "установитьКличкуToolStripMenuItem";
            this.установитьКличкуToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
            this.установитьКличкуToolStripMenuItem.Text = "Установить кличку";
            this.установитьКличкуToolStripMenuItem.Click += new System.EventHandler(this.установитьКличкуToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(568, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMeniGrid
            // 
            this.contextMeniGrid.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMeniGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обновитьToolStripMenuItem});
            this.contextMeniGrid.Name = "contextMeniGrid";
            this.contextMeniGrid.Size = new System.Drawing.Size(148, 28);
            // 
            // обновитьToolStripMenuItem
            // 
            this.обновитьToolStripMenuItem.Name = "обновитьToolStripMenuItem";
            this.обновитьToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.обновитьToolStripMenuItem.Text = "Обновить";
            this.обновитьToolStripMenuItem.Click += new System.EventHandler(this.обновитьToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.gridControl);
            this.panel1.Location = new System.Drawing.Point(13, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 394);
            this.panel1.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMeniGrid.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem установитьКличкуToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMeniGrid;
        private System.Windows.Forms.ToolStripMenuItem обновитьToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
    }
}

