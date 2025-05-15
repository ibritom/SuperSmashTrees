using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject Player;
    public GameObject BSTTree;
    public GameObject AVLTree;
    public Transform[] spawnPoints;
    public Transform[] treeVisualizers;
    public float spawnPointRadius = 5;
    public float treeVisualRadius = 5;
    private int playerCount = 0;

    public List<GameObject> players = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int gamepadIndex = 0;

        // Spawnear primer jugador con teclado (si existe)
        if (Keyboard.current != null && playerCount < spawnPoints.Length && playerCount < treeVisualizers.Length)
        {
            SpawnPlayerWithTree(Keyboard.current);
        }

        // Spawnear jugadores por cada gamepad conectado
        foreach (var gamepad in Gamepad.all)
        {
            if (playerCount < spawnPoints.Length && playerCount < treeVisualizers.Length)
            {
                SpawnPlayerWithTree(gamepad);
            }
        }
    }
    void SpawnPlayerWithTree(InputDevice device)
    {
        Transform spawnPoint = spawnPoints[playerCount];
        Transform treePosition = treeVisualizers[playerCount];
        if (Challenger.Instance.ChallengeType == 0)
        {
            GameObject newPlayer = Instantiate(Player, spawnPoint.position, Quaternion.identity);
            GameObject newBST = Instantiate(BSTTree, treePosition.position, Quaternion.identity);
            ScoreManager.Instance.AddPlayer(newPlayer);
            players.Add(newPlayer);

            // Asignar árbol al jugador
            PlayerController playerController = newPlayer.GetComponent<PlayerController>();
            BSTController bstController = newBST.GetComponent<BSTController>();

            if (playerController != null && bstController != null)
            {
                playerController.assignedBST = bstController;
                Debug.LogWarning("Asignando árbol al jugador " + playerController.name + ": " + bstController.name);
            }
            else
            {
                Debug.LogWarning("No se pudo asignar el árbol: faltan componentes.");
            }


            var playerInput = newPlayer.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                playerInput.SwitchCurrentControlScheme(device);
                playerInput.defaultControlScheme = device is Keyboard ? "Keyboard" : "Gamepad";
            }

            var animator = newPlayer.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetInteger("PlayerNum", playerCount);
            }
        } else {
            GameObject newPlayer = Instantiate(Player, spawnPoint.position, Quaternion.identity);
            GameObject newAVL = Instantiate(AVLTree, treePosition.position, Quaternion.identity);
            ScoreManager.Instance.AddPlayer(newPlayer);
            players.Add(newPlayer);

            // Asignar árbol al jugador
            PlayerController playerController = newPlayer.GetComponent<PlayerController>();
            AVLController avlController = newAVL.GetComponent<AVLController>();

            if (playerController != null && avlController != null)
            {
                playerController.assignedAVL = avlController;
                Debug.LogWarning("Asignando árbol al jugador " + playerController.name + ": " + avlController.name);
            }
            else
            {
                Debug.LogWarning("No se pudo asignar el árbol: faltan componentes.");
            }


            var playerInput = newPlayer.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                playerInput.SwitchCurrentControlScheme(device);
                playerInput.defaultControlScheme = device is Keyboard ? "Keyboard" : "Gamepad";
            }

            var animator = newPlayer.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetInteger("PlayerNum", playerCount);
            }
        }
            playerCount++;
    }

    // Visualizar donde spawnean los jugadores y árboles en el editor
    private void OnDrawGizmos()
    {
        foreach (Transform SpawnPoint in spawnPoints)
        {
            Gizmos.DrawWireSphere(SpawnPoint.position, spawnPointRadius);
        }
        foreach (Transform TreeVisualizer in treeVisualizers)
        {
            Gizmos.DrawWireSphere(TreeVisualizer.position, treeVisualRadius);
        }
    }
}