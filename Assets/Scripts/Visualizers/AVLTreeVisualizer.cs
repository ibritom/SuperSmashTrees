using UnityEngine;
using System.Collections.Generic;

public class AVLTreeVisualizer : MonoBehaviour
{
    public GameObject nodePrefab;         // Prefab del NodeUI
    public RectTransform panelTransform;  // Panel donde colocaremos los nodos
    private Dictionary<AVLTreeNode, GameObject> nodesSpawned = new();

    public GameObject linePrefab;          // Prefab de la línea
    private List<GameObject> linesSpawned = new(); // Para llevar control y limpiar después

    public void VisualizeTree(AVLTreeNode root)
    {
        if (root == null)
        {
            Debug.LogWarning("No hay árbol para visualizar.");
            return;
        }

        ClearPreviousNodes();

        float startX = 0f; // Coordenada inicial horizontal
        float startY = 0f; // Coordenada inicial vertical
        float horizontalSpacing = 15f; // Separación horizontal entre nodos
        float verticalSpacing = 18f;   // Separación vertical entre niveles

        DrawRecursive(root, startX, startY, horizontalSpacing, verticalSpacing);
    }

    private void DrawRecursive(AVLTreeNode node, float x, float y, float hSpacing, float vSpacing)
    {
        if (node == null)
            return;

        // Instanciar el nodo
        GameObject newNode = Instantiate(nodePrefab, panelTransform);
        newNode.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = node.Value.ToString();
        RectTransform rt = newNode.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(x, y);

        nodesSpawned[node] = newNode;

        // Dibujar hijo izquierdo
        if (node.Left != null)
        {
            DrawRecursive(node.Left, x - hSpacing, y - vSpacing, hSpacing * 0.7f, vSpacing);
            DrawLineBetween(nodesSpawned[node], nodesSpawned[node.Left]);
        }

        // Dibujar hijo derecho
        if (node.Right != null)
        {
            DrawRecursive(node.Right, x + hSpacing, y - vSpacing, hSpacing * 0.7f, vSpacing);
            DrawLineBetween(nodesSpawned[node], nodesSpawned[node.Right]);
        }

    }

    private void DrawLineBetween(GameObject fromNode, GameObject toNode)
    {
        GameObject line = Instantiate(linePrefab, panelTransform);
        RectTransform lineRect = line.GetComponent<RectTransform>();

        Vector2 startPos = fromNode.GetComponent<RectTransform>().anchoredPosition;
        Vector2 endPos = toNode.GetComponent<RectTransform>().anchoredPosition;

        Vector2 direction = endPos - startPos;
        float distance = direction.magnitude;

        lineRect.sizeDelta = new Vector2(4f, distance); // Grosor y longitud
        lineRect.anchoredPosition = startPos + direction / 2;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineRect.rotation = Quaternion.Euler(0, 0, angle - 90);

        line.transform.SetAsFirstSibling(); // Para que la línea esté detrás del nodo

        linesSpawned.Add(line);
    }


    private void ClearPreviousNodes()
    {
        foreach (var entry in nodesSpawned)
        {
            Destroy(entry.Value);
        }
        nodesSpawned.Clear();
    }
}

