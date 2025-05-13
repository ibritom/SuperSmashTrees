using UnityEngine;

public class BSTTester : MonoBehaviour
{
    void Start()
    {
        BST tree = new BST();
        tree.Insert(9);
        tree.Insert(8);
        tree.Insert(7);
        tree.Insert(6);
        tree.Insert(5);

        Debug.Log("Recorrido inorden del árbol:");
        InOrderTraversal(tree.GetRoot());
    }

    void InOrderTraversal(BSTNode<int> node)
    {
        if (node == null)
            return;

        InOrderTraversal(node.Left);
        Debug.Log("Valor del nodo: " + node.Value);
        InOrderTraversal(node.Right);
    }
}
