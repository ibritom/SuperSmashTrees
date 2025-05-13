using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject Player;
    public GameObject BSTTree;
    public Transform[] spawnPoints;
    public Transform[] treeVisualizers;
    public float spawnPointRadius = 5;
    public float treeVisualRadius = 75;
    private int playerCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadSceneAsync("Trees", LoadSceneMode.Additive);
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

        GameObject newPlayer = Instantiate(Player, spawnPoint.position, Quaternion.identity);

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

        // Instanciar árbol correspondiente
        GameObject playerTree = Instantiate(BSTTree, treePosition.position, Quaternion.identity);

        playerCount++;
    }

    // Visualizar donde spawnean los jugadores en el editor
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