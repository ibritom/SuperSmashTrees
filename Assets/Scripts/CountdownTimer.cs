using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    PlayerSpawner spawner;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    private bool inputDisabled = false;
    private void Awake()
    {
        GameObject Scripter = GameObject.Find("Scripter");
        spawner = Scripter.GetComponent<PlayerSpawner>();
    }
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if(remainingTime < 0)
        {
           remainingTime = 0;
           timerText.color = Color.red;
           DisableAllPlayerInputs();
           inputDisabled = true;
           ScoreManager.Instance.SaveAllScoresToPrefs(spawner.players);
            StartCoroutine(Cooldown(5f));

        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void DisableAllPlayerInputs()
    {
        PlayerInput[] players = FindObjectsOfType<PlayerInput>();
        foreach (var player in players)
        {
            player.enabled = false;
        }
    }

    public IEnumerator Cooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("ResultsScreen");
    }
}
