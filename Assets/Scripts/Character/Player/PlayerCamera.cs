using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;

    [Header("References")]
    public PlayerManager player;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Camera mainCamera;

    [Header("Camera Settings")]
    [SerializeField] private float cameraSmoothTime = 1f;
    [SerializeField] private float rightLeftRotationSpeed = 220f;
    [SerializeField] private float upDownRotationSpeed = 220f;
    [SerializeField] private float minPivot = -30f;
    [SerializeField] private float maxPivot = 50f;
    [SerializeField] private float cameraCollisionRadius = 0.2f;
    [SerializeField] private LayerMask cameraCollideLayerMask;

    [Header("Camera Values")]
    [SerializeField] private float rightLeftLookAngle;
    [SerializeField] private float upDownLookAngle;
    [SerializeField] private float defaultCameraPositionZ;
    [SerializeField] private float targetCameraPositionZ;
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition;

    private void Awake()
    {
        if (Instance == null)
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
        defaultCameraPositionZ = mainCamera.transform.localPosition.z;
    }

    public void HandleAllCameraActions()
    {
        if(player != null)
        {
            HandleFollowTarget();
            HandleRotations();
            HandleCollisions();
        }
    }

    private void HandleFollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothTime * Time.deltaTime);
        transform.position = targetPosition;
    }

    private void HandleRotations()
    {
        Quaternion updownRotation;
        Quaternion rightLeftRotation;

        rightLeftLookAngle += PlayerInputManager.Instance.cameraHorizontalInput * Time.deltaTime * rightLeftRotationSpeed;
        rightLeftRotation = Quaternion.Euler(0f, rightLeftLookAngle, 0);
        transform.rotation = rightLeftRotation;

        upDownLookAngle += PlayerInputManager.Instance.cameraVerticalInput * Time.deltaTime * upDownRotationSpeed;
        upDownLookAngle = Mathf.Clamp(upDownLookAngle, minPivot, maxPivot);
        updownRotation = Quaternion.Euler(upDownLookAngle,0,0);
        cameraPivot.localRotation = updownRotation;
    }

    private void HandleCollisions()
    {
        targetCameraPositionZ = defaultCameraPositionZ;

        Vector3 direction = mainCamera.transform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.position, cameraCollisionRadius, direction, out RaycastHit hitInfo, Mathf.Abs(targetCameraPositionZ), cameraCollideLayerMask))
        {
            float distanceFromObject = Vector3.Distance(cameraPivot.position, hitInfo.point);
            targetCameraPositionZ = -(distanceFromObject - cameraCollisionRadius);
        }

        if(Mathf.Abs(targetCameraPositionZ) < cameraCollisionRadius)
        {
            targetCameraPositionZ = -cameraCollisionRadius;
        }

        cameraObjectPosition.z = Mathf.Lerp(mainCamera.transform.localPosition.z, targetCameraPositionZ, 0.2f);
        mainCamera.transform.localPosition = cameraObjectPosition;

    }

}