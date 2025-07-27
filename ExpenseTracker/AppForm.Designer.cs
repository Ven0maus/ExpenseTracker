namespace ExpenseTracker
{
    partial class AppForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppForm));
            panel1 = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            BtnDashboard = new Button();
            BtnCalendar = new Button();
            BtnPurchases = new Button();
            BtnSettings = new Button();
            ContentPanel = new Panel();
            panel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(34, 41, 60);
            panel1.Controls.Add(flowLayoutPanel1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(167, 642);
            panel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(BtnDashboard);
            flowLayoutPanel1.Controls.Add(BtnCalendar);
            flowLayoutPanel1.Controls.Add(BtnPurchases);
            flowLayoutPanel1.Controls.Add(BtnSettings);
            flowLayoutPanel1.Dock = DockStyle.Left;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(167, 642);
            flowLayoutPanel1.TabIndex = 4;
            // 
            // BtnDashboard
            // 
            BtnDashboard.BackgroundImageLayout = ImageLayout.None;
            BtnDashboard.FlatAppearance.BorderSize = 0;
            BtnDashboard.FlatStyle = FlatStyle.Flat;
            BtnDashboard.Font = new Font("Modern No. 20", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnDashboard.ForeColor = Color.FromArgb(125, 173, 217);
            BtnDashboard.Image = (Image)resources.GetObject("BtnDashboard.Image");
            BtnDashboard.ImageAlign = ContentAlignment.BottomCenter;
            BtnDashboard.Location = new Point(3, 4);
            BtnDashboard.Margin = new Padding(3, 4, 3, 4);
            BtnDashboard.Name = "BtnDashboard";
            BtnDashboard.Size = new Size(152, 83);
            BtnDashboard.TabIndex = 1;
            BtnDashboard.Text = "Dashboard";
            BtnDashboard.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnDashboard.UseVisualStyleBackColor = true;
            BtnDashboard.Click += BtnDashboard_Click;
            // 
            // BtnCalendar
            // 
            BtnCalendar.BackgroundImageLayout = ImageLayout.None;
            BtnCalendar.FlatAppearance.BorderSize = 0;
            BtnCalendar.FlatStyle = FlatStyle.Flat;
            BtnCalendar.Font = new Font("Modern No. 20", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnCalendar.ForeColor = Color.FromArgb(125, 173, 217);
            BtnCalendar.Image = (Image)resources.GetObject("BtnCalendar.Image");
            BtnCalendar.ImageAlign = ContentAlignment.BottomCenter;
            BtnCalendar.Location = new Point(3, 95);
            BtnCalendar.Margin = new Padding(3, 4, 3, 4);
            BtnCalendar.Name = "BtnCalendar";
            BtnCalendar.Size = new Size(152, 83);
            BtnCalendar.TabIndex = 2;
            BtnCalendar.Text = "Calendar";
            BtnCalendar.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnCalendar.UseVisualStyleBackColor = true;
            BtnCalendar.Click += BtnCalendar_Click;
            // 
            // BtnPurchases
            // 
            BtnPurchases.BackgroundImageLayout = ImageLayout.None;
            BtnPurchases.FlatAppearance.BorderSize = 0;
            BtnPurchases.FlatStyle = FlatStyle.Flat;
            BtnPurchases.Font = new Font("Modern No. 20", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnPurchases.ForeColor = Color.FromArgb(125, 173, 217);
            BtnPurchases.Image = (Image)resources.GetObject("BtnPurchases.Image");
            BtnPurchases.ImageAlign = ContentAlignment.BottomCenter;
            BtnPurchases.Location = new Point(3, 186);
            BtnPurchases.Margin = new Padding(3, 4, 3, 4);
            BtnPurchases.Name = "BtnPurchases";
            BtnPurchases.Size = new Size(152, 83);
            BtnPurchases.TabIndex = 3;
            BtnPurchases.Text = "Purchases";
            BtnPurchases.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnPurchases.UseVisualStyleBackColor = true;
            BtnPurchases.Click += BtnPurchases_Click;
            // 
            // BtnSettings
            // 
            BtnSettings.BackgroundImageLayout = ImageLayout.None;
            BtnSettings.FlatAppearance.BorderSize = 0;
            BtnSettings.FlatStyle = FlatStyle.Flat;
            BtnSettings.Font = new Font("Modern No. 20", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnSettings.ForeColor = Color.FromArgb(125, 173, 217);
            BtnSettings.Image = (Image)resources.GetObject("BtnSettings.Image");
            BtnSettings.ImageAlign = ContentAlignment.BottomCenter;
            BtnSettings.Location = new Point(3, 277);
            BtnSettings.Margin = new Padding(3, 4, 3, 4);
            BtnSettings.Name = "BtnSettings";
            BtnSettings.Size = new Size(152, 83);
            BtnSettings.TabIndex = 4;
            BtnSettings.Text = "Settings";
            BtnSettings.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnSettings.UseVisualStyleBackColor = true;
            BtnSettings.Click += BtnSettings_Click;
            // 
            // ContentPanel
            // 
            ContentPanel.Dock = DockStyle.Fill;
            ContentPanel.Location = new Point(167, 0);
            ContentPanel.Name = "ContentPanel";
            ContentPanel.Size = new Size(794, 642);
            ContentPanel.TabIndex = 1;
            // 
            // AppForm
            // 
            AutoScaleDimensions = new SizeF(8F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 41, 64);
            ClientSize = new Size(961, 642);
            Controls.Add(ContentPanel);
            Controls.Add(panel1);
            Font = new Font("Segoe Print", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.Black;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "AppForm";
            Text = "Expenses Tracker";
            panel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button BtnDashboard;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button BtnCalendar;
        private Button BtnPurchases;
        private Button BtnSettings;
        private Panel ContentPanel;
    }
}
