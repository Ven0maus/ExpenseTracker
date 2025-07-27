namespace ExpenseTracker
{
    partial class SettingsControl
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
            BtnSave = new Button();
            label1 = new Label();
            NrMonthlyBudget = new TextBox();
            SuspendLayout();
            // 
            // BtnSave
            // 
            BtnSave.Location = new Point(29, 82);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(279, 32);
            BtnSave.TabIndex = 0;
            BtnSave.Text = "Save Settings";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(29, 38);
            label1.Name = "label1";
            label1.Size = new Size(124, 21);
            label1.TabIndex = 1;
            label1.Text = "Monthly Budget:";
            // 
            // NrMonthlyBudget
            // 
            NrMonthlyBudget.Location = new Point(159, 40);
            NrMonthlyBudget.Name = "NrMonthlyBudget";
            NrMonthlyBudget.Size = new Size(149, 23);
            NrMonthlyBudget.TabIndex = 2;
            // 
            // SettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 41, 64);
            Controls.Add(NrMonthlyBudget);
            Controls.Add(label1);
            Controls.Add(BtnSave);
            Name = "SettingsControl";
            Size = new Size(763, 637);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnSave;
        private Label label1;
        private TextBox NrMonthlyBudget;
    }
}
