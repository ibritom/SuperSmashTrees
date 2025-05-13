using UnityEngine;

public class AVLTree
{
    public AVLTreeNode root;

    public void Insert(int value)
    {
        root = Insert(root, value);
    }

    private AVLTreeNode Insert(AVLTreeNode node, int value)
    {
        if (node == null)
            return new AVLTreeNode(value);

        if (value < node.value)
            node.left = Insert(node.left, value);
        else if (value > node.value)
            node.right = Insert(node.right, value);
        else
            return node; // No duplicados

        UpdateHeight(node);

        int balance = GetBalance(node);

        // Rotaciones
        if (balance > 1 && value < node.left.value)
            return RotateRight(node); // LL
        if (balance < -1 && value > node.right.value)
            return RotateLeft(node); // RR
        if (balance > 1 && value > node.left.value)
        {
            node.left = RotateLeft(node.left); // LR
            return RotateRight(node);
        }
        if (balance < -1 && value < node.right.value)
        {
            node.right = RotateRight(node.right); // RL
            return RotateLeft(node);
        }

        return node;
    }

    private void UpdateHeight(AVLTreeNode node)
    {
        node.height = 1 + Mathf.Max(GetHeight(node.left), GetHeight(node.right));
    }

    private int GetHeight(AVLTreeNode node)
    {
        return node != null ? node.height : 0;
    }

    private int GetBalance(AVLTreeNode node)
    {
        return node != null ? GetHeight(node.left) - GetHeight(node.right) : 0;
    }

    private AVLTreeNode RotateRight(AVLTreeNode y)
    {
        AVLTreeNode x = y.left;
        AVLTreeNode T2 = x.right;

        x.right = y;
        y.left = T2;

        UpdateHeight(y);
        UpdateHeight(x);

        return x;
    }

    private AVLTreeNode RotateLeft(AVLTreeNode x)
    {
        AVLTreeNode y = x.right;
        AVLTreeNode T2 = y.left;

        y.left = x;
        x.right = T2;

        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }
}
