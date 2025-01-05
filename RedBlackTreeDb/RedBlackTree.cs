using RedBlackTreeDb;
using System.Collections.Generic;

public enum NodeColor { Red, Black }

public class RedBlackNode
{
    public RedBlackTreeModel Object;
    public NodeColor Color;
    public RedBlackNode Left, Right, Parent;

    public RedBlackNode(RedBlackTreeModel key)
    {
        Object = key;
        Color = NodeColor.Red; // New nodes are initially red
        Left = Right = Parent = null;
    }
}

public class RedBlackTree
{
    private RedBlackNode Root;
    private readonly RedBlackNode Nil;
    private int searchComparisons = 0;

    public RedBlackTree()
    {
        Nil = new RedBlackNode(new RedBlackTreeModel { Key = 0}) { Color = NodeColor.Black };
        Root = Nil;
    }

    public void Delete(int key)
    {
        var z = Search(Root, key);

        if (z == Nil)
            return; // Key not found

        RedBlackNode y = z;
        NodeColor yOriginalColor = y.Color;
        RedBlackNode x;

        // Case 1: z has no left child
        if (z.Left == Nil)
        {
            x = z.Right;
            Transplant(z, z.Right);
        }
        // Case 2: z has no right child
        else if (z.Right == Nil)
        {
            x = z.Left;
            Transplant(z, z.Left);
        }
        // Case 3: z has two children
        else
        {
            y = Maximum(z.Left);
            yOriginalColor = y.Color;
            x = y.Left;

            if (y.Parent == z)
            {
                x.Parent = y;
            }
            else
            {
                Transplant(y, y.Left);

                y.Left = z.Left;
                y.Left.Parent = y;
            }

            Transplant(z, y);
            y.Right = z.Right;
            y.Right.Parent = y;
            y.Color = z.Color;
        }

        if (yOriginalColor == NodeColor.Black)
        {
            DeleteFixup(x);
        }
    }

    private void Transplant(RedBlackNode u, RedBlackNode v)
    {
        if (u.Parent == null)
            Root = v;
        else if (u == u.Parent.Left)
            u.Parent.Left = v;
        else
            u.Parent.Right = v;

        v.Parent = u.Parent;
    }

    private RedBlackNode Search(RedBlackNode node, int key)
    {
        while (node != Nil && key != node.Object.Key)
        {
            if (key < node.Object.Key)
                node = node.Left;
            else
                node = node.Right;
            searchComparisons++;
        }

        return node;
    }

    public void Update(int key, string value)
    {
        var node = Search(Root, key);
        if (node == Nil)
            return;
        node.Object.Value = value;
    }


    public (RedBlackNode node, int comparisons) SearchById(int key)
    {
        var node = Search(Root, key);
        int comparisons = searchComparisons;
        searchComparisons = 0;
        if (node == Nil)
        {
            return (null, comparisons);
        }
        
        return (node, comparisons);
    }

    private RedBlackNode Maximum(RedBlackNode node)
    {
        while (node.Right != Nil)
            node = node.Right;
        return node;
    }

    private void DeleteFixup(RedBlackNode x)
    {
        while (x != Root && x.Color == NodeColor.Black)
        {
            if (x == x.Parent.Left)
            {
                RedBlackNode w = x.Parent.Right;

                // Case 1: w is red
                if (w.Color == NodeColor.Red)
                {
                    w.Color = NodeColor.Black;
                    x.Parent.Color = NodeColor.Red;
                    RotateLeft(x.Parent);
                    w = x.Parent.Right;
                }

                // Case 2: w's children are black
                if (w.Left.Color == NodeColor.Black && w.Right.Color == NodeColor.Black)
                {
                    w.Color = NodeColor.Red;
                    x = x.Parent;
                }
                else
                {
                    // Case 3: w's right child is black
                    if (w.Right.Color == NodeColor.Black)
                    {
                        w.Left.Color = NodeColor.Black;
                        w.Color = NodeColor.Red;
                        RotateRight(w);
                        w = x.Parent.Right;
                    }

                    // Case 4: w's right child is red
                    w.Color = x.Parent.Color;
                    x.Parent.Color = NodeColor.Black;
                    w.Right.Color = NodeColor.Black;
                    RotateLeft(x.Parent);
                    x = Root;
                }
            }
            else
            {
                RedBlackNode w = x.Parent.Left;

                // Case 1: w is red
                if (w.Color == NodeColor.Red)
                {
                    w.Color = NodeColor.Black;
                    x.Parent.Color = NodeColor.Red;
                    RotateRight(x.Parent);
                    w = x.Parent.Left;
                }

                // Case 2: w's children are black
                if (w.Right.Color == NodeColor.Black && w.Left.Color == NodeColor.Black)
                {
                    w.Color = NodeColor.Red;
                    x = x.Parent;
                }
                else
                {
                    // Case 3: w's left child is black
                    if (w.Left.Color == NodeColor.Black)
                    {
                        w.Right.Color = NodeColor.Black;
                        w.Color = NodeColor.Red;
                        RotateLeft(w);
                        w = x.Parent.Left;
                    }

                    // Case 4: w's left child is red
                    w.Color = x.Parent.Color;
                    x.Parent.Color = NodeColor.Black;
                    w.Left.Color = NodeColor.Black;
                    RotateRight(x.Parent);
                    x = Root;
                }
            }
        }

        x.Color = NodeColor.Black;
    }

    public void Insert(RedBlackTreeModel newObject)
    {
        var newNode = new RedBlackNode(newObject) { Left = Nil, Right = Nil };
        RedBlackNode parent = null;
        var current = Root;

        // Find the appropriate place for the new node
        while (current != Nil)
        {
            parent = current;
            current = newObject.Key < current.Object.Key ? current.Left : current.Right;
        }

        newNode.Parent = parent;

        if (parent == null)
            Root = newNode; // Tree was empty
        else if (newObject.Key < parent.Object.Key)
            parent.Left = newNode;
        else
            parent.Right = newNode;

        newNode.Color = NodeColor.Red;

        FixInsert(newNode); // Fix the tree
    }

    private void FixInsert(RedBlackNode node)
    {
        while (node.Parent?.Color == NodeColor.Red)
        {
            var grandparent = node.Parent.Parent;

            if (node.Parent == grandparent?.Left) // Parent is a left child
            {
                var uncle = grandparent.Right;

                if (uncle.Color == NodeColor.Red) // Case 1: Uncle is red (recoloring)
                {
                    node.Parent.Color = NodeColor.Black;
                    uncle.Color = NodeColor.Black;
                    grandparent.Color = NodeColor.Red;
                    node = grandparent;
                }
                else
                {
                    if (node == node.Parent.Right) // Case 2: Node is a right child, uncle is black, triangle (rotate Parent)
                    {
                        node = node.Parent;
                        RotateLeft(node);
                    }

                    // Case 3: Node is a left child, uncle is black, line (rotate Grandparent and recolor)
                    node.Parent.Color = NodeColor.Black;
                    grandparent.Color = NodeColor.Red;
                    RotateRight(grandparent);
                }
            }
            else // Parent is a right child (mirror of above cases)
            {
                var uncle = grandparent.Left;

                if (uncle.Color == NodeColor.Red)
                {
                    node.Parent.Color = NodeColor.Black;
                    uncle.Color = NodeColor.Black;
                    grandparent.Color = NodeColor.Red;
                    node = grandparent;
                }
                else
                {
                    if (node == node.Parent.Left)
                    {
                        node = node.Parent;
                        RotateRight(node);
                    }

                    node.Parent.Color = NodeColor.Black;
                    grandparent.Color = NodeColor.Red;
                    RotateLeft(grandparent);
                }
            }
        }

        Root.Color = NodeColor.Black; // Root must always be black
    }

    private void RotateLeft(RedBlackNode node)
    {
        var pivot = node.Right; 
        node.Right = pivot.Left;

        if (pivot.Left != Nil)
            pivot.Left.Parent = node;  

        pivot.Parent = node.Parent;

        if (node.Parent == null)
            Root = pivot;
        else if (node == node.Parent.Left)
            node.Parent.Left = pivot;
        else
            node.Parent.Right = pivot;

        pivot.Left = node;
        node.Parent = pivot;
    }

    private void RotateRight(RedBlackNode node)
    {
        var pivot = node.Left;
        node.Left = pivot.Right;

        if (pivot.Right != Nil)
            pivot.Right.Parent = node;

        pivot.Parent = node.Parent;

        if (node.Parent == null)
            Root = pivot;
        else if (node == node.Parent.Right)
            node.Parent.Right = pivot;
        else
            node.Parent.Left = pivot;

        pivot.Right = node;
        node.Parent = pivot;
    }


    public List<RedBlackTreeModel> FlattenAndSort()
    {
        var result = new List<RedBlackTreeModel>();
        InOrderTraversal(Root, result);
        return result;
    }

    private void InOrderTraversal(RedBlackNode node, List<RedBlackTreeModel> result)
    {
        if (node == Nil)
            return;

        InOrderTraversal(node.Left, result); // Visit left subtree
        result.Add(node.Object);            // Visit node
        InOrderTraversal(node.Right, result); // Visit right subtree
    }


}

