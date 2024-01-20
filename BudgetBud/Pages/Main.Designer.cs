namespace BudgetBud.Pages
{
    partial class Main
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.exitBtn = new System.Windows.Forms.Button();
            this.maximizeBtn = new System.Windows.Forms.Button();
            this.minimizeBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusText = new System.Windows.Forms.Label();
            this.usernameText = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.navBarContainer = new System.Windows.Forms.Panel();
            this.PnlNav = new System.Windows.Forms.Panel();
            this.logoutBtn = new System.Windows.Forms.Button();
            this.analysisBtn = new System.Windows.Forms.Button();
            this.historyBtn = new System.Windows.Forms.Button();
            this.expenseBtn = new System.Windows.Forms.Button();
            this.categoriesBtn = new System.Windows.Forms.Button();
            this.budgetBtn = new System.Windows.Forms.Button();
            this.homeBtn = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.navBarContainer.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(20)))), ((int)(((byte)(44)))));
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1101, 56);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.Controls.Add(this.exitBtn, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.maximizeBtn, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.minimizeBtn, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(962, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(139, 56);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // exitBtn
            // 
            this.exitBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.exitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.exitBtn.BackgroundImage = global::BudgetBud.Properties.Resources.close;
            this.exitBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.exitBtn.FlatAppearance.BorderSize = 0;
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBtn.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.exitBtn.Location = new System.Drawing.Point(90, 15);
            this.exitBtn.Margin = new System.Windows.Forms.Padding(2);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(25, 25);
            this.exitBtn.TabIndex = 2;
            this.exitBtn.UseVisualStyleBackColor = false;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // maximizeBtn
            // 
            this.maximizeBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maximizeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.maximizeBtn.BackgroundImage = global::BudgetBud.Properties.Resources.maximize;
            this.maximizeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.maximizeBtn.FlatAppearance.BorderSize = 0;
            this.maximizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maximizeBtn.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.maximizeBtn.Location = new System.Drawing.Point(49, 15);
            this.maximizeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.maximizeBtn.Name = "maximizeBtn";
            this.maximizeBtn.Size = new System.Drawing.Size(25, 25);
            this.maximizeBtn.TabIndex = 1;
            this.maximizeBtn.UseVisualStyleBackColor = false;
            this.maximizeBtn.Click += new System.EventHandler(this.maximizeBtn_Click);
            // 
            // minimizeBtn
            // 
            this.minimizeBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.minimizeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.minimizeBtn.BackgroundImage = global::BudgetBud.Properties.Resources.minimize;
            this.minimizeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.minimizeBtn.FlatAppearance.BorderSize = 0;
            this.minimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeBtn.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.minimizeBtn.Location = new System.Drawing.Point(8, 15);
            this.minimizeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(25, 25);
            this.minimizeBtn.TabIndex = 0;
            this.minimizeBtn.UseVisualStyleBackColor = false;
            this.minimizeBtn.Click += new System.EventHandler(this.minimizeBtn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.98547F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.01453F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.contentPanel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 56);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1101, 611);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.navBarContainer, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(176, 611);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel4);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(172, 156);
            this.panel2.TabIndex = 0;
            // 
            // statusText
            // 
            this.statusText.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.statusText.AutoSize = true;
            this.statusText.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.statusText.Location = new System.Drawing.Point(65, 28);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(42, 15);
            this.statusText.TabIndex = 2;
            this.statusText.Text = "Status";
            // 
            // usernameText
            // 
            this.usernameText.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.usernameText.AutoSize = true;
            this.usernameText.Font = new System.Drawing.Font("Nirmala UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.usernameText.Location = new System.Drawing.Point(33, 0);
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(106, 28);
            this.usernameText.TabIndex = 1;
            this.usernameText.Text = "Username";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::BudgetBud.Properties.Resources.user_icon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(172, 96);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // navBarContainer
            // 
            this.navBarContainer.Controls.Add(this.PnlNav);
            this.navBarContainer.Controls.Add(this.logoutBtn);
            this.navBarContainer.Controls.Add(this.analysisBtn);
            this.navBarContainer.Controls.Add(this.historyBtn);
            this.navBarContainer.Controls.Add(this.expenseBtn);
            this.navBarContainer.Controls.Add(this.categoriesBtn);
            this.navBarContainer.Controls.Add(this.budgetBtn);
            this.navBarContainer.Controls.Add(this.homeBtn);
            this.navBarContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarContainer.Location = new System.Drawing.Point(0, 160);
            this.navBarContainer.Margin = new System.Windows.Forms.Padding(0);
            this.navBarContainer.Name = "navBarContainer";
            this.navBarContainer.Size = new System.Drawing.Size(176, 451);
            this.navBarContainer.TabIndex = 1;
            // 
            // PnlNav
            // 
            this.PnlNav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.PnlNav.Location = new System.Drawing.Point(0, 150);
            this.PnlNav.Margin = new System.Windows.Forms.Padding(0);
            this.PnlNav.Name = "PnlNav";
            this.PnlNav.Size = new System.Drawing.Size(4, 56);
            this.PnlNav.TabIndex = 8;
            // 
            // logoutBtn
            // 
            this.logoutBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logoutBtn.FlatAppearance.BorderSize = 0;
            this.logoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logoutBtn.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.logoutBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.logoutBtn.Image = global::BudgetBud.Properties.Resources.logout__2_;
            this.logoutBtn.Location = new System.Drawing.Point(0, 395);
            this.logoutBtn.Margin = new System.Windows.Forms.Padding(2);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(176, 56);
            this.logoutBtn.TabIndex = 7;
            this.logoutBtn.Text = "Logout";
            this.logoutBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.logoutBtn.UseVisualStyleBackColor = true;
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
            // 
            // analysisBtn
            // 
            this.analysisBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.analysisBtn.FlatAppearance.BorderSize = 0;
            this.analysisBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.analysisBtn.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.analysisBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.analysisBtn.Image = global::BudgetBud.Properties.Resources.diagram;
            this.analysisBtn.Location = new System.Drawing.Point(0, 280);
            this.analysisBtn.Margin = new System.Windows.Forms.Padding(2);
            this.analysisBtn.Name = "analysisBtn";
            this.analysisBtn.Size = new System.Drawing.Size(176, 56);
            this.analysisBtn.TabIndex = 6;
            this.analysisBtn.Text = "Analysis";
            this.analysisBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.analysisBtn.UseVisualStyleBackColor = true;
            this.analysisBtn.Click += new System.EventHandler(this.analysisBtn_Click);
            // 
            // historyBtn
            // 
            this.historyBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.historyBtn.FlatAppearance.BorderSize = 0;
            this.historyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.historyBtn.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.historyBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.historyBtn.Image = global::BudgetBud.Properties.Resources.history;
            this.historyBtn.Location = new System.Drawing.Point(0, 224);
            this.historyBtn.Margin = new System.Windows.Forms.Padding(2);
            this.historyBtn.Name = "historyBtn";
            this.historyBtn.Size = new System.Drawing.Size(176, 56);
            this.historyBtn.TabIndex = 5;
            this.historyBtn.Text = "History";
            this.historyBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.historyBtn.UseVisualStyleBackColor = true;
            this.historyBtn.Click += new System.EventHandler(this.historyBtn_Click);
            // 
            // expenseBtn
            // 
            this.expenseBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.expenseBtn.FlatAppearance.BorderSize = 0;
            this.expenseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expenseBtn.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.expenseBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.expenseBtn.Image = global::BudgetBud.Properties.Resources.plus;
            this.expenseBtn.Location = new System.Drawing.Point(0, 168);
            this.expenseBtn.Margin = new System.Windows.Forms.Padding(2);
            this.expenseBtn.Name = "expenseBtn";
            this.expenseBtn.Size = new System.Drawing.Size(176, 56);
            this.expenseBtn.TabIndex = 4;
            this.expenseBtn.Text = "Expense";
            this.expenseBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.expenseBtn.UseVisualStyleBackColor = true;
            this.expenseBtn.Click += new System.EventHandler(this.expenseBtn_Click);
            // 
            // categoriesBtn
            // 
            this.categoriesBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.categoriesBtn.FlatAppearance.BorderSize = 0;
            this.categoriesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.categoriesBtn.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.categoriesBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.categoriesBtn.Image = global::BudgetBud.Properties.Resources.categories2;
            this.categoriesBtn.Location = new System.Drawing.Point(0, 112);
            this.categoriesBtn.Margin = new System.Windows.Forms.Padding(2);
            this.categoriesBtn.Name = "categoriesBtn";
            this.categoriesBtn.Size = new System.Drawing.Size(176, 56);
            this.categoriesBtn.TabIndex = 3;
            this.categoriesBtn.Text = "Categories";
            this.categoriesBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.categoriesBtn.UseVisualStyleBackColor = true;
            this.categoriesBtn.Click += new System.EventHandler(this.categoriesBtn_Click);
            // 
            // budgetBtn
            // 
            this.budgetBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.budgetBtn.FlatAppearance.BorderSize = 0;
            this.budgetBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.budgetBtn.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.budgetBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.budgetBtn.Image = global::BudgetBud.Properties.Resources.budgetpng;
            this.budgetBtn.Location = new System.Drawing.Point(0, 56);
            this.budgetBtn.Margin = new System.Windows.Forms.Padding(2);
            this.budgetBtn.Name = "budgetBtn";
            this.budgetBtn.Size = new System.Drawing.Size(176, 56);
            this.budgetBtn.TabIndex = 2;
            this.budgetBtn.Text = "Budget";
            this.budgetBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.budgetBtn.UseVisualStyleBackColor = true;
            this.budgetBtn.Click += new System.EventHandler(this.budgetBtn_Click);
            // 
            // homeBtn
            // 
            this.homeBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.homeBtn.FlatAppearance.BorderSize = 0;
            this.homeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.homeBtn.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.homeBtn.Image = global::BudgetBud.Properties.Resources.Home_Icon;
            this.homeBtn.Location = new System.Drawing.Point(0, 0);
            this.homeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.homeBtn.Name = "homeBtn";
            this.homeBtn.Size = new System.Drawing.Size(176, 56);
            this.homeBtn.TabIndex = 1;
            this.homeBtn.Text = "Home";
            this.homeBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.homeBtn.UseVisualStyleBackColor = true;
            this.homeBtn.Click += new System.EventHandler(this.homeBtn_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(176, 0);
            this.contentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(925, 611);
            this.contentPanel.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.usernameText, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.statusText, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 96);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(172, 51);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(1101, 667);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.navBarContainer.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button homeBtn;
        private System.Windows.Forms.Panel navBarContainer;
        private System.Windows.Forms.Button logoutBtn;
        private System.Windows.Forms.Button analysisBtn;
        private System.Windows.Forms.Button historyBtn;
        private System.Windows.Forms.Button expenseBtn;
        private System.Windows.Forms.Button categoriesBtn;
        private System.Windows.Forms.Button budgetBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button minimizeBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button maximizeBtn;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel PnlNav;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label usernameText;
        private System.Windows.Forms.Label statusText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}