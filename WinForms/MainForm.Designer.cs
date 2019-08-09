namespace WinForms
{
    partial class MainForm
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
            this.OpenButton = new System.Windows.Forms.Button();
            this.PortsComboBox = new System.Windows.Forms.ComboBox();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.ReadCoilsButton = new System.Windows.Forms.Button();
            this.WriteCoilsButton = new System.Windows.Forms.Button();
            this.WriteHoldingRegistersButton = new System.Windows.Forms.Button();
            this.ReadInputRegistersButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(12, 39);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 0;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // PortsComboBox
            // 
            this.PortsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PortsComboBox.FormattingEnabled = true;
            this.PortsComboBox.Location = new System.Drawing.Point(12, 12);
            this.PortsComboBox.Name = "PortsComboBox";
            this.PortsComboBox.Size = new System.Drawing.Size(121, 21);
            this.PortsComboBox.TabIndex = 1;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(12, 207);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(776, 202);
            this.LogTextBox.TabIndex = 2;
            // 
            // ReadCoilsButton
            // 
            this.ReadCoilsButton.Enabled = false;
            this.ReadCoilsButton.Location = new System.Drawing.Point(237, 10);
            this.ReadCoilsButton.Name = "ReadCoilsButton";
            this.ReadCoilsButton.Size = new System.Drawing.Size(147, 23);
            this.ReadCoilsButton.TabIndex = 3;
            this.ReadCoilsButton.Text = "Read Coils";
            this.ReadCoilsButton.UseVisualStyleBackColor = true;
            this.ReadCoilsButton.Click += new System.EventHandler(this.ReadCoilsButton_Click);
            // 
            // WriteCoilsButton
            // 
            this.WriteCoilsButton.Enabled = false;
            this.WriteCoilsButton.Location = new System.Drawing.Point(237, 39);
            this.WriteCoilsButton.Name = "WriteCoilsButton";
            this.WriteCoilsButton.Size = new System.Drawing.Size(147, 23);
            this.WriteCoilsButton.TabIndex = 4;
            this.WriteCoilsButton.Text = "Write Coils";
            this.WriteCoilsButton.UseVisualStyleBackColor = true;
            this.WriteCoilsButton.Click += new System.EventHandler(this.WriteCoilsButton_Click);
            // 
            // WriteHoldingRegistersButton
            // 
            this.WriteHoldingRegistersButton.Enabled = false;
            this.WriteHoldingRegistersButton.Location = new System.Drawing.Point(237, 97);
            this.WriteHoldingRegistersButton.Name = "WriteHoldingRegistersButton";
            this.WriteHoldingRegistersButton.Size = new System.Drawing.Size(147, 23);
            this.WriteHoldingRegistersButton.TabIndex = 6;
            this.WriteHoldingRegistersButton.Text = "Write Holding Registers";
            this.WriteHoldingRegistersButton.UseVisualStyleBackColor = true;
            this.WriteHoldingRegistersButton.Click += new System.EventHandler(this.WriteHoldingRegistersButton_Click);
            // 
            // ReadInputRegistersButton
            // 
            this.ReadInputRegistersButton.Enabled = false;
            this.ReadInputRegistersButton.Location = new System.Drawing.Point(237, 68);
            this.ReadInputRegistersButton.Name = "ReadInputRegistersButton";
            this.ReadInputRegistersButton.Size = new System.Drawing.Size(147, 23);
            this.ReadInputRegistersButton.TabIndex = 5;
            this.ReadInputRegistersButton.Text = "Read Input Registers";
            this.ReadInputRegistersButton.UseVisualStyleBackColor = true;
            this.ReadInputRegistersButton.Click += new System.EventHandler(this.ReadInputRegistersButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(713, 415);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 7;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.WriteHoldingRegistersButton);
            this.Controls.Add(this.ReadInputRegistersButton);
            this.Controls.Add(this.WriteCoilsButton);
            this.Controls.Add(this.ReadCoilsButton);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.PortsComboBox);
            this.Controls.Add(this.OpenButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.ComboBox PortsComboBox;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.Button ReadCoilsButton;
        private System.Windows.Forms.Button WriteCoilsButton;
        private System.Windows.Forms.Button WriteHoldingRegistersButton;
        private System.Windows.Forms.Button ReadInputRegistersButton;
        private System.Windows.Forms.Button ClearButton;
    }
}

