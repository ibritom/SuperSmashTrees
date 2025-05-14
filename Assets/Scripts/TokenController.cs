using UnityEngine;

public class TokenController : MonoBehaviour
{
    public int numeroToken;
    public int TokenValue;
    public int WantedDepth;
    public int CurrentDepth;
    void Start()
    {
        int WantedDepth = Challenger.Instance.WantedDepth;
        if (Challenger.Instance != null)
        {
            Debug.Log("Desde TokenController: WantedDepth = " + Challenger.Instance.WantedDepth);
        }
        else
        {
            Debug.LogWarning("Challenger.Instance es null en TokenController");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            if (player.assignedBST != null)
            {
                player.assignedBST.insert(TokenValue);
                Debug.Log("Token recogido. Insertado valor: " + TokenValue);
                CurrentDepth = player.assignedBST.depth();
                if (CurrentDepth == Challenger.Instance.WantedDepth)
                {
                    Debug.Log(player.name + "ha completado un reto.");
                    ScoreManager.Instance.AddPointToPlayer(player.gameObject);
                    player.IncrementChallengeCount();
                }
            }
            else
            {
                Debug.LogWarning("Jugador no tiene un árbol binario adjunto.");
            }
        }

        Destroy(gameObject);
    }
}