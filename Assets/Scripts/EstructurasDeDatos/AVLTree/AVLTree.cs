using UnityEngine;

public class AVLTree
{
    public AVLTreeNode root;

    public void Insert(int Value)
    {
        root = Insert(root, Value);
    }

    private AVLTreeNode Insert(AVLTreeNode node, int value)
    {
        if (node == null)
            return new AVLTreeNode(value);

        if (value < node.Value)
            node.Left = Insert(node.Left, value);
        else if (value > node.Value)
            node.Right = Insert(node.Right, value);
        else
            return node; // No duplicados

        UpdateHeight(node);

        int balance = GetBalance(node);

        // Rotaciones
        if (balance > 1 && value < node.Left.Value)
            return RotateRight(node); // LL
        if (balance < -1 && value > node.Right.Value)
            return RotateLeft(node); // RR
        if (balance > 1 && value > node.Left.Value)
        {
            node.Left = RotateLeft(node.Left); // LR
            return RotateRight(node);
        }
        if (balance < -1 && value < node.Right.Value)
        {
            node.Right = RotateRight(node.Right); // RL
            return RotateLeft(node);
        }

        return node;
    }

    private void UpdateHeight(AVLTreeNode node)
    {
        node.height = 1 + Mathf.Max(GetHeight(node.Left), GetHeight(node.Right));
    }

    public int GetHeight(AVLTreeNode node)
    {
        return node != null ? node.height : 0;
    }

    public int GetBalance(AVLTreeNode node)
    {
        return node != null ? GetHeight(node.Left) - GetHeight(node.Right) : 0;
    }

    private AVLTreeNode RotateRight(AVLTreeNode y)
    {
        AVLTreeNode x = y.Left;
        AVLTreeNode T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        UpdateHeight(y);
        UpdateHeight(x);

        return x;
    }

    private AVLTreeNode RotateLeft(AVLTreeNode x)
    {
        AVLTreeNode y = x.Right;
        AVLTreeNode T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }
    public void Clear()
    {
        root = null;
    }
}
