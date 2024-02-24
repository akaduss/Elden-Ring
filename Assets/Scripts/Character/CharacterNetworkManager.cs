using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterNetworkManager : NetworkBehaviour
{
    [Header("Position")]
    public NetworkVariable<Vector3> networkPosition 
        = new(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<Quaternion> networkRotation 
        = new(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public Vector3 networkPositionVelocity;
    public float networkPositionSmoothTime = 0.1f;
    public float networkRotationSmoothTime = 0.2f;

    [Header("Animator")]
    public NetworkVariable<float> networkAnimatorVerticalParameter = new(0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> networkAnimatorHorizontalParameter = new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> networkMoveAmount = new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public float networkAnimationDampTime = 0.1f;
}