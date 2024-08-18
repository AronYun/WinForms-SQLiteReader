
namespace SQLiteReader
{
    partial class Reader
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.connectButton = new System.Windows.Forms.Button();
            this.unconnectButton = new System.Windows.Forms.Button();
            this.tableListBox = new System.Windows.Forms.ListBox();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.createButton = new System.Windows.Forms.Button();
            this.scriptTextBox = new System.Windows.Forms.TextBox();
            this.executeButton = new System.Windows.Forms.Button();
            this.resultTabControl = new System.Windows.Forms.TabControl();
            this.resultTabPage = new System.Windows.Forms.TabPage();
            this.tableDataGridView = new System.Windows.Forms.DataGridView();
            this.messageTabPage = new System.Windows.Forms.TabPage();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.resultTabControl.SuspendLayout();
            this.resultTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGridView)).BeginInit();
            this.messageTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.connectButton.AutoSize = true;
            this.connectButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.connectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.connectButton.FlatAppearance.BorderSize = 0;
            this.connectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connectButton.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.connectButton.ForeColor = System.Drawing.Color.Black;
            this.connectButton.Location = new System.Drawing.Point(10, 10);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 29);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "連線";
            this.connectButton.UseVisualStyleBackColor = false;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // unconnectButton
            // 
            this.unconnectButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.unconnectButton.AutoSize = true;
            this.unconnectButton.BackColor = System.Drawing.Color.Salmon;
            this.unconnectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.unconnectButton.Enabled = false;
            this.unconnectButton.FlatAppearance.BorderSize = 0;
            this.unconnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.unconnectButton.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.unconnectButton.ForeColor = System.Drawing.Color.Black;
            this.unconnectButton.Location = new System.Drawing.Point(95, 10);
            this.unconnectButton.Name = "unconnectButton";
            this.unconnectButton.Size = new System.Drawing.Size(75, 29);
            this.unconnectButton.TabIndex = 1;
            this.unconnectButton.Text = "中斷連線";
            this.unconnectButton.UseVisualStyleBackColor = false;
            this.unconnectButton.Click += new System.EventHandler(this.unconnectButton_Click);
            // 
            // tableListBox
            // 
            this.tableListBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableListBox.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tableListBox.FormattingEnabled = true;
            this.tableListBox.ItemHeight = 17;
            this.tableListBox.Location = new System.Drawing.Point(10, 45);
            this.tableListBox.Name = "tableListBox";
            this.tableListBox.Size = new System.Drawing.Size(160, 427);
            this.tableListBox.TabIndex = 2;
            this.tableListBox.DoubleClick += new System.EventHandler(this.tableListBox_DoubleClick);
            this.tableListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tableListBox_MouseUp);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pathTextBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.pathTextBox.Location = new System.Drawing.Point(180, 10);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(571, 29);
            this.pathTextBox.TabIndex = 3;
            this.pathTextBox.WordWrap = false;
            this.pathTextBox.TextChanged += new System.EventHandler(this.pathTextBox_TextChanged);
            // 
            // createButton
            // 
            this.createButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.createButton.AutoSize = true;
            this.createButton.BackColor = System.Drawing.Color.DarkGray;
            this.createButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.createButton.FlatAppearance.BorderSize = 0;
            this.createButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createButton.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.createButton.ForeColor = System.Drawing.Color.Black;
            this.createButton.Location = new System.Drawing.Point(761, 10);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(83, 29);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "建立資料庫";
            this.createButton.UseVisualStyleBackColor = false;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // scriptTextBox
            // 
            this.scriptTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.scriptTextBox.Enabled = false;
            this.scriptTextBox.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.scriptTextBox.Location = new System.Drawing.Point(180, 45);
            this.scriptTextBox.Multiline = true;
            this.scriptTextBox.Name = "scriptTextBox";
            this.scriptTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.scriptTextBox.Size = new System.Drawing.Size(664, 150);
            this.scriptTextBox.TabIndex = 5;
            this.scriptTextBox.WordWrap = false;
            // 
            // executeButton
            // 
            this.executeButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.executeButton.AutoSize = true;
            this.executeButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.executeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.executeButton.Enabled = false;
            this.executeButton.FlatAppearance.BorderSize = 0;
            this.executeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.executeButton.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.executeButton.ForeColor = System.Drawing.Color.Black;
            this.executeButton.Location = new System.Drawing.Point(180, 200);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(664, 27);
            this.executeButton.TabIndex = 6;
            this.executeButton.Text = "▼ 執行 ▼";
            this.executeButton.UseVisualStyleBackColor = false;
            this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
            // 
            // resultTabControl
            // 
            this.resultTabControl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.resultTabControl.Controls.Add(this.resultTabPage);
            this.resultTabControl.Controls.Add(this.messageTabPage);
            this.resultTabControl.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.resultTabControl.Location = new System.Drawing.Point(180, 232);
            this.resultTabControl.Name = "resultTabControl";
            this.resultTabControl.SelectedIndex = 0;
            this.resultTabControl.Size = new System.Drawing.Size(668, 240);
            this.resultTabControl.TabIndex = 7;
            // 
            // resultTabPage
            // 
            this.resultTabPage.Controls.Add(this.tableDataGridView);
            this.resultTabPage.Location = new System.Drawing.Point(4, 24);
            this.resultTabPage.Name = "resultTabPage";
            this.resultTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.resultTabPage.Size = new System.Drawing.Size(660, 212);
            this.resultTabPage.TabIndex = 0;
            this.resultTabPage.Text = "結果";
            this.resultTabPage.UseVisualStyleBackColor = true;
            // 
            // tableDataGridView
            // 
            this.tableDataGridView.AllowUserToAddRows = false;
            this.tableDataGridView.AllowUserToDeleteRows = false;
            this.tableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableDataGridView.Location = new System.Drawing.Point(3, 3);
            this.tableDataGridView.Name = "tableDataGridView";
            this.tableDataGridView.ReadOnly = true;
            this.tableDataGridView.RowTemplate.Height = 24;
            this.tableDataGridView.Size = new System.Drawing.Size(654, 206);
            this.tableDataGridView.TabIndex = 0;
            // 
            // messageTabPage
            // 
            this.messageTabPage.Controls.Add(this.messageTextBox);
            this.messageTabPage.Location = new System.Drawing.Point(4, 24);
            this.messageTabPage.Name = "messageTabPage";
            this.messageTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.messageTabPage.Size = new System.Drawing.Size(660, 212);
            this.messageTabPage.TabIndex = 1;
            this.messageTabPage.Text = "訊息";
            this.messageTabPage.UseVisualStyleBackColor = true;
            // 
            // messageTextBox
            // 
            this.messageTextBox.BackColor = System.Drawing.Color.White;
            this.messageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageTextBox.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.messageTextBox.Location = new System.Drawing.Point(3, 3);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            this.messageTextBox.Size = new System.Drawing.Size(654, 206);
            this.messageTextBox.TabIndex = 0;
            // 
            // Reader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(854, 480);
            this.Controls.Add(this.resultTabControl);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.scriptTextBox);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.tableListBox);
            this.Controls.Add(this.unconnectButton);
            this.Controls.Add(this.connectButton);
            this.Name = "Reader";
            this.ShowIcon = false;
            this.Text = "SQLiteReader";
            this.Load += new System.EventHandler(this.Reader_Load);
            this.Shown += new System.EventHandler(this.Reader_Shown);
            this.Resize += new System.EventHandler(this.Reader_Resize);
            this.resultTabControl.ResumeLayout(false);
            this.resultTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGridView)).EndInit();
            this.messageTabPage.ResumeLayout(false);
            this.messageTabPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button unconnectButton;
        private System.Windows.Forms.ListBox tableListBox;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.TextBox scriptTextBox;
        private System.Windows.Forms.Button executeButton;
        private System.Windows.Forms.TabControl resultTabControl;
        private System.Windows.Forms.TabPage resultTabPage;
        private System.Windows.Forms.TabPage messageTabPage;
        private System.Windows.Forms.DataGridView tableDataGridView;
        private System.Windows.Forms.TextBox messageTextBox;
    }
}

