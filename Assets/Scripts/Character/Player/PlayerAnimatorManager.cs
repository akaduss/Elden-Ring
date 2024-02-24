using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : CharacterAnimatorManager
{
    PlayerManager player;

    private float moveAmount;
    private float horizontalInput;
    private float verticalInput;

    private void Start()
    {
        player = GetComponent<PlayerManager>();
    }

    protected override void Update()
    {
        base.Update();

        verticalInput = PlayerInputManager.Instance.verticalMoveInput;
        horizontalInput = PlayerInputManager.Instance.horizontalMoveInput;
        moveAmount = PlayerInputManager.Instance.moveAmount;

        if (player.IsOwner)
        {
            player.characterNetworkManager.networkAnimatorVerticalParameter.Value = verticalInput;
            player.characterNetworkManager.networkAnimatorHorizontalParameter.Value = horizontalInput;
            player.characterNetworkManager.networkMoveAmount.Value = moveAmount;
        }
        else
        {
            verticalInput = player.characterNetworkManager.networkAnimatorVerticalParameter.Value;
            horizontalInput = player.characterNetworkManager.networkAnimatorHorizontalParameter.Value;
            moveAmount = player.characterNetworkManager.networkMoveAmount.Value;
        }

        UpdateAnimatorValues(0, moveAmount);
    }

}
