using UnityEngine;

public class TokenController : MonoBehaviour
{
    public int numeroToken;
    public int TokenValue;
    public int CurrentDepth;
    public int CurrentBalance;
    private bool _collected = false;
    void Start()
    {
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
        if (_collected) return;
        _collected = true;
        var player = collision.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            if (Challenger.Instance.ChallengeType == 0)
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
                        player.assignedBST.tree.Clear();
                    }
                }
                else
                {
                    Debug.LogWarning("Jugador no tiene un árbol binario adjunto.");
                }
            } else {
                if (player.assignedAVL != null)
                {
                    player.assignedAVL.insert(TokenValue);
                    Debug.Log("Token recogido. Insertado valor: " + TokenValue);
                    CurrentDepth = player.assignedAVL.getHeight();
                    CurrentBalance = player.assignedAVL.getBalance();
                    if (CurrentDepth == Challenger.Instance.WantedDepth && CurrentBalance == Challenger.Instance.WantedBalance)
                    {
                        Debug.Log(player.name + "ha completado un reto.");
                        ScoreManager.Instance.AddPointToPlayer(player.gameObject);
                        player.IncrementChallengeCount();
                        player.assignedAVL.tree.Clear();
                    }
                }
                else
                {
                    Debug.LogWarning("Jugador no tiene un árbol binario adjunto.");
                }
            }
        }
        Destroy(gameObject);
    }
}