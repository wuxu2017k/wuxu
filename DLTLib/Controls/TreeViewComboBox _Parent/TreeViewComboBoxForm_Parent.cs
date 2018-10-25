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
    public partial class TreeViewComboBoxForm_Parent : Form
    {

        public TreeViewComboBoxForm_Parent()
        {
            InitializeComponent();
            this.CmbTreeView.ExpandAll();
        }
        private void CmbTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
                return;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}