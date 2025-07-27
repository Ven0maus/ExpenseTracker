namespace ExpenseTracker
{
    partial class DashboardControl
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
            panel1 = new Panel();
            NrComparedToLastMonth = new Label();
            label2 = new Label();
            NrLastMonthTotal = new Label();
            label4 = new Label();
            NrThisMonthPrediction = new Label();
            label3 = new Label();
            NrThisMonthTotal = new Label();
            label1 = new Label();
            panel2 = new Panel();
            NrAvgYearlySpend = new Label();
            label6 = new Label();
            NrAvgMonthlySpend = new Label();
            label8 = new Label();
            NrAvgWeeklySpend = new Label();
            label10 = new Label();
            NrAvgDailySpend = new Label();
            label12 = new Label();
            panel3 = new Panel();
            NrComparedToLastWeek = new Label();
            label7 = new Label();
            NrLastWeeKTotal = new Label();
            label11 = new Label();
            NrThisWeekPrediction = new Label();
            label14 = new Label();
            NrThisWeekTotal = new Label();
            label16 = new Label();
            panel4 = new Panel();
            NrBudgetLeft = new Label();
            label13 = new Label();
            NrMonthlyBudget = new Label();
            label9 = new Label();
            NrTotalSpend = new Label();
            label20 = new Label();
            panel5 = new Panel();
            NrComparedToLastYear = new Label();
            label15 = new Label();
            NrLastYearTotal = new Label();
            label18 = new Label();
            NrThisYearPrediction = new Label();
            label21 = new Label();
            NrThisYearTotal = new Label();
            label23 = new Label();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            panel6 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            SpendTrendValue5 = new Label();
            SpendTrendValue4 = new Label();
            SpendTrendValue3 = new Label();
            SpendTrendValue2 = new Label();
            SpendTrendValue1 = new Label();
            SpendTrend1 = new Label();
            SpendTrend2 = new Label();
            SpendTrend3 = new Label();
            SpendTrend4 = new Label();
            SpendTrend5 = new Label();
            label27 = new Label();
            panel7 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            BigExpenseValue5 = new Label();
            BigExpenseValue4 = new Label();
            BigExpenseValue3 = new Label();
            BigExpenseValue2 = new Label();
            BigExpenseValue1 = new Label();
            BigExpense1 = new Label();
            BigExpense2 = new Label();
            BigExpense3 = new Label();
            BigExpense4 = new Label();
            BigExpense5 = new Label();
            label31 = new Label();
            panel8 = new Panel();
            label32 = new Label();
            panel9 = new Panel();
            TxtSpendingAnomalies = new Label();
            label33 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel7.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel8.SuspendLayout();
            panel9.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(25, 30, 50);
            panel1.Controls.Add(NrComparedToLastMonth);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(NrLastMonthTotal);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(NrThisMonthPrediction);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(NrThisMonthTotal);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(265, 41);
            panel1.Name = "panel1";
            panel1.Size = new Size(254, 117);
            panel1.TabIndex = 0;
            // 
            // NrComparedToLastMonth
            // 
            NrComparedToLastMonth.AutoSize = true;
            NrComparedToLastMonth.Font = new Font("Segoe UI", 12F);
            NrComparedToLastMonth.ForeColor = SystemColors.Control;
            NrComparedToLastMonth.Location = new Point(192, 88);
            NrComparedToLastMonth.Name = "NrComparedToLastMonth";
            NrComparedToLastMonth.Size = new Size(36, 21);
            NrComparedToLastMonth.TabIndex = 7;
            NrComparedToLastMonth.Text = "0 %";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(5, 87);
            label2.Name = "label2";
            label2.Size = new Size(187, 21);
            label2.TabIndex = 6;
            label2.Text = "Compared To Last Month:";
            // 
            // NrLastMonthTotal
            // 
            NrLastMonthTotal.AutoSize = true;
            NrLastMonthTotal.Font = new Font("Segoe UI", 12F);
            NrLastMonthTotal.ForeColor = SystemColors.Control;
            NrLastMonthTotal.Location = new Point(131, 63);
            NrLastMonthTotal.Name = "NrLastMonthTotal";
            NrLastMonthTotal.Size = new Size(32, 21);
            NrLastMonthTotal.TabIndex = 5;
            NrLastMonthTotal.Text = "€ 0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.ForeColor = SystemColors.Control;
            label4.Location = new Point(5, 62);
            label4.Name = "label4";
            label4.Size = new Size(127, 21);
            label4.TabIndex = 4;
            label4.Text = "Last Month Total:";
            // 
            // NrThisMonthPrediction
            // 
            NrThisMonthPrediction.AutoSize = true;
            NrThisMonthPrediction.Font = new Font("Segoe UI", 12F);
            NrThisMonthPrediction.ForeColor = SystemColors.Control;
            NrThisMonthPrediction.Location = new Point(170, 36);
            NrThisMonthPrediction.Name = "NrThisMonthPrediction";
            NrThisMonthPrediction.Size = new Size(32, 21);
            NrThisMonthPrediction.TabIndex = 3;
            NrThisMonthPrediction.Text = "€ 0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(5, 36);
            label3.Name = "label3";
            label3.Size = new Size(165, 21);
            label3.TabIndex = 2;
            label3.Text = "This Month Prediction:";
            // 
            // NrThisMonthTotal
            // 
            NrThisMonthTotal.AutoSize = true;
            NrThisMonthTotal.Font = new Font("Segoe UI", 12F);
            NrThisMonthTotal.ForeColor = SystemColors.Control;
            NrThisMonthTotal.Location = new Point(131, 9);
            NrThisMonthTotal.Name = "NrThisMonthTotal";
            NrThisMonthTotal.Size = new Size(32, 21);
            NrThisMonthTotal.TabIndex = 1;
            NrThisMonthTotal.Text = "€ 0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(5, 9);
            label1.Name = "label1";
            label1.Size = new Size(127, 21);
            label1.TabIndex = 0;
            label1.Text = "This Month Total:";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(25, 30, 50);
            panel2.Controls.Add(NrAvgYearlySpend);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(NrAvgMonthlySpend);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(NrAvgWeeklySpend);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(NrAvgDailySpend);
            panel2.Controls.Add(label12);
            panel2.Location = new Point(6, 164);
            panel2.Name = "panel2";
            panel2.Size = new Size(254, 117);
            panel2.TabIndex = 8;
            // 
            // NrAvgYearlySpend
            // 
            NrAvgYearlySpend.AutoSize = true;
            NrAvgYearlySpend.Font = new Font("Segoe UI", 12F);
            NrAvgYearlySpend.ForeColor = SystemColors.Control;
            NrAvgYearlySpend.Location = new Point(138, 88);
            NrAvgYearlySpend.Name = "NrAvgYearlySpend";
            NrAvgYearlySpend.Size = new Size(32, 21);
            NrAvgYearlySpend.TabIndex = 7;
            NrAvgYearlySpend.Text = "€ 0";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.ForeColor = SystemColors.Control;
            label6.Location = new Point(5, 87);
            label6.Name = "label6";
            label6.Size = new Size(134, 21);
            label6.TabIndex = 6;
            label6.Text = "Avg Yearly Spend:";
            // 
            // NrAvgMonthlySpend
            // 
            NrAvgMonthlySpend.AutoSize = true;
            NrAvgMonthlySpend.Font = new Font("Segoe UI", 12F);
            NrAvgMonthlySpend.ForeColor = SystemColors.Control;
            NrAvgMonthlySpend.Location = new Point(154, 63);
            NrAvgMonthlySpend.Name = "NrAvgMonthlySpend";
            NrAvgMonthlySpend.Size = new Size(32, 21);
            NrAvgMonthlySpend.TabIndex = 5;
            NrAvgMonthlySpend.Text = "€ 0";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.ForeColor = SystemColors.Control;
            label8.Location = new Point(5, 62);
            label8.Name = "label8";
            label8.Size = new Size(150, 21);
            label8.TabIndex = 4;
            label8.Text = "Avg Monthly Spend:";
            // 
            // NrAvgWeeklySpend
            // 
            NrAvgWeeklySpend.AutoSize = true;
            NrAvgWeeklySpend.Font = new Font("Segoe UI", 12F);
            NrAvgWeeklySpend.ForeColor = SystemColors.Control;
            NrAvgWeeklySpend.Location = new Point(147, 36);
            NrAvgWeeklySpend.Name = "NrAvgWeeklySpend";
            NrAvgWeeklySpend.Size = new Size(32, 21);
            NrAvgWeeklySpend.TabIndex = 3;
            NrAvgWeeklySpend.Text = "€ 0";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F);
            label10.ForeColor = SystemColors.Control;
            label10.Location = new Point(5, 36);
            label10.Name = "label10";
            label10.Size = new Size(142, 21);
            label10.TabIndex = 2;
            label10.Text = "Avg Weekly Spend:";
            // 
            // NrAvgDailySpend
            // 
            NrAvgDailySpend.AutoSize = true;
            NrAvgDailySpend.Font = new Font("Segoe UI", 12F);
            NrAvgDailySpend.ForeColor = SystemColors.Control;
            NrAvgDailySpend.Location = new Point(131, 9);
            NrAvgDailySpend.Name = "NrAvgDailySpend";
            NrAvgDailySpend.Size = new Size(32, 21);
            NrAvgDailySpend.TabIndex = 1;
            NrAvgDailySpend.Text = "€ 0";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 12F);
            label12.ForeColor = SystemColors.Control;
            label12.Location = new Point(5, 9);
            label12.Name = "label12";
            label12.Size = new Size(127, 21);
            label12.TabIndex = 0;
            label12.Text = "Avg Daily Spend:";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(25, 30, 50);
            panel3.Controls.Add(NrComparedToLastWeek);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(NrLastWeeKTotal);
            panel3.Controls.Add(label11);
            panel3.Controls.Add(NrThisWeekPrediction);
            panel3.Controls.Add(label14);
            panel3.Controls.Add(NrThisWeekTotal);
            panel3.Controls.Add(label16);
            panel3.Location = new Point(6, 41);
            panel3.Name = "panel3";
            panel3.Size = new Size(254, 117);
            panel3.TabIndex = 8;
            // 
            // NrComparedToLastWeek
            // 
            NrComparedToLastWeek.AutoSize = true;
            NrComparedToLastWeek.Font = new Font("Segoe UI", 12F);
            NrComparedToLastWeek.ForeColor = SystemColors.Control;
            NrComparedToLastWeek.Location = new Point(183, 88);
            NrComparedToLastWeek.Name = "NrComparedToLastWeek";
            NrComparedToLastWeek.Size = new Size(36, 21);
            NrComparedToLastWeek.TabIndex = 7;
            NrComparedToLastWeek.Text = "0 %";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.ForeColor = SystemColors.Control;
            label7.Location = new Point(5, 87);
            label7.Name = "label7";
            label7.Size = new Size(179, 21);
            label7.TabIndex = 6;
            label7.Text = "Compared To Last Week:";
            // 
            // NrLastWeeKTotal
            // 
            NrLastWeeKTotal.AutoSize = true;
            NrLastWeeKTotal.Font = new Font("Segoe UI", 12F);
            NrLastWeeKTotal.ForeColor = SystemColors.Control;
            NrLastWeeKTotal.Location = new Point(123, 63);
            NrLastWeeKTotal.Name = "NrLastWeeKTotal";
            NrLastWeeKTotal.Size = new Size(32, 21);
            NrLastWeeKTotal.TabIndex = 5;
            NrLastWeeKTotal.Text = "€ 0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F);
            label11.ForeColor = SystemColors.Control;
            label11.Location = new Point(5, 62);
            label11.Name = "label11";
            label11.Size = new Size(119, 21);
            label11.TabIndex = 4;
            label11.Text = "Last Week Total:";
            // 
            // NrThisWeekPrediction
            // 
            NrThisWeekPrediction.AutoSize = true;
            NrThisWeekPrediction.Font = new Font("Segoe UI", 12F);
            NrThisWeekPrediction.ForeColor = SystemColors.Control;
            NrThisWeekPrediction.Location = new Point(161, 36);
            NrThisWeekPrediction.Name = "NrThisWeekPrediction";
            NrThisWeekPrediction.Size = new Size(32, 21);
            NrThisWeekPrediction.TabIndex = 3;
            NrThisWeekPrediction.Text = "€ 0";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 12F);
            label14.ForeColor = SystemColors.Control;
            label14.Location = new Point(5, 36);
            label14.Name = "label14";
            label14.Size = new Size(157, 21);
            label14.TabIndex = 2;
            label14.Text = "This Week Prediction:";
            // 
            // NrThisWeekTotal
            // 
            NrThisWeekTotal.AutoSize = true;
            NrThisWeekTotal.Font = new Font("Segoe UI", 12F);
            NrThisWeekTotal.ForeColor = SystemColors.Control;
            NrThisWeekTotal.Location = new Point(123, 9);
            NrThisWeekTotal.Name = "NrThisWeekTotal";
            NrThisWeekTotal.Size = new Size(32, 21);
            NrThisWeekTotal.TabIndex = 1;
            NrThisWeekTotal.Text = "€ 0";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 12F);
            label16.ForeColor = SystemColors.Control;
            label16.Location = new Point(5, 9);
            label16.Name = "label16";
            label16.Size = new Size(119, 21);
            label16.TabIndex = 0;
            label16.Text = "This Week Total:";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(25, 30, 50);
            panel4.Controls.Add(NrBudgetLeft);
            panel4.Controls.Add(label13);
            panel4.Controls.Add(NrMonthlyBudget);
            panel4.Controls.Add(label9);
            panel4.Controls.Add(NrTotalSpend);
            panel4.Controls.Add(label20);
            panel4.Location = new Point(6, 7);
            panel4.Name = "panel4";
            panel4.Size = new Size(750, 28);
            panel4.TabIndex = 9;
            // 
            // NrBudgetLeft
            // 
            NrBudgetLeft.AutoSize = true;
            NrBudgetLeft.Font = new Font("Segoe UI", 12F);
            NrBudgetLeft.ForeColor = SystemColors.Control;
            NrBudgetLeft.Location = new Point(614, 4);
            NrBudgetLeft.Name = "NrBudgetLeft";
            NrBudgetLeft.Size = new Size(32, 21);
            NrBudgetLeft.TabIndex = 5;
            NrBudgetLeft.Text = "€ 0";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 12F);
            label13.ForeColor = SystemColors.Control;
            label13.Location = new Point(523, 4);
            label13.Name = "label13";
            label13.Size = new Size(92, 21);
            label13.TabIndex = 4;
            label13.Text = "Budget Left:";
            // 
            // NrMonthlyBudget
            // 
            NrMonthlyBudget.AutoSize = true;
            NrMonthlyBudget.Font = new Font("Segoe UI", 12F);
            NrMonthlyBudget.ForeColor = SystemColors.Control;
            NrMonthlyBudget.Location = new Point(385, 4);
            NrMonthlyBudget.Name = "NrMonthlyBudget";
            NrMonthlyBudget.Size = new Size(32, 21);
            NrMonthlyBudget.TabIndex = 3;
            NrMonthlyBudget.Text = "€ 0";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F);
            label9.ForeColor = SystemColors.Control;
            label9.Location = new Point(262, 4);
            label9.Name = "label9";
            label9.Size = new Size(124, 21);
            label9.TabIndex = 2;
            label9.Text = "Monthly Budget:";
            // 
            // NrTotalSpend
            // 
            NrTotalSpend.AutoSize = true;
            NrTotalSpend.Font = new Font("Segoe UI", 12F);
            NrTotalSpend.ForeColor = SystemColors.Control;
            NrTotalSpend.Location = new Point(93, 4);
            NrTotalSpend.Name = "NrTotalSpend";
            NrTotalSpend.Size = new Size(32, 21);
            NrTotalSpend.TabIndex = 1;
            NrTotalSpend.Text = "€ 0";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 12F);
            label20.ForeColor = SystemColors.Control;
            label20.Location = new Point(5, 4);
            label20.Name = "label20";
            label20.Size = new Size(89, 21);
            label20.TabIndex = 0;
            label20.Text = "Total Spent:";
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(25, 30, 50);
            panel5.Controls.Add(NrComparedToLastYear);
            panel5.Controls.Add(label15);
            panel5.Controls.Add(NrLastYearTotal);
            panel5.Controls.Add(label18);
            panel5.Controls.Add(NrThisYearPrediction);
            panel5.Controls.Add(label21);
            panel5.Controls.Add(NrThisYearTotal);
            panel5.Controls.Add(label23);
            panel5.Location = new Point(525, 41);
            panel5.Name = "panel5";
            panel5.Size = new Size(231, 117);
            panel5.TabIndex = 8;
            // 
            // NrComparedToLastYear
            // 
            NrComparedToLastYear.AutoSize = true;
            NrComparedToLastYear.Font = new Font("Segoe UI", 12F);
            NrComparedToLastYear.ForeColor = SystemColors.Control;
            NrComparedToLastYear.Location = new Point(175, 88);
            NrComparedToLastYear.Name = "NrComparedToLastYear";
            NrComparedToLastYear.Size = new Size(36, 21);
            NrComparedToLastYear.TabIndex = 7;
            NrComparedToLastYear.Text = "0 %";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 12F);
            label15.ForeColor = SystemColors.Control;
            label15.Location = new Point(5, 87);
            label15.Name = "label15";
            label15.Size = new Size(171, 21);
            label15.TabIndex = 6;
            label15.Text = "Compared To Last Year:";
            // 
            // NrLastYearTotal
            // 
            NrLastYearTotal.AutoSize = true;
            NrLastYearTotal.Font = new Font("Segoe UI", 12F);
            NrLastYearTotal.ForeColor = SystemColors.Control;
            NrLastYearTotal.Location = new Point(115, 63);
            NrLastYearTotal.Name = "NrLastYearTotal";
            NrLastYearTotal.Size = new Size(32, 21);
            NrLastYearTotal.TabIndex = 5;
            NrLastYearTotal.Text = "€ 0";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 12F);
            label18.ForeColor = SystemColors.Control;
            label18.Location = new Point(5, 62);
            label18.Name = "label18";
            label18.Size = new Size(111, 21);
            label18.TabIndex = 4;
            label18.Text = "Last Year Total:";
            // 
            // NrThisYearPrediction
            // 
            NrThisYearPrediction.AutoSize = true;
            NrThisYearPrediction.Font = new Font("Segoe UI", 12F);
            NrThisYearPrediction.ForeColor = SystemColors.Control;
            NrThisYearPrediction.Location = new Point(153, 36);
            NrThisYearPrediction.Name = "NrThisYearPrediction";
            NrThisYearPrediction.Size = new Size(32, 21);
            NrThisYearPrediction.TabIndex = 3;
            NrThisYearPrediction.Text = "€ 0";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Segoe UI", 12F);
            label21.ForeColor = SystemColors.Control;
            label21.Location = new Point(5, 36);
            label21.Name = "label21";
            label21.Size = new Size(149, 21);
            label21.TabIndex = 2;
            label21.Text = "This Year Prediction:";
            // 
            // NrThisYearTotal
            // 
            NrThisYearTotal.AutoSize = true;
            NrThisYearTotal.Font = new Font("Segoe UI", 12F);
            NrThisYearTotal.ForeColor = SystemColors.Control;
            NrThisYearTotal.Location = new Point(115, 9);
            NrThisYearTotal.Name = "NrThisYearTotal";
            NrThisYearTotal.Size = new Size(32, 21);
            NrThisYearTotal.TabIndex = 1;
            NrThisYearTotal.Text = "€ 0";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Segoe UI", 12F);
            label23.ForeColor = SystemColors.Control;
            label23.Location = new Point(5, 9);
            label23.Name = "label23";
            label23.Size = new Size(111, 21);
            label23.TabIndex = 0;
            label23.Text = "This Year Total:";
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(25, 30, 50);
            panel6.Controls.Add(tableLayoutPanel1);
            panel6.Controls.Add(label27);
            panel6.Location = new Point(266, 197);
            panel6.Name = "panel6";
            panel6.Size = new Size(253, 202);
            panel6.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.Controls.Add(SpendTrendValue5, 1, 4);
            tableLayoutPanel1.Controls.Add(SpendTrendValue4, 1, 3);
            tableLayoutPanel1.Controls.Add(SpendTrendValue3, 1, 2);
            tableLayoutPanel1.Controls.Add(SpendTrendValue2, 1, 1);
            tableLayoutPanel1.Controls.Add(SpendTrendValue1, 1, 0);
            tableLayoutPanel1.Controls.Add(SpendTrend1, 0, 0);
            tableLayoutPanel1.Controls.Add(SpendTrend2, 0, 1);
            tableLayoutPanel1.Controls.Add(SpendTrend3, 0, 2);
            tableLayoutPanel1.Controls.Add(SpendTrend4, 0, 3);
            tableLayoutPanel1.Controls.Add(SpendTrend5, 0, 4);
            tableLayoutPanel1.Location = new Point(5, 47);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(238, 147);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // SpendTrendValue5
            // 
            SpendTrendValue5.AutoSize = true;
            SpendTrendValue5.Font = new Font("Segoe UI", 12F);
            SpendTrendValue5.ForeColor = SystemColors.Control;
            SpendTrendValue5.Location = new Point(169, 120);
            SpendTrendValue5.Name = "SpendTrendValue5";
            SpendTrendValue5.Size = new Size(22, 21);
            SpendTrendValue5.TabIndex = 17;
            SpendTrendValue5.Text = "5.";
            // 
            // SpendTrendValue4
            // 
            SpendTrendValue4.AutoSize = true;
            SpendTrendValue4.Font = new Font("Segoe UI", 12F);
            SpendTrendValue4.ForeColor = SystemColors.Control;
            SpendTrendValue4.Location = new Point(169, 90);
            SpendTrendValue4.Name = "SpendTrendValue4";
            SpendTrendValue4.Size = new Size(22, 21);
            SpendTrendValue4.TabIndex = 16;
            SpendTrendValue4.Text = "4.";
            // 
            // SpendTrendValue3
            // 
            SpendTrendValue3.AutoSize = true;
            SpendTrendValue3.Font = new Font("Segoe UI", 12F);
            SpendTrendValue3.ForeColor = SystemColors.Control;
            SpendTrendValue3.Location = new Point(169, 60);
            SpendTrendValue3.Name = "SpendTrendValue3";
            SpendTrendValue3.Size = new Size(22, 21);
            SpendTrendValue3.TabIndex = 15;
            SpendTrendValue3.Text = "3.";
            // 
            // SpendTrendValue2
            // 
            SpendTrendValue2.AutoSize = true;
            SpendTrendValue2.Font = new Font("Segoe UI", 12F);
            SpendTrendValue2.ForeColor = SystemColors.Control;
            SpendTrendValue2.Location = new Point(169, 30);
            SpendTrendValue2.Name = "SpendTrendValue2";
            SpendTrendValue2.Size = new Size(22, 21);
            SpendTrendValue2.TabIndex = 14;
            SpendTrendValue2.Text = "2.";
            // 
            // SpendTrendValue1
            // 
            SpendTrendValue1.AutoSize = true;
            SpendTrendValue1.Font = new Font("Segoe UI", 12F);
            SpendTrendValue1.ForeColor = SystemColors.Control;
            SpendTrendValue1.Location = new Point(169, 0);
            SpendTrendValue1.Name = "SpendTrendValue1";
            SpendTrendValue1.Size = new Size(22, 21);
            SpendTrendValue1.TabIndex = 13;
            SpendTrendValue1.Text = "1.";
            // 
            // SpendTrend1
            // 
            SpendTrend1.AutoSize = true;
            SpendTrend1.Font = new Font("Segoe UI", 12F);
            SpendTrend1.ForeColor = SystemColors.Control;
            SpendTrend1.Location = new Point(3, 0);
            SpendTrend1.Name = "SpendTrend1";
            SpendTrend1.Size = new Size(22, 21);
            SpendTrend1.TabIndex = 8;
            SpendTrend1.Text = "1.";
            // 
            // SpendTrend2
            // 
            SpendTrend2.AutoSize = true;
            SpendTrend2.Font = new Font("Segoe UI", 12F);
            SpendTrend2.ForeColor = SystemColors.Control;
            SpendTrend2.Location = new Point(3, 30);
            SpendTrend2.Name = "SpendTrend2";
            SpendTrend2.Size = new Size(22, 21);
            SpendTrend2.TabIndex = 9;
            SpendTrend2.Text = "2.";
            // 
            // SpendTrend3
            // 
            SpendTrend3.AutoSize = true;
            SpendTrend3.Font = new Font("Segoe UI", 12F);
            SpendTrend3.ForeColor = SystemColors.Control;
            SpendTrend3.Location = new Point(3, 60);
            SpendTrend3.Name = "SpendTrend3";
            SpendTrend3.Size = new Size(22, 21);
            SpendTrend3.TabIndex = 10;
            SpendTrend3.Text = "3.";
            // 
            // SpendTrend4
            // 
            SpendTrend4.AutoSize = true;
            SpendTrend4.Font = new Font("Segoe UI", 12F);
            SpendTrend4.ForeColor = SystemColors.Control;
            SpendTrend4.Location = new Point(3, 90);
            SpendTrend4.Name = "SpendTrend4";
            SpendTrend4.Size = new Size(22, 21);
            SpendTrend4.TabIndex = 11;
            SpendTrend4.Text = "4.";
            // 
            // SpendTrend5
            // 
            SpendTrend5.AutoSize = true;
            SpendTrend5.Font = new Font("Segoe UI", 12F);
            SpendTrend5.ForeColor = SystemColors.Control;
            SpendTrend5.Location = new Point(3, 120);
            SpendTrend5.Name = "SpendTrend5";
            SpendTrend5.Size = new Size(22, 21);
            SpendTrend5.TabIndex = 12;
            SpendTrend5.Text = "5.";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("PMingLiU-ExtB", 18F, FontStyle.Bold);
            label27.ForeColor = SystemColors.Control;
            label27.Location = new Point(5, 9);
            label27.Name = "label27";
            label27.Size = new Size(199, 24);
            label27.TabIndex = 0;
            label27.Text = "Category summary";
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(25, 30, 50);
            panel7.Controls.Add(tableLayoutPanel2);
            panel7.Controls.Add(label31);
            panel7.Location = new Point(525, 197);
            panel7.Name = "panel7";
            panel7.Size = new Size(231, 202);
            panel7.TabIndex = 10;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel2.Controls.Add(BigExpenseValue5, 1, 4);
            tableLayoutPanel2.Controls.Add(BigExpenseValue4, 1, 3);
            tableLayoutPanel2.Controls.Add(BigExpenseValue3, 1, 2);
            tableLayoutPanel2.Controls.Add(BigExpenseValue2, 1, 1);
            tableLayoutPanel2.Controls.Add(BigExpenseValue1, 1, 0);
            tableLayoutPanel2.Controls.Add(BigExpense1, 0, 0);
            tableLayoutPanel2.Controls.Add(BigExpense2, 0, 1);
            tableLayoutPanel2.Controls.Add(BigExpense3, 0, 2);
            tableLayoutPanel2.Controls.Add(BigExpense4, 0, 3);
            tableLayoutPanel2.Controls.Add(BigExpense5, 0, 4);
            tableLayoutPanel2.Location = new Point(5, 47);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(223, 147);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // BigExpenseValue5
            // 
            BigExpenseValue5.AutoSize = true;
            BigExpenseValue5.Font = new Font("Segoe UI", 12F);
            BigExpenseValue5.ForeColor = SystemColors.Control;
            BigExpenseValue5.Location = new Point(159, 120);
            BigExpenseValue5.Name = "BigExpenseValue5";
            BigExpenseValue5.Size = new Size(22, 21);
            BigExpenseValue5.TabIndex = 17;
            BigExpenseValue5.Text = "5.";
            // 
            // BigExpenseValue4
            // 
            BigExpenseValue4.AutoSize = true;
            BigExpenseValue4.Font = new Font("Segoe UI", 12F);
            BigExpenseValue4.ForeColor = SystemColors.Control;
            BigExpenseValue4.Location = new Point(159, 90);
            BigExpenseValue4.Name = "BigExpenseValue4";
            BigExpenseValue4.Size = new Size(22, 21);
            BigExpenseValue4.TabIndex = 16;
            BigExpenseValue4.Text = "4.";
            // 
            // BigExpenseValue3
            // 
            BigExpenseValue3.AutoSize = true;
            BigExpenseValue3.Font = new Font("Segoe UI", 12F);
            BigExpenseValue3.ForeColor = SystemColors.Control;
            BigExpenseValue3.Location = new Point(159, 60);
            BigExpenseValue3.Name = "BigExpenseValue3";
            BigExpenseValue3.Size = new Size(22, 21);
            BigExpenseValue3.TabIndex = 15;
            BigExpenseValue3.Text = "3.";
            // 
            // BigExpenseValue2
            // 
            BigExpenseValue2.AutoSize = true;
            BigExpenseValue2.Font = new Font("Segoe UI", 12F);
            BigExpenseValue2.ForeColor = SystemColors.Control;
            BigExpenseValue2.Location = new Point(159, 30);
            BigExpenseValue2.Name = "BigExpenseValue2";
            BigExpenseValue2.Size = new Size(22, 21);
            BigExpenseValue2.TabIndex = 14;
            BigExpenseValue2.Text = "2.";
            // 
            // BigExpenseValue1
            // 
            BigExpenseValue1.AutoSize = true;
            BigExpenseValue1.Font = new Font("Segoe UI", 12F);
            BigExpenseValue1.ForeColor = SystemColors.Control;
            BigExpenseValue1.Location = new Point(159, 0);
            BigExpenseValue1.Name = "BigExpenseValue1";
            BigExpenseValue1.Size = new Size(22, 21);
            BigExpenseValue1.TabIndex = 13;
            BigExpenseValue1.Text = "1.";
            // 
            // BigExpense1
            // 
            BigExpense1.AutoSize = true;
            BigExpense1.Font = new Font("Segoe UI", 12F);
            BigExpense1.ForeColor = SystemColors.Control;
            BigExpense1.Location = new Point(3, 0);
            BigExpense1.Name = "BigExpense1";
            BigExpense1.Size = new Size(22, 21);
            BigExpense1.TabIndex = 8;
            BigExpense1.Text = "1.";
            // 
            // BigExpense2
            // 
            BigExpense2.AutoSize = true;
            BigExpense2.Font = new Font("Segoe UI", 12F);
            BigExpense2.ForeColor = SystemColors.Control;
            BigExpense2.Location = new Point(3, 30);
            BigExpense2.Name = "BigExpense2";
            BigExpense2.Size = new Size(22, 21);
            BigExpense2.TabIndex = 9;
            BigExpense2.Text = "2.";
            // 
            // BigExpense3
            // 
            BigExpense3.AutoSize = true;
            BigExpense3.Font = new Font("Segoe UI", 12F);
            BigExpense3.ForeColor = SystemColors.Control;
            BigExpense3.Location = new Point(3, 60);
            BigExpense3.Name = "BigExpense3";
            BigExpense3.Size = new Size(22, 21);
            BigExpense3.TabIndex = 10;
            BigExpense3.Text = "3.";
            // 
            // BigExpense4
            // 
            BigExpense4.AutoSize = true;
            BigExpense4.Font = new Font("Segoe UI", 12F);
            BigExpense4.ForeColor = SystemColors.Control;
            BigExpense4.Location = new Point(3, 90);
            BigExpense4.Name = "BigExpense4";
            BigExpense4.Size = new Size(22, 21);
            BigExpense4.TabIndex = 11;
            BigExpense4.Text = "4.";
            // 
            // BigExpense5
            // 
            BigExpense5.AutoSize = true;
            BigExpense5.Font = new Font("Segoe UI", 12F);
            BigExpense5.ForeColor = SystemColors.Control;
            BigExpense5.Location = new Point(3, 120);
            BigExpense5.Name = "BigExpense5";
            BigExpense5.Size = new Size(22, 21);
            BigExpense5.TabIndex = 12;
            BigExpense5.Text = "5.";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("PMingLiU-ExtB", 18F, FontStyle.Bold);
            label31.ForeColor = SystemColors.Control;
            label31.Location = new Point(5, 9);
            label31.Name = "label31";
            label31.Size = new Size(185, 24);
            label31.TabIndex = 0;
            label31.Text = "Biggest Expenses";
            // 
            // panel8
            // 
            panel8.BackColor = Color.FromArgb(25, 30, 50);
            panel8.Controls.Add(label32);
            panel8.Location = new Point(265, 164);
            panel8.Name = "panel8";
            panel8.Size = new Size(491, 28);
            panel8.TabIndex = 10;
            // 
            // label32
            // 
            label32.BackColor = Color.Transparent;
            label32.Font = new Font("PMingLiU-ExtB", 16F, FontStyle.Bold);
            label32.ForeColor = SystemColors.Control;
            label32.Location = new Point(0, 0);
            label32.Name = "label32";
            label32.Size = new Size(491, 28);
            label32.TabIndex = 2;
            label32.Text = "This Month's Trend Information";
            label32.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel9
            // 
            panel9.BackColor = Color.FromArgb(25, 30, 50);
            panel9.Controls.Add(TxtSpendingAnomalies);
            panel9.Controls.Add(label33);
            panel9.Location = new Point(6, 287);
            panel9.Name = "panel9";
            panel9.Size = new Size(254, 112);
            panel9.TabIndex = 9;
            // 
            // TxtSpendingAnomalies
            // 
            TxtSpendingAnomalies.Font = new Font("Segoe UI", 11F);
            TxtSpendingAnomalies.ForeColor = SystemColors.Control;
            TxtSpendingAnomalies.Location = new Point(4, 36);
            TxtSpendingAnomalies.Name = "TxtSpendingAnomalies";
            TxtSpendingAnomalies.Size = new Size(246, 69);
            TxtSpendingAnomalies.TabIndex = 8;
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new Font("PMingLiU-ExtB", 15F, FontStyle.Bold);
            label33.ForeColor = SystemColors.Control;
            label33.Location = new Point(5, 7);
            label33.Name = "label33";
            label33.Size = new Size(180, 20);
            label33.TabIndex = 2;
            label33.Text = "Spending Anomalies";
            // 
            // DashboardControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 41, 64);
            Controls.Add(panel9);
            Controls.Add(panel8);
            Controls.Add(panel7);
            Controls.Add(panel6);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "DashboardControl";
            Size = new Size(763, 637);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panel8.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label NrThisMonthPrediction;
        private Label label3;
        private Label NrThisMonthTotal;
        private Label label1;
        private Label label4;
        private Label NrComparedToLastMonth;
        private Label label2;
        private Label NrLastMonthTotal;
        private Panel panel2;
        private Label NrAvgYearlySpend;
        private Label label6;
        private Label NrAvgMonthlySpend;
        private Label label8;
        private Label NrAvgWeeklySpend;
        private Label label10;
        private Label NrAvgDailySpend;
        private Label label12;
        private Panel panel3;
        private Label NrComparedToLastWeek;
        private Label label7;
        private Label NrLastWeeKTotal;
        private Label label11;
        private Label NrThisWeekPrediction;
        private Label label14;
        private Label NrThisWeekTotal;
        private Label label16;
        private Panel panel4;
        private Label NrTotalSpend;
        private Label label20;
        private Label NrBudgetLeft;
        private Label label13;
        private Label NrMonthlyBudget;
        private Label label9;
        private Panel panel5;
        private Label NrComparedToLastYear;
        private Label label15;
        private Label NrLastYearTotal;
        private Label label18;
        private Label NrThisYearPrediction;
        private Label label21;
        private Label NrThisYearTotal;
        private Label label23;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
        private Panel panel6;
        private Label label27;
        private TableLayoutPanel tableLayoutPanel1;
        private Label SpendTrend1;
        private Label SpendTrend2;
        private Label SpendTrend3;
        private Label SpendTrend4;
        private Label SpendTrend5;
        private Label SpendTrendValue5;
        private Label SpendTrendValue4;
        private Label SpendTrendValue3;
        private Label SpendTrendValue2;
        private Label SpendTrendValue1;
        private Panel panel7;
        private TableLayoutPanel tableLayoutPanel2;
        private Label BigExpenseValue5;
        private Label BigExpenseValue4;
        private Label BigExpenseValue3;
        private Label BigExpenseValue2;
        private Label BigExpenseValue1;
        private Label BigExpense1;
        private Label BigExpense2;
        private Label BigExpense3;
        private Label BigExpense4;
        private Label BigExpense5;
        private Label label31;
        private Panel panel8;
        private Label label32;
        private Panel panel9;
        private Label TxtSpendingAnomalies;
        private Label label33;
    }
}
