using UnityEngine;
using UnityEngine.InputSystem;

public class GameTimer : MonoBehaviour
{
    // Darle accesso al InputActionsAsset
    public InputActionAsset InputActions;
    private InputActionMap playerMap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerMap = InputActions.FindActionMap("PlayerKeyboardAndController");
    }

    // Update is called once per frame
    void Update()
    {
        DisablePlayerInput();
    }

    // Desactivar los controles con la tecla C, luego esto se va a activar con un timer
    public void DisablePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerMap.Disable();
            Debug.Log("Controles desactivados");
        }
    }
}
