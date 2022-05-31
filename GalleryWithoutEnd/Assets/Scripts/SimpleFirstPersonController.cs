using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleFirstPersonController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Transform cameraTransform;

    private float xRotation = 0;
    private float yRotation = 0;

    [SerializeField] private float playerSpeed = 5f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovePlayer();
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector2 lookVector = _inputReader.GetLookVector();

        yRotation += lookVector.x;
        xRotation -= lookVector.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void MovePlayer()
    {
        Vector2 moveVector = _inputReader.GetMovementVector();

        Vector3 move = new Vector3(moveVector.x, 0, moveVector.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;

        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += -5f * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
