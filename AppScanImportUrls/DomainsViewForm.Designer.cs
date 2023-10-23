namespace AppScanImportUrls
{
    partial class DomainsViewForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.DomainsListBox = new System.Windows.Forms.ListBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Domains";
            // 
            // DomainsListBox
            // 
            this.DomainsListBox.FormattingEnabled = true;
            this.DomainsListBox.HorizontalScrollbar = true;
            this.DomainsListBox.ItemHeight = 16;
            this.DomainsListBox.Location = new System.Drawing.Point(63, 100);
            this.DomainsListBox.Name = "DomainsListBox";
            this.DomainsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.DomainsListBox.Size = new System.Drawing.Size(574, 340);
            this.DomainsListBox.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(370, 458);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(87, 49);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // DomainsViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 519);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.DomainsListBox);
            this.Controls.Add(this.label1);
            this.Name = "DomainsViewForm";
            this.Text = "DomainsViewForm";
            this.Load += new System.EventHandler(this.DomainsViewForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox DomainsListBox;
        private System.Windows.Forms.Button okButton;
    }
}