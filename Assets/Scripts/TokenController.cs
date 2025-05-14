using UnityEngine;

public class TokenController : MonoBehaviour
{
    public int numeroToken;
    public int TokenValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("Token tocado por: " + collision.name);

        var player = collision.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            Debug.LogWarning("Jugador detectado: " + player.name);
            Debug.LogWarning("Árbol asignado: " + (player.assignedBST != null ? player.assignedBST.name : "null"));

            if (player.assignedBST != null)
            {
                player.assignedBST.insert(TokenValue);
                Debug.LogWarning("Token recogido. Insertado valor: " + TokenValue);
            }
            else
            {
                Debug.LogWarning("Jugador no tiene un árbol binario adjunto.");
            }
        }
        else
        {
            Debug.LogWarning("El objeto que colisionó no es un jugador.");
        }

        Destroy(gameObject);
    }
}