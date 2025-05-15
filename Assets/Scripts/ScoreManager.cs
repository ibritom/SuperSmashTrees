using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private Dictionary<GameObject, int> playerScores = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPlayer(GameObject player)
    {
        if (!playerScores.ContainsKey(player))
        {
            playerScores[player] = 0;
        }
    }

    public void AddPointToPlayer(GameObject player)
    {
        if (playerScores.ContainsKey(player))
        {
            playerScores[player]++;
            Debug.Log($"Punto para {player.name}! Total: {playerScores[player]}");
        }
        else
        {
            Debug.LogWarning($"Jugador {player.name} no registrado en ScoreManager.");
        }
    }

    public int GetScore(GameObject player)
    {
        return playerScores.TryGetValue(player, out int score) ? score : 0;
    }

    public void SaveAllScoresToPrefs(List<GameObject> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            GameObject player = players[i];
            int score = GetScore(player);
            PlayerPrefs.SetInt($"PlayerScore_{i}", score);
        }
        PlayerPrefs.Save();
    }
}
