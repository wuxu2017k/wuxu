#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

#endregion

namespace DLTLib.Controls.TreeViewComboBox
{
    public partial class TreeViewComboBoxForm : Form
    {
        public TreeViewComboBoxForm()
        {
            InitializeComponent();
            this.CmbTreeView.CollapseAll();
        }
        public bool IsNonLeafNodeSelectable { get; set; }

        private void CmbTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)  
                return;
            if (!e.Node.HasNodes || IsNonLeafNodeSelectable)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}