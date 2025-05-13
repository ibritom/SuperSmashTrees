using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class AVLTreeVisualizer : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject linePrefab;
    public float horizontalSpacing = 1.5f;
    public float verticalSpacing = 2.0f;
    private Dictionary<AVLTreeNode, GameObject> nodeObjects = new Dictionary<AVLTreeNode, GameObject>();

    public void VisualizeTree(AVLTreeNode root)
    {
        ClearVisualization();
        if (root != null)
        {
            DrawNode(root, Vector2.zero, 0);
        }
    }

    private void DrawNode(AVLTreeNode node, Vector2 position, int depth)
    {
        if (node == null) return;

        // Crear el nodo visual
        GameObject nodeObj = Instantiate(nodePrefab, position, Quaternion.identity, transform);
        nodeObj.GetComponentInChildren<TextMesh>().text = node.value.ToString(); // Asumiendo que AVLNode tiene un campo value
        nodeObjects[node] = nodeObj;

        // Calcular posiciones de hijos
        float childOffset = horizontalSpacing * Mathf.Pow(0.5f, depth);

        if (node.left != null)
        {
            Vector2 leftPos = position + new Vector2(-childOffset, -verticalSpacing);
            DrawNode(node.left, leftPos, depth + 1);
            DrawLine(position, leftPos);
        }

        if (node.right != null)
        {
            Vector2 rightPos = position + new Vector2(childOffset, -verticalSpacing);
            DrawNode(node.right, rightPos, depth + 1);
            DrawLine(position, rightPos);
        }
    }

    private void DrawLine(Vector2 from, Vector2 to)
    {
        GameObject line = Instantiate(linePrefab, transform);
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, from);
        lr.SetPosition(1, to);
    }

    private void ClearVisualization()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        nodeObjects.Clear();
    }
}