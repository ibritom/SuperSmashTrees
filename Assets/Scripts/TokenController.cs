using UnityEngine;

public class TokenController : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D other)
    {
	Destroy(gameObject);
    }
}
