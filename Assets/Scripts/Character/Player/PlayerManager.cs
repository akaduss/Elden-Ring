using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    public PlayerAnimatorManager playerAnimatorManager;
    public PlayerLocomotionManager playerLocomotionManager;
    protected override void Awake()
    {
        base.Awake();

        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
    }

    protected override void Update()
    {
        base.Update();
        if (!IsOwner) return;

        playerLocomotionManager.HandleAllMovement();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (!IsOwner) return;

        PlayerCamera.Instance.HandleAllCameraActions();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if(IsOwner)
        {
            PlayerCamera.Instance.player = this;
            PlayerInputManager.Instance.playerManager = this;
        }
    }
}