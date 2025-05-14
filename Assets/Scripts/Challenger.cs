using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Challenger : MonoBehaviour
{
    public int ChallengeType;
    public int ChallengeNumber;
    public int WantedDepth;

    public TextMeshProUGUI ChallengeUIText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateChallenge();
    }

    public void CreateChallenge()
    {
        //ChallengeType = Random.Range(0, 1); // 0 = BST, 1 = AVL
        ChallengeType = 0;  // Voy a forzar BST hasta que AVL este implementado
        ChallengeNumber = Random.Range(0, 1);
        if (ChallengeType == 0)
        {
            // Hacer challenges del BST
            if (ChallengeNumber == 0)
            {
                // Challenge 1
                WantedDepth = 2;
                ChallengeUIText.text = string.Format("RETO: Hacer un árbol BST de profundidad 2");
            } else {
                // Challenge 2
                WantedDepth = 4;
                ChallengeUIText.text = string.Format("RETO: Hacer un árbol BST de profundidad 4");
            }
        } else {
            // Hacer challenges del AVL
            if (ChallengeNumber == 0)
            {
                // Challenge 1
            }
            else {
                // Challenge 2
            }
        }
    }
}
