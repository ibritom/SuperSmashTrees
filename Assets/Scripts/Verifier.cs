using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class Verifier : MonoBehaviour
{
    public GameObject Title;
    public GameObject Prompt;
    public GameObject Warning;

    public void LoadMainGame()
    {
        if (Gamepad.all.Count > 0)
        {
            SceneManager.LoadScene("MainGame");
        } else
        {
            Title.SetActive(false);
            Prompt.SetActive(false);
            Warning.SetActive(true);
            StartCoroutine(RemoveWarning(5f));
        }
    }
    public IEnumerator RemoveWarning(float delay)
    {
        yield return new WaitForSeconds(delay);
        Warning.SetActive(false);
        Title.SetActive(true);
        Prompt.SetActive(true);
    }
}
