namespace ExpenseTracker
{
    partial class CalendarControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Calendar = new MonthCalendar();
            BtnViewPurchases = new Button();
            LblTitle = new Label();
            SuspendLayout();
            // 
            // Calendar
            // 
            Calendar.CalendarDimensions = new Size(3, 3);
            Calendar.Font = new Font("Segoe UI", 9F);
            Calendar.Location = new Point(32, 79);
            Calendar.MaxSelectionCount = 999999;
            Calendar.Name = "Calendar";
            Calendar.TabIndex = 0;
            // 
            // BtnViewPurchases
            // 
            BtnViewPurchases.Location = new Point(32, 546);
            BtnViewPurchases.Name = "BtnViewPurchases";
            BtnViewPurchases.Size = new Size(689, 44);
            BtnViewPurchases.TabIndex = 1;
            BtnViewPurchases.Text = "View Purchases Of Selection";
            BtnViewPurchases.UseVisualStyleBackColor = true;
            // 
            // LblTitle
            // 
            LblTitle.AutoSize = true;
            LblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            LblTitle.ForeColor = Color.White;
            LblTitle.Location = new Point(32, 40);
            LblTitle.Name = "LblTitle";
            LblTitle.Size = new Size(105, 30);
            LblTitle.TabIndex = 13;
            LblTitle.Text = "Calendar";
            // 
            // CalendarControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 41, 64);
            Controls.Add(LblTitle);
            Controls.Add(BtnViewPurchases);
            Controls.Add(Calendar);
            Name = "CalendarControl";
            Size = new Size(763, 637);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MonthCalendar Calendar;
        private Button BtnViewPurchases;
        private Label LblTitle;
    }
}
