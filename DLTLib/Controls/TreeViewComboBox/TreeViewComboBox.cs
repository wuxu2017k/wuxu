//V1.2(2016.7.10)
using System;
using System.Data;
using System.Configuration;
using System.Web;

using Gizmox.WebGUI.Forms;
using System.ComponentModel;

namespace DLTLib.Controls.TreeViewComboBox
{
    /// <summary>
    /// TreeViewComboBox by DLT Soft.
    /// </summary>
    [Description("TreeViewComboBox by DLT Soft.")]
    public class TreeViewComboBox : ComboBox
    {
        /// <summary>
        /// The form to be used as custom DropDown control.
        /// </summary>
        private TreeViewComboBoxForm _treeViewForm = null;
        private int _treeWidth = 120;
        private int _treeHeight = 200;

        /// <summary>
        /// The constructor.
        /// </summary>
        public TreeViewComboBox()
        {
            _treeViewForm = new TreeViewComboBoxForm();
            _treeViewForm.Closed += new EventHandler(OnTreeViewFormClosed);
            this.IsNonLeafNodeSelectable = false;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// Called when the dropdown form is closed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnTreeViewFormClosed(object sender, EventArgs e)
        {
            if (_treeViewForm.DialogResult == DialogResult.OK)
            {
                if (Tree.SelectedNode != null)
                {
                    this.SelectedValue = Tree.SelectedNode.Name;
                }
            }
        }

        public Gizmox.WebGUI.Forms.TreeView Tree
        {
            get { return _treeViewForm.CmbTreeView; }
        }

        public bool IsNonLeafNodeSelectable
        {
            get
            {
                return this._treeViewForm.IsNonLeafNodeSelectable;
            }
            set
            {
                this._treeViewForm.IsNonLeafNodeSelectable = value;
            }
        }

        /// <summary>
        /// Gets the currently selected item index.
        /// </summary>
        /// <value></value        
        public new Gizmox.WebGUI.Forms.TreeNode SelectedItem
        {
            get
            {
                return this.Tree.SelectedNode;
            }
        }

        public new Object SelectedValue
        {
            get
            {
                return Tree.SelectedNode == null ? null : Tree.SelectedNode.Name;
            }
            set
            {
                //V1.1(2016.5.7)：避免新增记录时在树中产生空结点
                if (value == DBNull.Value)
                {
                    this.Tree.SelectedNode = null;
                    this.Items.Clear();
                    this.Text = null;
                    return;
                }
                //V1.1(2016.5.7)：后面的代码在执行新增员工时，会在当前记录上执行3次，
                //增加if语句使其只可能执行一次
                if (Tree.SelectedNode != null)
                    if (Tree.SelectedNode.Name == value.ToString())
                    { 
                        if (this.Text != Tree.SelectedNode.Text)
                        {
                            this.Items.Clear();
                            this.Items.Add(Tree.SelectedNode.Text);
                            this.Text = Tree.SelectedNode.Text;
                        }
                        return;
                    }
                Gizmox.WebGUI.Forms.TreeNode[] tns = Tree.Nodes.Find(value.ToString(), true);
                if (tns.Length > 0)
                {
                    if (this.SelectedItem != tns[0])
                        Tree.SelectedNode = tns[0];
                }
                else
                {
                    Gizmox.WebGUI.Forms.TreeNode tn = new Gizmox.WebGUI.Forms.TreeNode(value.ToString());
                    tn.Name = value.ToString();
                    tn.ImageIndex = 0;
                    tn.SelectedImageIndex = 1;
                    Tree.Nodes.Add(tn);
                    Tree.SelectedNode = tn;
                }
                this.Items.Clear();
                this.Items.Add(Tree.SelectedNode.Text);
                this.Text = Tree.SelectedNode.Text;
            }
        }

        public ImageList ImageList
        {
            get
            {
                return Tree.ImageList;
            }
            set
            {
                Tree.ImageList = value;
            }
        }

        public int TreeWidth
        {
            get
            {
                return _treeWidth;
            }
            set
            {
                _treeWidth = value < 120 ? 120 : value;
            }
        }

        public int TreeHeight
        {
            get
            {
                return _treeHeight;
            }
            set
            {
                _treeHeight = value < 200 ? 200 : value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a custom drop down.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has a custom drop down; otherwise, <c>false</c>.
        /// </value>
        protected override bool IsCustomDropDown
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the custom form to display as drop down
        /// </summary>
        /// <returns></returns>
        protected override Form GetCustomDropDown()
        {
            _treeViewForm.DialogResult = DialogResult.None;
            _treeViewForm.Width = Math.Max(this.Width, _treeViewForm.Width);
            return _treeViewForm;
        }
    }
}
