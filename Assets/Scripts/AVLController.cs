using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class AVLController : MonoBehaviour
{
    public AVLTreeVisualizer visualizer;

    public AVLTree tree = new AVLTree();

    void Start()
    {
        AVLTree tree = new AVLTree();
        visualizer.VisualizeTree(tree.root);
    }

    public void insert(int value)
    {
        tree.Insert(value);
        visualizer.VisualizeTree(tree.root);
    }
    public int getHeight()
    {
        return tree.GetHeight(tree.root);
    }

    public int getBalance()
    {
        return tree.GetBalance(tree.root);
    }
}
