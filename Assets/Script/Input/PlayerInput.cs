using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void PlayerMovementPerformed(Vector2 direction);
    public static event PlayerMovementPerformed OnPlayerMovementPerformed;

    public delegate void PlayerMovementCanceled();
    public static event PlayerMovementCanceled OnPlayerMovementCanceled;

    public delegate void PlayerStartRunning();
    public static event PlayerStartRunning OnPlayerStartRunning;

    public delegate void PlayerStopRunning();
    public static event PlayerStopRunning OnPlayerStopRunning;

    public delegate void PlayerInteracted();
    public static event PlayerInteracted OnPlayerInteracted;

    public delegate void PlayerRequestSwitch();
    /// <summary>
    /// When player request to switch to other place;
    /// </summary>
    public static event PlayerRequestSwitch OnPlayerRequestSwitch;

    public delegate void CameraMovement(Vector2 delta);
    public static event CameraMovement OnCameraMovementPerformed;

    public delegate void StopCameraMovement();
    public static event StopCameraMovement OnCameraMovementCancelled;

    public delegate void ToggleInventory();
    public static event ToggleInventory OnRequestToggleInventory;

    public delegate void TogglePause();
    public static event TogglePause OnRequestTogglePause;

    public delegate void ToggleDebugConsole();
    public static event ToggleDebugConsole OnRequestToggleConsole;

    private MainInput inputManage;

    public static void ForceMovementCancelled() => OnPlayerMovementCanceled?.Invoke();

    private void Awake()
    {
        inputManage = new MainInput();

        inputManage.Player.Movement.performed += context => { if (OnPlayerMovementPerformed != null) OnPlayerMovementPerformed(context.ReadValue<Vector2>()); };
        inputManage.Player.Movement.canceled += context => { if (OnPlayerMovementCanceled != null) OnPlayerMovementCanceled(); };
        inputManage.Player.Run.performed += context => OnPlayerStartRunning?.Invoke();
        inputManage.Player.Run.canceled += context => OnPlayerStopRunning?.Invoke();
        inputManage.Player.Interact.performed += context => OnPlayerInteracted?.Invoke();
        inputManage.Player.Switch.performed += context => OnPlayerRequestSwitch?.Invoke();
        inputManage.Player.Look.performed += context => OnCameraMovementPerformed?.Invoke(context.ReadValue<Vector2>());
        inputManage.Player.Look.canceled += context => OnCameraMovementCancelled?.Invoke();
        inputManage.Player.Inventory.performed += context => OnRequestToggleInventory?.Invoke();
        inputManage.Player.Pause.performed += context => OnRequestTogglePause?.Invoke();
        //DEBUG
        inputManage.Player.DEBUG_TOGGLECONSOLE.performed += context => OnRequestToggleConsole?.Invoke();
    }

    private void OnEnable()
    {
        inputManage.Enable();
    }

    private void OnDisable()
    {
        inputManage.Disable();
    }
}