namespace NESDebuggerHarness
{
    partial class frmDebugger
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
            this.MainDebugger = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblNotification = new System.Windows.Forms.Label();
            this.cmbMemory = new System.Windows.Forms.ComboBox();
            this.chkUpdateUI = new System.Windows.Forms.CheckBox();
            this.btnMemory = new System.Windows.Forms.Button();
            this.lblLoadedCart = new System.Windows.Forms.Label();
            this.lblLoadText = new System.Windows.Forms.Label();
            this.btnLoadCart = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lstAssembly = new System.Windows.Forms.ListBox();
            this.ctxBreakpoints = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddBreakPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRemoveBreakPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.txtErrorConsole = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tblDebuggerCmds = new System.Windows.Forms.TableLayoutPanel();
            this.btnJumpToLine = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnNextFrame = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnSerialize = new System.Windows.Forms.Button();
            this.tblCPUInfo = new System.Windows.Forms.TableLayoutPanel();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtACC = new System.Windows.Forms.TextBox();
            this.txtSP = new System.Windows.Forms.TextBox();
            this.txtPS = new System.Windows.Forms.TextBox();
            this.lblPC = new System.Windows.Forms.Label();
            this.lblPS = new System.Windows.Forms.Label();
            this.lblSP = new System.Windows.Forms.Label();
            this.lblACC = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.txtPC = new System.Windows.Forms.TextBox();
            this.MainDebugger.Panel1.SuspendLayout();
            this.MainDebugger.Panel2.SuspendLayout();
            this.MainDebugger.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.ctxBreakpoints.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tblDebuggerCmds.SuspendLayout();
            this.tblCPUInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainDebugger
            // 
            this.MainDebugger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainDebugger.Location = new System.Drawing.Point(0, 0);
            this.MainDebugger.Name = "MainDebugger";
            // 
            // MainDebugger.Panel1
            // 
            this.MainDebugger.Panel1.Controls.Add(this.splitContainer1);
            // 
            // MainDebugger.Panel2
            // 
            this.MainDebugger.Panel2.Controls.Add(this.splitContainer2);
            this.MainDebugger.Size = new System.Drawing.Size(940, 544);
            this.MainDebugger.SplitterDistance = 658;
            this.MainDebugger.TabIndex = 0;
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
            this.splitContainer1.Panel1.Controls.Add(this.lblNotification);
            this.splitContainer1.Panel1.Controls.Add(this.cmbMemory);
            this.splitContainer1.Panel1.Controls.Add(this.chkUpdateUI);
            this.splitContainer1.Panel1.Controls.Add(this.btnMemory);
            this.splitContainer1.Panel1.Controls.Add(this.lblLoadedCart);
            this.splitContainer1.Panel1.Controls.Add(this.lblLoadText);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoadCart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(658, 544);
            this.splitContainer1.SplitterDistance = 137;
            this.splitContainer1.TabIndex = 0;
            // 
            // lblNotification
            // 
            this.lblNotification.AutoSize = true;
            this.lblNotification.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotification.ForeColor = System.Drawing.Color.Blue;
            this.lblNotification.Location = new System.Drawing.Point(266, 72);
            this.lblNotification.Name = "lblNotification";
            this.lblNotification.Size = new System.Drawing.Size(90, 17);
            this.lblNotification.TabIndex = 6;
            this.lblNotification.Text = "Notification";
            // 
            // cmbMemory
            // 
            this.cmbMemory.FormattingEnabled = true;
            this.cmbMemory.Items.AddRange(new object[] {
            "Main",
            "PPU"});
            this.cmbMemory.Location = new System.Drawing.Point(3, 111);
            this.cmbMemory.Name = "cmbMemory";
            this.cmbMemory.Size = new System.Drawing.Size(299, 21);
            this.cmbMemory.TabIndex = 5;
            // 
            // chkUpdateUI
            // 
            this.chkUpdateUI.AutoSize = true;
            this.chkUpdateUI.Location = new System.Drawing.Point(12, 73);
            this.chkUpdateUI.Name = "chkUpdateUI";
            this.chkUpdateUI.Size = new System.Drawing.Size(169, 17);
            this.chkUpdateUI.TabIndex = 4;
            this.chkUpdateUI.Text = "&Update UI On Each Operation";
            this.chkUpdateUI.UseVisualStyleBackColor = true;
            // 
            // btnMemory
            // 
            this.btnMemory.Enabled = false;
            this.btnMemory.Location = new System.Drawing.Point(312, 104);
            this.btnMemory.Name = "btnMemory";
            this.btnMemory.Size = new System.Drawing.Size(346, 33);
            this.btnMemory.TabIndex = 3;
            this.btnMemory.Text = "&View Memory";
            this.btnMemory.UseVisualStyleBackColor = true;
            this.btnMemory.Click += new System.EventHandler(this.btnMemory_Click);
            // 
            // lblLoadedCart
            // 
            this.lblLoadedCart.AutoSize = true;
            this.lblLoadedCart.Location = new System.Drawing.Point(142, 51);
            this.lblLoadedCart.Margin = new System.Windows.Forms.Padding(3, 100, 3, 100);
            this.lblLoadedCart.Name = "lblLoadedCart";
            this.lblLoadedCart.Size = new System.Drawing.Size(0, 13);
            this.lblLoadedCart.TabIndex = 2;
            this.lblLoadedCart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadText
            // 
            this.lblLoadText.AutoSize = true;
            this.lblLoadText.Location = new System.Drawing.Point(8, 51);
            this.lblLoadText.Margin = new System.Windows.Forms.Padding(3, 100, 3, 100);
            this.lblLoadText.Name = "lblLoadText";
            this.lblLoadText.Size = new System.Drawing.Size(128, 13);
            this.lblLoadText.TabIndex = 1;
            this.lblLoadText.Text = "Current Loaded Cartridge:";
            this.lblLoadText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLoadCart
            // 
            this.btnLoadCart.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLoadCart.Location = new System.Drawing.Point(0, 0);
            this.btnLoadCart.Name = "btnLoadCart";
            this.btnLoadCart.Size = new System.Drawing.Size(658, 37);
            this.btnLoadCart.TabIndex = 0;
            this.btnLoadCart.Text = "&Load ROM";
            this.btnLoadCart.UseVisualStyleBackColor = true;
            this.btnLoadCart.Click += new System.EventHandler(this.btnLoadCart_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lstAssembly);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.txtErrorConsole);
            this.splitContainer3.Size = new System.Drawing.Size(658, 403);
            this.splitContainer3.SplitterDistance = 296;
            this.splitContainer3.TabIndex = 1;
            // 
            // lstAssembly
            // 
            this.lstAssembly.ContextMenuStrip = this.ctxBreakpoints;
            this.lstAssembly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAssembly.FormattingEnabled = true;
            this.lstAssembly.Location = new System.Drawing.Point(0, 0);
            this.lstAssembly.Name = "lstAssembly";
            this.lstAssembly.Size = new System.Drawing.Size(658, 290);
            this.lstAssembly.TabIndex = 0;
            this.lstAssembly.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstAssembly_KeyPress);
            // 
            // ctxBreakpoints
            // 
            this.ctxBreakpoints.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddBreakPoint,
            this.mnuRemoveBreakPoint});
            this.ctxBreakpoints.Name = "contextMenuStrip1";
            this.ctxBreakpoints.Size = new System.Drawing.Size(178, 48);
            // 
            // mnuAddBreakPoint
            // 
            this.mnuAddBreakPoint.Name = "mnuAddBreakPoint";
            this.mnuAddBreakPoint.Size = new System.Drawing.Size(177, 22);
            this.mnuAddBreakPoint.Text = "&Add Breakpoint";
            this.mnuAddBreakPoint.Click += new System.EventHandler(this.mnuAddBreakPoint_Click);
            // 
            // mnuRemoveBreakPoint
            // 
            this.mnuRemoveBreakPoint.Name = "mnuRemoveBreakPoint";
            this.mnuRemoveBreakPoint.Size = new System.Drawing.Size(177, 22);
            this.mnuRemoveBreakPoint.Text = "&Remove Breakpoint";
            this.mnuRemoveBreakPoint.Click += new System.EventHandler(this.mnuRemoveBreakPoint_Click);
            // 
            // txtErrorConsole
            // 
            this.txtErrorConsole.AcceptsReturn = true;
            this.txtErrorConsole.AcceptsTab = true;
            this.txtErrorConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtErrorConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtErrorConsole.ForeColor = System.Drawing.Color.Red;
            this.txtErrorConsole.Location = new System.Drawing.Point(0, 0);
            this.txtErrorConsole.Multiline = true;
            this.txtErrorConsole.Name = "txtErrorConsole";
            this.txtErrorConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtErrorConsole.Size = new System.Drawing.Size(658, 103);
            this.txtErrorConsole.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tblDebuggerCmds);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tblCPUInfo);
            this.splitContainer2.Size = new System.Drawing.Size(278, 544);
            this.splitContainer2.SplitterDistance = 265;
            this.splitContainer2.TabIndex = 0;
            // 
            // tblDebuggerCmds
            // 
            this.tblDebuggerCmds.ColumnCount = 1;
            this.tblDebuggerCmds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDebuggerCmds.Controls.Add(this.btnJumpToLine, 0, 6);
            this.tblDebuggerCmds.Controls.Add(this.btnStep, 0, 2);
            this.tblDebuggerCmds.Controls.Add(this.txtLine, 0, 5);
            this.tblDebuggerCmds.Controls.Add(this.btnPause, 0, 3);
            this.tblDebuggerCmds.Controls.Add(this.btnReset, 0, 4);
            this.tblDebuggerCmds.Controls.Add(this.btnRun, 0, 1);
            this.tblDebuggerCmds.Controls.Add(this.btnNextFrame, 0, 0);
            this.tblDebuggerCmds.Controls.Add(this.btnRecord, 0, 7);
            this.tblDebuggerCmds.Controls.Add(this.btnSerialize, 0, 8);
            this.tblDebuggerCmds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDebuggerCmds.Enabled = false;
            this.tblDebuggerCmds.Location = new System.Drawing.Point(0, 0);
            this.tblDebuggerCmds.Margin = new System.Windows.Forms.Padding(100);
            this.tblDebuggerCmds.Name = "tblDebuggerCmds";
            this.tblDebuggerCmds.RowCount = 9;
            this.tblDebuggerCmds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11259F));
            this.tblDebuggerCmds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11038F));
            this.tblDebuggerCmds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11038F));
            this.tblDebuggerCmds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11038F));
            this.tblDebuggerCmds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11038F));
            this.tblDebuggerCmds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11038F));
            this.tblDebuggerCmds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11038F));
            this.tblDebuggerCmds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11402F));
            this.tblDebuggerCmds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tblDebuggerCmds.Size = new System.Drawing.Size(278, 265);
            this.tblDebuggerCmds.TabIndex = 0;
            // 
            // btnJumpToLine
            // 
            this.btnJumpToLine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJumpToLine.Location = new System.Drawing.Point(3, 177);
            this.btnJumpToLine.Name = "btnJumpToLine";
            this.btnJumpToLine.Size = new System.Drawing.Size(272, 23);
            this.btnJumpToLine.TabIndex = 5;
            this.btnJumpToLine.Text = "&Go To Line";
            this.btnJumpToLine.UseVisualStyleBackColor = true;
            this.btnJumpToLine.Click += new System.EventHandler(this.btnJumpToLine_Click);
            // 
            // btnStep
            // 
            this.btnStep.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStep.Location = new System.Drawing.Point(3, 61);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(272, 23);
            this.btnStep.TabIndex = 1;
            this.btnStep.Text = "&Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // txtLine
            // 
            this.txtLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLine.Location = new System.Drawing.Point(3, 148);
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(272, 20);
            this.txtLine.TabIndex = 3;
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.Location = new System.Drawing.Point(3, 90);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(272, 23);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "&Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(3, 119);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(272, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "R&eset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(3, 32);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(272, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "&Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnNextFrame
            // 
            this.btnNextFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextFrame.Location = new System.Drawing.Point(3, 3);
            this.btnNextFrame.Name = "btnNextFrame";
            this.btnNextFrame.Size = new System.Drawing.Size(272, 23);
            this.btnNextFrame.TabIndex = 0;
            this.btnNextFrame.Text = "&Next Frame";
            this.btnNextFrame.UseVisualStyleBackColor = true;
            this.btnNextFrame.Click += new System.EventHandler(this.btnNextFrame_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecord.Location = new System.Drawing.Point(3, 206);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(272, 23);
            this.btnRecord.TabIndex = 5;
            this.btnRecord.Text = "&Start Recording";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnSerialize
            // 
            this.btnSerialize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSerialize.Location = new System.Drawing.Point(3, 235);
            this.btnSerialize.Name = "btnSerialize";
            this.btnSerialize.Size = new System.Drawing.Size(272, 27);
            this.btnSerialize.TabIndex = 5;
            this.btnSerialize.Text = "&Serialize Logs";
            this.btnSerialize.UseVisualStyleBackColor = true;
            this.btnSerialize.Click += new System.EventHandler(this.btnSerialize_Click);
            // 
            // tblCPUInfo
            // 
            this.tblCPUInfo.ColumnCount = 2;
            this.tblCPUInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.75758F));
            this.tblCPUInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.24242F));
            this.tblCPUInfo.Controls.Add(this.txtY, 1, 5);
            this.tblCPUInfo.Controls.Add(this.txtX, 1, 4);
            this.tblCPUInfo.Controls.Add(this.txtACC, 1, 3);
            this.tblCPUInfo.Controls.Add(this.txtSP, 1, 2);
            this.tblCPUInfo.Controls.Add(this.txtPS, 1, 1);
            this.tblCPUInfo.Controls.Add(this.lblPC, 0, 0);
            this.tblCPUInfo.Controls.Add(this.lblPS, 0, 1);
            this.tblCPUInfo.Controls.Add(this.lblSP, 0, 2);
            this.tblCPUInfo.Controls.Add(this.lblACC, 0, 3);
            this.tblCPUInfo.Controls.Add(this.lblX, 0, 4);
            this.tblCPUInfo.Controls.Add(this.lblY, 0, 5);
            this.tblCPUInfo.Controls.Add(this.txtPC, 1, 0);
            this.tblCPUInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblCPUInfo.Location = new System.Drawing.Point(0, 0);
            this.tblCPUInfo.Name = "tblCPUInfo";
            this.tblCPUInfo.RowCount = 6;
            this.tblCPUInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblCPUInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblCPUInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblCPUInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblCPUInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblCPUInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblCPUInfo.Size = new System.Drawing.Size(278, 275);
            this.tblCPUInfo.TabIndex = 0;
            // 
            // txtY
            // 
            this.txtY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtY.ForeColor = System.Drawing.Color.Blue;
            this.txtY.Location = new System.Drawing.Point(74, 240);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(201, 20);
            this.txtY.TabIndex = 11;
            this.txtY.Leave += new System.EventHandler(this.txtState_Leave);
            // 
            // txtX
            // 
            this.txtX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtX.ForeColor = System.Drawing.Color.Blue;
            this.txtX.Location = new System.Drawing.Point(74, 192);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(201, 20);
            this.txtX.TabIndex = 10;
            this.txtX.Leave += new System.EventHandler(this.txtState_Leave);
            // 
            // txtACC
            // 
            this.txtACC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtACC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtACC.ForeColor = System.Drawing.Color.Blue;
            this.txtACC.Location = new System.Drawing.Point(74, 147);
            this.txtACC.Name = "txtACC";
            this.txtACC.Size = new System.Drawing.Size(201, 20);
            this.txtACC.TabIndex = 9;
            this.txtACC.Leave += new System.EventHandler(this.txtState_Leave);
            // 
            // txtSP
            // 
            this.txtSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSP.ForeColor = System.Drawing.Color.Blue;
            this.txtSP.Location = new System.Drawing.Point(74, 102);
            this.txtSP.Name = "txtSP";
            this.txtSP.Size = new System.Drawing.Size(201, 20);
            this.txtSP.TabIndex = 8;
            this.txtSP.Leave += new System.EventHandler(this.txtState_Leave);
            // 
            // txtPS
            // 
            this.txtPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPS.ForeColor = System.Drawing.Color.Blue;
            this.txtPS.Location = new System.Drawing.Point(74, 57);
            this.txtPS.Name = "txtPS";
            this.txtPS.Size = new System.Drawing.Size(201, 20);
            this.txtPS.TabIndex = 7;
            this.txtPS.Leave += new System.EventHandler(this.txtState_Leave);
            // 
            // lblPC
            // 
            this.lblPC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPC.AutoSize = true;
            this.lblPC.Location = new System.Drawing.Point(3, 16);
            this.lblPC.Name = "lblPC";
            this.lblPC.Size = new System.Drawing.Size(65, 13);
            this.lblPC.TabIndex = 0;
            this.lblPC.Text = "PC";
            // 
            // lblPS
            // 
            this.lblPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPS.AutoSize = true;
            this.lblPS.Location = new System.Drawing.Point(3, 61);
            this.lblPS.Name = "lblPS";
            this.lblPS.Size = new System.Drawing.Size(65, 13);
            this.lblPS.TabIndex = 1;
            this.lblPS.Text = "PS";
            // 
            // lblSP
            // 
            this.lblSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSP.AutoSize = true;
            this.lblSP.Location = new System.Drawing.Point(3, 106);
            this.lblSP.Name = "lblSP";
            this.lblSP.Size = new System.Drawing.Size(65, 13);
            this.lblSP.TabIndex = 2;
            this.lblSP.Text = "SP";
            // 
            // lblACC
            // 
            this.lblACC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblACC.AutoSize = true;
            this.lblACC.Location = new System.Drawing.Point(3, 151);
            this.lblACC.Name = "lblACC";
            this.lblACC.Size = new System.Drawing.Size(65, 13);
            this.lblACC.TabIndex = 3;
            this.lblACC.Text = "ACC";
            // 
            // lblX
            // 
            this.lblX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(3, 196);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(65, 13);
            this.lblX.TabIndex = 4;
            this.lblX.Text = "X";
            // 
            // lblY
            // 
            this.lblY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(3, 243);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(65, 13);
            this.lblY.TabIndex = 5;
            this.lblY.Text = "Y";
            // 
            // txtPC
            // 
            this.txtPC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPC.ForeColor = System.Drawing.Color.Blue;
            this.txtPC.Location = new System.Drawing.Point(74, 12);
            this.txtPC.Name = "txtPC";
            this.txtPC.Size = new System.Drawing.Size(201, 20);
            this.txtPC.TabIndex = 6;
            this.txtPC.Leave += new System.EventHandler(this.txtState_Leave);
            // 
            // frmDebugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 544);
            this.Controls.Add(this.MainDebugger);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmDebugger";
            this.Text = "NES Debugger";
            this.Load += new System.EventHandler(this.frmDebugger_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmDebugger_KeyPress);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDebugger_FormClosing);
            this.MainDebugger.Panel1.ResumeLayout(false);
            this.MainDebugger.Panel2.ResumeLayout(false);
            this.MainDebugger.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.ctxBreakpoints.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tblDebuggerCmds.ResumeLayout(false);
            this.tblDebuggerCmds.PerformLayout();
            this.tblCPUInfo.ResumeLayout(false);
            this.tblCPUInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainDebugger;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblLoadText;
        private System.Windows.Forms.Button btnLoadCart;
        private System.Windows.Forms.ListBox lstAssembly;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lblLoadedCart;
        private System.Windows.Forms.TableLayoutPanel tblDebuggerCmds;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.Button btnJumpToLine;
        private System.Windows.Forms.ContextMenuStrip ctxBreakpoints;
        private System.Windows.Forms.ToolStripMenuItem mnuAddBreakPoint;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveBreakPoint;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TableLayoutPanel tblCPUInfo;
        private System.Windows.Forms.Label lblPC;
        private System.Windows.Forms.Label lblPS;
        private System.Windows.Forms.Label lblSP;
        private System.Windows.Forms.Label lblACC;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtACC;
        private System.Windows.Forms.TextBox txtSP;
        private System.Windows.Forms.TextBox txtPS;
        private System.Windows.Forms.TextBox txtPC;
        private System.Windows.Forms.Button btnMemory;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox txtErrorConsole;
        private System.Windows.Forms.CheckBox chkUpdateUI;
        private System.Windows.Forms.ComboBox cmbMemory;
        private System.Windows.Forms.Button btnNextFrame;
        private System.Windows.Forms.Label lblNotification;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnSerialize;
    }
}

