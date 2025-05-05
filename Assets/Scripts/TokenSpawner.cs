using UnityEngine;

public class TokenSpawner : MonoBehaviour
{ 
    float timer;
    public GameObject tokenPrefab;
	public int numeroToken;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            timer = 0;
            int numeroToken = Random.Range(0, 20);
            float x = Random.Range(-52f, 50f);
            Vector3 position = new Vector3(x, 32, 0);
            Quaternion rotation = new Quaternion();
            Instantiate(tokenPrefab, position, rotation);
        }
    }
    
}
