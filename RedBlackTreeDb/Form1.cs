using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RedBlackTreeDb
{
    public partial class Form1 : Form
    {
        private RedBlackTree redBlackTree;
        public Form1()
        {
            InitializeComponent();
            redBlackTree = new RedBlackTree();
            InitializeRedBlackTree();
            InitializeDbTable();
        }

        public void InitializeRedBlackTree()
        {
            redBlackTree = TryLoadRedBlackTree();

            if (redBlackTree == null)
            {
                redBlackTree = new RedBlackTree();
                for (int i = 1; i < 10000; i++)
                {
                    var redBlackTreeModel = new RedBlackTreeModel
                    {
                        Key = i,
                        Value = $"Value {i}"
                    };
                    redBlackTree.Insert(redBlackTreeModel);
                }
                SaveChanges();
            }
        }
        public void InitializeDbTable()
        {
            dbTable.Rows.Clear();
            foreach (var node in redBlackTree.FlattenAndSort())
            {
                dbTable.Rows.Add(node.Key, node.Value);
            }
        }

        private void searchInputField_OnFocus(object sender, EventArgs e)
        {
            searchInputField.Text = "";
        }
        private void searchInputField_OnLeave(object sender, EventArgs e)
        {
            if (searchInputField.Text == "")
            {
                searchInputField.Text = "Enter Id";
            }
        }

        private void addRowButton_Click(object sender, EventArgs e)
        {
            int nextId = 1; // Default ID for the first row

            if (dbTable.Rows.Count > 0)
            {
                // Get the value of the 'Id' column in the last row
                var lastRow = dbTable.Rows[dbTable.Rows.Count - 1];
                if (lastRow.Cells["IdTableColumn"].Value != null && int.TryParse(lastRow.Cells["IdTableColumn"].Value.ToString(), out int lastId))
                {
                    nextId = lastId + 1;
                }
            }
            var newEntry = new RedBlackTreeModel
            {
                Key = nextId,
                Value = "New Entry"
            };


            // Add a new row and set the ID
            int newRowIndex = dbTable.Rows.Add(newEntry.Key, newEntry.Value);
            redBlackTree.Insert(newEntry);
        }

        public void deleteRowButton_Click(object sender, EventArgs e)
        {
            if (dbTable.SelectedRows.Count > 0)
            {
                var index = dbTable.SelectedRows[0].Index;
                var id = int.Parse(dbTable.Rows[index].Cells["IdTableColumn"].Value.ToString());
                dbTable.Rows.RemoveAt(index);
                //get Id value of index

                redBlackTree.Delete(id);
            }
        }

        public void searchButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(searchInputField.Text, out int searchId))
            {
                var searchResult = redBlackTree.SearchById(searchId);
                if (searchResult.node != null)
                {
                    MessageBox.Show($"Id: {searchResult.node.Object.Key}, Value: {searchResult.node.Object.Value}. Comparisons: {searchResult.comparisons}");
                }
                else
                {
                    MessageBox.Show("Entry not found.");
                }
            }
            else
            {
                MessageBox.Show("Invalid ID.");
            }

        }

        public void saveChangesButton_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        //cell value changed
        private void dbTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var key = int.Parse(dbTable.Rows[e.RowIndex].Cells["IdTableColumn"].Value.ToString());
                var value = dbTable.Rows[e.RowIndex].Cells["NameTableColumn"].Value.ToString();

                redBlackTree.Update(key, value);
            }
        }


        public void SaveChanges()
        {
            var flattenedTree = redBlackTree.FlattenAndSort();
            var serializedTree = JsonConvert.SerializeObject(flattenedTree);
            System.IO.File.WriteAllText("redBlackTree.json", serializedTree);
        }
        public RedBlackTree TryLoadRedBlackTree()
        {
            try
            {
                //load the serialized tree from the file
                var serializedTree = System.IO.File.ReadAllText("redBlackTree.json");
                //deserialize the tree

                var redBlackTreeItems = JsonConvert.DeserializeObject<List<RedBlackTreeModel>>(serializedTree);
                //reconstruct the tree
                var redBlackTree = new RedBlackTree();
                foreach (var item in redBlackTreeItems)
                {
                    redBlackTree.Insert(item);
                }

                return redBlackTree;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
