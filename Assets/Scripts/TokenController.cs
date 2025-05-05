using UnityEngine;

public class TokenController : MonoBehaviour 
{
    public int numeroToken;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fighters"))
        {
            //BinaryTree tree = collision.GetComponent<BinaryTree>();
            //if (tree != null)
            //{
            //    tree.Insertar(TokenValue);
            //    Debug.Log("Token recogido. Insertado valor: " + TokenValue);
            //}
        } else
        {
            //Debug.LogWarning("Jugador no tiene un árbol binario adjunto.");
        }
        Destroy(gameObject); // Destruir el token
    }
}
