using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSmoothLocomotion : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 120.0f;
    private CharacterController characterController;
    private XRNode leftHand = XRNode.LeftHand;
    private XRNode rightHand = XRNode.RightHand;
    private Vector2 moveInput;
    private Vector2 turnInput;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input from controllers
        InputDevice leftDevice = InputDevices.GetDeviceAtXRNode(leftHand);
        InputDevice rightDevice = InputDevices.GetDeviceAtXRNode(rightHand);
        leftDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out moveInput);
        rightDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out turnInput);

        MovePlayer();
        RotatePlayer();
    }

    void MovePlayer()
    {
        Vector3 direction = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(direction * moveSpeed * Time.deltaTime);
    }

    void RotatePlayer()
    {
        if (Mathf.Abs(turnInput.x) > 0.1f)
        {
            float rotation = turnInput.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotation);
        }
    }
}
