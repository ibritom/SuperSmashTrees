using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    public GameObject terrainPrefab;

    void Start()
    {
	int c = Random.Range(2,8);
		for (int i = 0; i < c; i++) 
		{
        	float y = Random.Range(-34, 16);
			float x = Random.Range(-50f, 50f);
        	Vector3 position = new Vector3(x, y, 0);
        	Quaternion rotation = new Quaternion();
        	Instantiate(terrainPrefab, position, rotation);
		}
		return;

    }
}
