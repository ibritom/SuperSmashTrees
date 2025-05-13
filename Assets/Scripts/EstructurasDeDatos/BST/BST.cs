using UnityEngine;

public class BST
{
    private BSTNode<int> root;

    public BST()
    {
        root = null;
    }

    public void Insert(int value)
    {
        root = InsertRecursive(root, value);
    }

    private BSTNode<int> InsertRecursive(BSTNode<int> node, int value)
    {
        if (node == null)
            return new BSTNode<int>(value);

        if (value < node.Value)
            node.Left = InsertRecursive(node.Left, value);
        else
            node.Right = InsertRecursive(node.Right, value);

        return node;
    }

    public BSTNode<int> GetRoot()
    {
        return root;
    }
}
