using UnityEngine;

public class AVLTester : MonoBehaviour
{
    void Start()
    {
        AVLTree tree = new AVLTree();
        int[] values = { 10, 20, 30, 40, 50, 25 };

        foreach (int val in values)
        {
            tree.Insert(val);
        }

        Debug.Log("Árbol AVL creado con inserciones.");
    }
}

