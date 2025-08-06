namespace ExpenseTracker
{
    partial class PurchasesControl
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
            BtnAdd = new Button();
            NrAmount = new TextBox();
            label2 = new Label();
            label3 = new Label();
            DatePicker = new DateTimePicker();
            TxtShopName = new TextBox();
            label4 = new Label();
            PurchasesGrid = new DataGridView();
            IdCol = new DataGridViewTextBoxColumn();
            ShopCol = new DataGridViewTextBoxColumn();
            AmountCol = new DataGridViewTextBoxColumn();
            label5 = new Label();
            NrTotal = new TextBox();
            LblTitle = new Label();
            BtnDelete = new Button();
            BtnShowToday = new Button();
            label1 = new Label();
            CmbCategory = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)PurchasesGrid).BeginInit();
            SuspendLayout();
            // 
            // BtnAdd
            // 
            BtnAdd.Location = new Point(692, 220);
            BtnAdd.Name = "BtnAdd";
            BtnAdd.Size = new Size(91, 47);
            BtnAdd.TabIndex = 4;
            BtnAdd.Text = "Add";
            BtnAdd.UseVisualStyleBackColor = true;
            BtnAdd.Click += BtnAdd_Click;
            // 
            // NrAmount
            // 
            NrAmount.Location = new Point(533, 244);
            NrAmount.Name = "NrAmount";
            NrAmount.Size = new Size(153, 23);
            NrAmount.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(533, 220);
            label2.Name = "label2";
            label2.Size = new Size(69, 21);
            label2.TabIndex = 4;
            label2.Text = "Amount:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(533, 61);
            label3.Name = "label3";
            label3.Size = new Size(45, 21);
            label3.TabIndex = 5;
            label3.Text = "Date:";
            // 
            // DatePicker
            // 
            DatePicker.Location = new Point(533, 85);
            DatePicker.Name = "DatePicker";
            DatePicker.Size = new Size(250, 23);
            DatePicker.TabIndex = 0;
            DatePicker.ValueChanged += DatePicker_ValueChanged;
            // 
            // TxtShopName
            // 
            TxtShopName.Location = new Point(533, 190);
            TxtShopName.Name = "TxtShopName";
            TxtShopName.Size = new Size(250, 23);
            TxtShopName.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.ForeColor = Color.White;
            label4.Location = new Point(533, 166);
            label4.Name = "label4";
            label4.Size = new Size(95, 21);
            label4.TabIndex = 7;
            label4.Text = "Shop Name:";
            // 
            // PurchasesGrid
            // 
            PurchasesGrid.AllowUserToAddRows = false;
            PurchasesGrid.AllowUserToDeleteRows = false;
            PurchasesGrid.AllowUserToOrderColumns = true;
            PurchasesGrid.BorderStyle = BorderStyle.Fixed3D;
            PurchasesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PurchasesGrid.Columns.AddRange(new DataGridViewColumn[] { IdCol, ShopCol, AmountCol });
            PurchasesGrid.Location = new Point(16, 61);
            PurchasesGrid.Name = "PurchasesGrid";
            PurchasesGrid.ReadOnly = true;
            PurchasesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PurchasesGrid.Size = new Size(511, 560);
            PurchasesGrid.TabIndex = 9;
            PurchasesGrid.TabStop = false;
            PurchasesGrid.SelectionChanged += PurchasesGrid_SelectionChanged;
            // 
            // IdCol
            // 
            IdCol.HeaderText = "Id";
            IdCol.Name = "IdCol";
            IdCol.ReadOnly = true;
            // 
            // ShopCol
            // 
            ShopCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ShopCol.HeaderText = "Shop";
            ShopCol.Name = "ShopCol";
            ShopCol.ReadOnly = true;
            ShopCol.Resizable = DataGridViewTriState.False;
            ShopCol.Width = 200;
            // 
            // AmountCol
            // 
            AmountCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AmountCol.HeaderText = "Amount";
            AmountCol.Name = "AmountCol";
            AmountCol.ReadOnly = true;
            AmountCol.Resizable = DataGridViewTriState.False;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.ForeColor = Color.White;
            label5.Location = new Point(533, 574);
            label5.Name = "label5";
            label5.Size = new Size(45, 21);
            label5.TabIndex = 11;
            label5.Text = "Total:";
            // 
            // NrTotal
            // 
            NrTotal.Location = new Point(533, 598);
            NrTotal.Name = "NrTotal";
            NrTotal.ReadOnly = true;
            NrTotal.Size = new Size(250, 23);
            NrTotal.TabIndex = 10;
            NrTotal.TabStop = false;
            // 
            // LblTitle
            // 
            LblTitle.AutoSize = true;
            LblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            LblTitle.ForeColor = Color.White;
            LblTitle.Location = new Point(19, 22);
            LblTitle.Name = "LblTitle";
            LblTitle.Size = new Size(198, 30);
            LblTitle.TabIndex = 12;
            LblTitle.Text = "Purchases | Today";
            // 
            // BtnDelete
            // 
            BtnDelete.Enabled = false;
            BtnDelete.Location = new Point(533, 279);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(250, 30);
            BtnDelete.TabIndex = 5;
            BtnDelete.Text = "Delete Selected";
            BtnDelete.UseVisualStyleBackColor = true;
            BtnDelete.Click += BtnDelete_Click;
            // 
            // BtnShowToday
            // 
            BtnShowToday.Location = new Point(645, 12);
            BtnShowToday.Name = "BtnShowToday";
            BtnShowToday.Size = new Size(138, 30);
            BtnShowToday.TabIndex = 14;
            BtnShowToday.TabStop = false;
            BtnShowToday.Text = "Show Today";
            BtnShowToday.UseVisualStyleBackColor = true;
            BtnShowToday.Visible = false;
            BtnShowToday.Click += BtnShowToday_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(533, 113);
            label1.Name = "label1";
            label1.Size = new Size(76, 21);
            label1.TabIndex = 16;
            label1.Text = "Category:";
            // 
            // CmbCategory
            // 
            CmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            CmbCategory.FormattingEnabled = true;
            CmbCategory.Items.AddRange(new object[] { "Groceries", "Clothes & Goods", "Entertainment", "Subscriptions", "Rent", "Others" });
            CmbCategory.Location = new Point(533, 140);
            CmbCategory.Name = "CmbCategory";
            CmbCategory.Size = new Size(250, 23);
            CmbCategory.TabIndex = 17;
            // 
            // PurchasesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 41, 64);
            Controls.Add(CmbCategory);
            Controls.Add(label1);
            Controls.Add(BtnShowToday);
            Controls.Add(BtnDelete);
            Controls.Add(LblTitle);
            Controls.Add(label5);
            Controls.Add(NrTotal);
            Controls.Add(PurchasesGrid);
            Controls.Add(TxtShopName);
            Controls.Add(label4);
            Controls.Add(DatePicker);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(NrAmount);
            Controls.Add(BtnAdd);
            Name = "PurchasesControl";
            Size = new Size(794, 637);
            ((System.ComponentModel.ISupportInitialize)PurchasesGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnAdd;
        private TextBox NrAmount;
        private Label label2;
        private Label label3;
        private DateTimePicker DatePicker;
        private TextBox TxtShopName;
        private Label label4;
        private DataGridView PurchasesGrid;
        private Label label5;
        private TextBox NrTotal;
        private Label LblTitle;
        private Button BtnDelete;
        private Button BtnShowToday;
        private DataGridViewTextBoxColumn IdCol;
        private DataGridViewTextBoxColumn ShopCol;
        private DataGridViewTextBoxColumn AmountCol;
        private Label label1;
        private ComboBox CmbCategory;
    }
}
