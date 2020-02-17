namespace WoWViewer
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
            this.m_button = new System.Windows.Forms.Button();
            this.m_listbox = new System.Windows.Forms.ListBox();
            this.m_treeview = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // m_button
            // 
            this.m_button.Location = new System.Drawing.Point(376, 325);
            this.m_button.Name = "m_button";
            this.m_button.Size = new System.Drawing.Size(75, 23);
            this.m_button.TabIndex = 0;
            this.m_button.Text = "button1";
            this.m_button.UseVisualStyleBackColor = true;
            this.m_button.Click += new System.EventHandler(this.m_button_Click);
            // 
            // m_listbox
            // 
            this.m_listbox.FormattingEnabled = true;
            this.m_listbox.Location = new System.Drawing.Point(376, 42);
            this.m_listbox.Name = "m_listbox";
            this.m_listbox.Size = new System.Drawing.Size(357, 277);
            this.m_listbox.TabIndex = 1;
            // 
            // m_treeview
            // 
            this.m_treeview.Location = new System.Drawing.Point(12, 12);
            this.m_treeview.Name = "m_treeview";
            this.m_treeview.Size = new System.Drawing.Size(295, 426);
            this.m_treeview.TabIndex = 2;
            this.m_treeview.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_treeview_BeforeExpand);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_treeview);
            this.Controls.Add(this.m_listbox);
            this.Controls.Add(this.m_button);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_button;
        private System.Windows.Forms.ListBox m_listbox;
        private System.Windows.Forms.TreeView m_treeview;
    }
}

