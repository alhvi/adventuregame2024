using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float movx = Input.GetAxis("Horizontal");
        float movz = Input.GetAxis("Vertical");
        Vector3 movementVector = new(movx, 0, movz);

        if (movementVector.magnitude > 0)
        {

            characterController.SimpleMove(movementVector * speed);
            transform.forward = movementVector.normalized;
        }
    }
}
