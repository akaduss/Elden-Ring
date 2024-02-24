using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager characterManager;

    protected virtual void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
    }

    protected virtual void Update()
    {
        
    }

    public void UpdateAnimatorValues(float horizontalInput, float verticalInput)
    {
        characterManager.animator.SetFloat("Horizontal", horizontalInput, 0.1f, Time.deltaTime);
        characterManager.animator.SetFloat("Vertical", verticalInput, 0.1f, Time.deltaTime);
    }
}