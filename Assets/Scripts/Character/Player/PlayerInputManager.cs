using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;

    public event EventHandler OnMoveInputChanged;

    public PlayerManager playerManager;

    PlayerControls playerControls;

    [Header("Player Movement Input")]
    [SerializeField] Vector2 movementInput;
    public float horizontalMoveInput;
    public float verticalMoveInput;
    public float moveAmount;
    public bool toggleRun = true;
    public bool dodge = false;

    [Header("Camera Movement Input")]
    [SerializeField] Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChange;
        Instance.enabled = false;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleCameraInput();
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        Instance.enabled = (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldBuildIndex());
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.ToggleRun.performed += i => toggleRun = !toggleRun;
            //playerControls.PlayerMovement.Dodge.performed +=
        }

        playerControls.Enable();
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    // IF WE MINIMIZE THE WINDOW, STOP ADJUSTING INPUTS
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            playerControls.Enable();
        }
        else
        {
            playerControls.Disable();
        }
    }


    private void HandleMovementInput()
    {
        horizontalMoveInput = movementInput.x;
        verticalMoveInput = movementInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalMoveInput) + Mathf.Abs(horizontalMoveInput));
        float clampedMoveAmount = moveAmount;

        if (moveAmount <= 0.5f && moveAmount > 0)
        {
            clampedMoveAmount = 0.5f;
        }
        else if (toggleRun == false && moveAmount > 0.5f && moveAmount <= 1)
        {
            clampedMoveAmount = 0.5f;
        }
        else if(toggleRun == true && moveAmount > 0.5f && moveAmount <= 1)
        {
            clampedMoveAmount = 1f;
        }
        moveAmount = clampedMoveAmount;
        
        OnMoveInputChanged?.Invoke(this, EventArgs.Empty);
    }

    private void HandleCameraInput()
    {
        cameraHorizontalInput = cameraInput.x;
        cameraVerticalInput = cameraInput.y;
    }

    private void HandleDodgeInput()
    {

    }

}