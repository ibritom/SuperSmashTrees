using UnityEngine;

public class TokenSpawner : MonoBehaviour
{ 
    float timer;
    public GameObject tokenPrefab;
	public int numeroToken = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            timer = 0;
            numeroToken = Random.Range(0, 9);
            tokenPrefab.GetComponent<TokenController>().TokenValue = numeroToken;
            float x = Random.Range(-52f, 50f);
            Vector3 position = new Vector3(x, 32, 0);
            Quaternion rotation = new Quaternion();
            GameObject token = Instantiate(tokenPrefab, position, rotation);
            token.GetComponent<TokenController>().numeroToken = numeroToken;
            var animator = token.GetComponent<Animator>();
            animator.SetInteger("TokenValue", numeroToken);
            Debug.Log("Valor del token:" + numeroToken);
        }
    }
}
