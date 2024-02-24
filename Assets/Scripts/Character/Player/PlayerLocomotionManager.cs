using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    public PlayerManager player;

    public float verticalMovement;
    public float horizontalMovement;
    public float moveAmount;

    private Vector3 moveDir;
    [SerializeField] private float walkSpeed = 2;
    [SerializeField] private float runSpeed = 5;
    [SerializeField] private float rotSpeed = 15f;
    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    public void HandleAllMovement()
    {
        HandleGroundedMovement();
        HandleRotation();
    }

    private void GetMovementInputValues()
    {
        verticalMovement = PlayerInputManager.Instance.verticalMoveInput;
        horizontalMovement = PlayerInputManager.Instance.horizontalMoveInput;
        moveAmount = PlayerInputManager.Instance.moveAmount;

        //TODO: clamp the movements
    }

    private void HandleGroundedMovement()
    {
        GetMovementInputValues();
        moveDir = PlayerCamera.Instance.transform.forward * verticalMovement + PlayerCamera.Instance.transform.right * horizontalMovement;
        moveDir.Normalize();
        moveDir.y = 0;

        if(PlayerInputManager.Instance.moveAmount > 0.5f )
        {
            player.characterController.Move(moveDir * runSpeed * Time.deltaTime);
        }
        else if(PlayerInputManager.Instance.moveAmount <= 0.5f)
        {
            player.characterController.Move(moveDir * walkSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        Vector3 targetRotationDir = PlayerCamera.Instance.transform.forward * verticalMovement + PlayerCamera.Instance.transform.right * horizontalMovement;
        targetRotationDir.Normalize();
        targetRotationDir.y = 0;

        if(targetRotationDir == Vector3.zero)
        {
            targetRotationDir = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
        
    }
}