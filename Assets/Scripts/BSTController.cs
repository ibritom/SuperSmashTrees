using UnityEngine;

public class BSTController : MonoBehaviour
{
    public BSTVisualizer visualizer;

    public BST tree;

    void Start()
    {
        tree = new BST();
        visualizer.Visualize(tree.GetRoot());
    }

    public void insert(int value) {
        tree.Insert(value);
        visualizer.Visualize(tree.GetRoot());
    }
}

