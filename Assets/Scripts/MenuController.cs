using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public InputActionAsset InputActions;
    private InputAction startGameAction;

    private Verifier verifier;

    private void Awake()
    {
        startGameAction = InputSystem.actions.FindAction("StartGame");
    }
    private void Start()
    {
        GameObject MenuManager = GameObject.Find("MenuManager");
        verifier = MenuManager.GetComponent<Verifier>();
    }
    public void OnStartGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            verifier.LoadMainGame();
        }
    }
}
