namespace RedBlackTreeDb
{
    partial class Form1
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
            this.dbTable = new System.Windows.Forms.DataGridView();
            this.IdTableColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameTableColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.addRowButton = new System.Windows.Forms.Button();
            this.deleteRowButton = new System.Windows.Forms.Button();
            this.searchRowButton = new System.Windows.Forms.Button();
            this.searchInputField = new System.Windows.Forms.TextBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dbTable)).BeginInit();
            this.SuspendLayout();

            // dbTable
            this.dbTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dbTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
        this.IdTableColumn,
        this.NameTableColumn});
            this.dbTable.Location = new System.Drawing.Point(250, 20);
            this.dbTable.Name = "dbTable";
            this.dbTable.RowHeadersWidth = 62;
            this.dbTable.RowTemplate.Height = 28;
            this.dbTable.Size = new System.Drawing.Size(800, 600);
            this.dbTable.TabIndex = 0;
            this.dbTable.AllowUserToAddRows = false;
            this.dbTable.MultiSelect = false;
            this.dbTable.CellValueChanged += dbTable_CellValueChanged;

            // IdTableColumn
            this.IdTableColumn.HeaderText = "Id";
            this.IdTableColumn.MinimumWidth = 8;
            this.IdTableColumn.Name = "IdTableColumn";
            this.IdTableColumn.Width = 150;
            this.IdTableColumn.ReadOnly = true;

            // NameTableColumn
            this.NameTableColumn.HeaderText = "Name";
            this.NameTableColumn.MinimumWidth = 8;
            this.NameTableColumn.Name = "NameTableColumn";
            this.NameTableColumn.Width = 300;

            // buttonPanel
            this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.buttonPanel.Location = new System.Drawing.Point(20, 20);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(200, 600);
            this.buttonPanel.TabIndex = 1;

            // saveChangesButton
            this.saveChangesButton.Text = "Save Changes";
            this.saveChangesButton.Size = new System.Drawing.Size(180, 40);
            this.saveChangesButton.UseVisualStyleBackColor = true;
            this.saveChangesButton.Click += saveChangesButton_Click;
            this.buttonPanel.Controls.Add(this.saveChangesButton);

            // addRowButton
            this.addRowButton.Text = "Add New Entry";
            this.addRowButton.Size = new System.Drawing.Size(180, 40);
            this.addRowButton.UseVisualStyleBackColor = true;
            this.addRowButton.Click += addRowButton_Click;
            this.buttonPanel.Controls.Add(this.addRowButton);

            // deleteRowButton
            this.deleteRowButton.Text = "Delete Entry";
            this.deleteRowButton.Size = new System.Drawing.Size(180, 40);
            this.deleteRowButton.UseVisualStyleBackColor = true;
            this.deleteRowButton.Click += deleteRowButton_Click;
            this.buttonPanel.Controls.Add(this.deleteRowButton);

            // searchInputField
            this.searchInputField.Text = "Enter Id";
            this.searchInputField.Size = new System.Drawing.Size(180, 30);
            this.searchInputField.GotFocus += searchInputField_OnFocus;
            this.searchInputField.LostFocus += searchInputField_OnLeave;
            this.buttonPanel.Controls.Add(this.searchInputField);

            // searchRowButton
            this.searchRowButton.Text = "Search by Id";
            this.searchRowButton.Size = new System.Drawing.Size(180, 40);
            this.searchRowButton.UseVisualStyleBackColor = true;
            this.searchRowButton.Click += searchButton_Click;
            this.buttonPanel.Controls.Add(this.searchRowButton);

            

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 640);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.dbTable);
            this.Name = "Form1";
            this.Text = "RedBlackDB";
            ((System.ComponentModel.ISupportInitialize)(this.dbTable)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dbTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn idTableColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameTableColumn;
        private System.Windows.Forms.Button saveChangesButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTableColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameTableColumn;
        private System.Windows.Forms.Button addRowButton;
        private System.Windows.Forms.Button deleteRowButton;
        private System.Windows.Forms.Button searchRowButton;
        private System.Windows.Forms.TextBox searchInputField;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.FlowLayoutPanel buttonPanel;
    }
}

