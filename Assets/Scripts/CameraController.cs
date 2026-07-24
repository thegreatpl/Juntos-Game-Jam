using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float movementSpeed = 1f; 

    InputAction Move; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Move = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        var movment = Move.ReadValue<Vector2>();

        transform.position = new Vector3(transform.position.x + (movment.x * movementSpeed), transform.position.y,
            transform.position.z + (movment.y * movementSpeed));
    }
}
