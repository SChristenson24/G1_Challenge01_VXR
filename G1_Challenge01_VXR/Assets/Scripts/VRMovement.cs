using UnityEngine;
using UnityEngine.XR;

public class VRMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float fallMultiplier = 2.5f;

    private CharacterController characterController;
    private Transform vrCamera;

    private Vector2 inputAxis;
    private bool jumpPressed;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        vrCamera = Camera.main.transform;
    }

    void Update()
    {
        HandleInput();

        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 forward = vrCamera.forward;
        Vector3 right = vrCamera.right;

        forward.y = 0f; // Ignore the vertical component
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * inputAxis.y + right * inputAxis.x;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        if (jumpPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (velocity.y < 0)
        {
            velocity.y += gravity * fallMultiplier * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleInput()
    {
        UnityEngine.XR.InputDevice rightHandController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 axisValue))
        {
            inputAxis = axisValue;
        }

        if (rightHandController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool jumpButtonValue))
        {
            jumpPressed = jumpButtonValue;
        }
        else
        {
            jumpPressed = false;
        }
    }
}
