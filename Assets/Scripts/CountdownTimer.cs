using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    private bool inputDisabled = false;

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
}
