namespace NESDebuggerHarness
{
    partial class frmMemory
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblError = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.MaskedTextBox();
            this.tblMemory = new System.Windows.Forms.TableLayoutPanel();
            this.ctxAddressTraps = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trapAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ctxAddressTraps.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblError);
            this.splitContainer1.Panel1.Controls.Add(this.btnGo);
            this.splitContainer1.Panel1.Controls.Add(this.txtAddress);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tblMemory);
            this.splitContainer1.Size = new System.Drawing.Size(926, 508);
            this.splitContainer1.SplitterDistance = 41;
            this.splitContainer1.TabIndex = 0;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(338, 11);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 3;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(171, 2);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(94, 22);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "&Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(3, 3);
            this.txtAddress.Mask = "AAAA";
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(162, 20);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtAddress_MaskInputRejected);
            // 
            // tblMemory
            // 
            this.tblMemory.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblMemory.ColumnCount = 17;
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.66055F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896404F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896404F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896404F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896404F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896404F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896404F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896404F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896027F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896027F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896027F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896027F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896027F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896027F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896027F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896027F));
            this.tblMemory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.896402F));
            this.tblMemory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMemory.Location = new System.Drawing.Point(0, 0);
            this.tblMemory.Name = "tblMemory";
            this.tblMemory.RowCount = 9;
            this.tblMemory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.71429F));
            this.tblMemory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.16072F));
            this.tblMemory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.16072F));
            this.tblMemory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.16072F));
            this.tblMemory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.16072F));
            this.tblMemory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.16072F));
            this.tblMemory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.16072F));
            this.tblMemory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.16072F));
            this.tblMemory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.16072F));
            this.tblMemory.Size = new System.Drawing.Size(926, 463);
            this.tblMemory.TabIndex = 0;
            // 
            // ctxAddressTraps
            // 
            this.ctxAddressTraps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trapAddressToolStripMenuItem,
            this.removeTrapToolStripMenuItem});
            this.ctxAddressTraps.Name = "ctxAddressTraps";
            this.ctxAddressTraps.Size = new System.Drawing.Size(153, 70);
            // 
            // trapAddressToolStripMenuItem
            // 
            this.trapAddressToolStripMenuItem.Name = "trapAddressToolStripMenuItem";
            this.trapAddressToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.trapAddressToolStripMenuItem.Text = "&Trap Address";
            this.trapAddressToolStripMenuItem.Click += new System.EventHandler(this.trapAddressToolStripMenuItem_Click);
            // 
            // removeTrapToolStripMenuItem
            // 
            this.removeTrapToolStripMenuItem.Name = "removeTrapToolStripMenuItem";
            this.removeTrapToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.removeTrapToolStripMenuItem.Text = "Remove Trap";
            this.removeTrapToolStripMenuItem.Click += new System.EventHandler(this.removeTrapToolStripMenuItem_Click);
            // 
            // frmMemory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 508);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmMemory";
            this.Text = "NES Memory";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ctxAddressTraps.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tblMemory;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.MaskedTextBox txtAddress;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.ContextMenuStrip ctxAddressTraps;
        private System.Windows.Forms.ToolStripMenuItem trapAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeTrapToolStripMenuItem;
    }
}