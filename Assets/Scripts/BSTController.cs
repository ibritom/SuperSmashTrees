using UnityEngine;

public class BSTController : MonoBehaviour
{
    public BSTVisualizer visualizer;

    private BST tree;

    void Start()
    {
        tree = new BST();
        tree.Insert(9);
        //tree.Insert(8);
        //tree.Insert(7);
        //tree.Insert(6);
        //tree.Insert(5);
        visualizer.Visualize(tree.GetRoot());
    }
}

