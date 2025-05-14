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
    public int GetDepth()
    {
        return GetDepthRecursive(root);
    }

    private int GetDepthRecursive(BSTNode<int> node)
    {
        if (node == null)
            return 0;

        int leftDepth = GetDepthRecursive(node.Left);
        int rightDepth = GetDepthRecursive(node.Right);

        return 1 + Mathf.Max(leftDepth, rightDepth);
    }
}
