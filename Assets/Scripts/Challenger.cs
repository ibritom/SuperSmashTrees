using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Challenger : MonoBehaviour
{
    public static Challenger Instance;

    public int ChallengeType;
    public int ChallengeNumber;
    public int WantedDepth;
    public int WantedBalance;

    public TextMeshProUGUI ChallengeUIText;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateChallenge();
    }

    public void CreateChallenge()
    {
        ChallengeType = Random.Range(0, 2); // 0 = BST, 1 = AVL
        ChallengeNumber = Random.Range(0, 2);
        Debug.Log("ChallengeNumber:" + ChallengeNumber);
        if (ChallengeType == 0)
        {
            // Hacer challenges del BST
            if (ChallengeNumber == 0)
            {
                // Challenge 1
                WantedDepth = 2;
                ChallengeUIText.text = string.Format("RETO: Hacer un �rbol BST de profundidad 2");
            } else {
                // Challenge 2
                WantedDepth = 4;
                ChallengeUIText.text = string.Format("RETO: Hacer un �rbol BST de profundidad 4");
            }
        } else {
            // Hacer challenges del AVL
            if (ChallengeNumber == 0)
            {
                // Challenge 1
                WantedDepth = 3;
                WantedBalance = 0;
                ChallengeUIText.text = string.Format("RETO: Hacer un �rbol AVL de profundidad 3 m�nimo con balanceo 0");
            }
            else {
                // Challenge 2
                WantedDepth = 3;
                WantedBalance = 1;
                ChallengeUIText.text = string.Format("RETO: Hacer un �rbol AVL de profundidad 3 m�nimo con balanceo 1");
            }
        }
        Debug.Log("Challenger configurado: WantedDepth = " + WantedDepth);
    }
}