using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace DLTLib.Controls.TreeViewComboBox
{
    partial class TreeViewComboBoxForm_Parent
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

        #region Visual WebGui Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CmbTreeView = new Gizmox.WebGUI.Forms.TreeView();
            this.SuspendLayout();
            // 
            // CmbTreeView
            // 
            this.CmbTreeView.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.CmbTreeView.Font = new System.Drawing.Font("ËÎÌו", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbTreeView.Location = new System.Drawing.Point(0, 0);
            this.CmbTreeView.Name = "CmbTreeView";
            this.CmbTreeView.SelectOnRightClick = true;
            this.CmbTreeView.Size = new System.Drawing.Size(235, 181);
            this.CmbTreeView.TabIndex = 0;
            this.CmbTreeView.AfterSelect += new Gizmox.WebGUI.Forms.TreeViewEventHandler(this.CmbTreeView_AfterSelect);
            // 
            // TreeViewComboBoxForm_Parent
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.CmbTreeView);
            this.Size = new System.Drawing.Size(235, 181);
            this.Text = "TreeViewComboBoxForm";
            this.ResumeLayout(false);

        }


        #endregion

        public TreeView CmbTreeView;
    }
}