using UnityEngine;

public class AVLTreeNode
{
    public int Value;
    public int height;
    public AVLTreeNode Left;
    public AVLTreeNode Right;

    public AVLTreeNode(int value)
    {
        this.Value = value;
        height = 1;
    }
}
