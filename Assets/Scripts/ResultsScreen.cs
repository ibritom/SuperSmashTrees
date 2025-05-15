using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsScreen : MonoBehaviour
{
    public int player1Score;
    public int player2Score;
    public int player3Score;
    public int player4Score;

    public TextMeshProUGUI P1ScoreText;
    public TextMeshProUGUI P2ScoreText;
    public TextMeshProUGUI P3ScoreText;
    public TextMeshProUGUI P4ScoreText;
    public TextMeshProUGUI WinnerText;

    public int MaxScore = int.MinValue;
    public int WinnerIndex = -1;
    public List<int> WinnerIndices = new();

    private void Awake()
    {
        if (P1ScoreText == null)
            P1ScoreText = GameObject.Find("P1ScoreText")?.GetComponent<TextMeshProUGUI>();
        if (P2ScoreText == null)
            P2ScoreText = GameObject.Find("P2ScoreText")?.GetComponent<TextMeshProUGUI>();
        if (P3ScoreText == null)
            P3ScoreText = GameObject.Find("P3ScoreText")?.GetComponent<TextMeshProUGUI>();
        if (P4ScoreText == null)
            P4ScoreText = GameObject.Find("P4ScoreText")?.GetComponent<TextMeshProUGUI>();
        if (WinnerText == null)
            WinnerText = GameObject.Find("WinnerText")?.GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        try
        {
            // Destruye objetos innecesarios si existen
            GameObject scripter = GameObject.Find("Scripter");
            if (scripter != null)
            {
                Destroy(scripter);
            }

            LoadScores();
            UpdateText();
        } catch (Exception e)
        {
            Debug.LogError("Error en ResultsScreen.Start(): " + e.Message + "\n" + e.StackTrace);
        }
    }

    public void UpdateText()
    {
        P1ScoreText.text = string.Format("Puntos Jugador 1:" + player1Score);
        P2ScoreText.text = string.Format("Puntos Jugador 2:" + player2Score);
        P3ScoreText.text = string.Format("Puntos Jugador 3:" + player3Score);
        P4ScoreText.text = string.Format("Puntos Jugador 4:" + player4Score);

        if (PlayerPrefs.HasKey("PlayerScore_2"))
        {
            player3Score = PlayerPrefs.GetInt("PlayerScore_2");
        }
        else
        {
            P3ScoreText.text = string.Format("");
            Debug.Log("No hay puntaje guardado para Jugador 3");
        }
        if (PlayerPrefs.HasKey("PlayerScore_3"))
            player4Score = PlayerPrefs.GetInt("PlayerScore_3");
        else
        {
            P4ScoreText.text = string.Format("");
            Debug.Log("No hay puntaje guardado para Jugador 4");
        }

        if (WinnerIndices.Count == 1)
        {
            string winnerName = GetWinnerName(WinnerIndices[0]);
            WinnerText.text = $"Ganador: {winnerName}";
        }
        else if (WinnerIndices.Count > 1)
        {
            string empateText = "Empate...";
            WinnerText.text = empateText;
        }
        else
        {
            WinnerText.text = "No se encontraron puntajes.";
        }
    }
    void LoadScores()
    {
        player1Score = PlayerPrefs.GetInt("PlayerScore_0", 0);
        player2Score = PlayerPrefs.GetInt("PlayerScore_1", 0);
        player3Score = PlayerPrefs.GetInt("PlayerScore_2", 0);
        player4Score = PlayerPrefs.GetInt("PlayerScore_3", 0);

        int[] scores = { player1Score, player2Score, player3Score, player4Score };

        WinnerIndices.Clear();
        for (int i = 0; i < 4; i++)
        {
            string key = $"PlayerScore_{i}";
            if (PlayerPrefs.HasKey(key))
            {
                int score = PlayerPrefs.GetInt(key);
                if (score > MaxScore)
                {
                    MaxScore = score;
                    WinnerIndices.Clear();
                    WinnerIndices.Add(i);
                }
                else if (score == MaxScore)
                {
                    WinnerIndices.Add(i);
                }
            }
        }

        if (WinnerIndex != -1)
        {
            Debug.Log($"El jugador {WinnerIndex + 1} ganó con {MaxScore} puntos.");
        }
        else
        {
            Debug.Log("No se encontraron puntajes.");
        }
    }
    string GetWinnerName(int index)
    {
        return index switch
        {
            0 => "Sanae Kochiya",
            1 => "Aya Shameimaru",
            2 => "Sakuya Izayoi",
            3 => "Yuyuko Saigyoji",
            _ => $"Jugador {index + 1}"
        };
    }
}
