using UnityEngine;

public class AVLTreeNode
{
    public int value;
    public int height;
    public AVLTreeNode left;
    public AVLTreeNode right;

    public AVLTreeNode(int value)
    {
        this.value = value;
        height = 1;
    }
}
