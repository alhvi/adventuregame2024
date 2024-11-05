using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private CharacterController characterController;
    private float cameraRotationSpeed = 200f;
    [SerializeField]
    private GameObject cameraLookout;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void CameraMovement() {
        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");

        float rotationAngleYaw = mousex * cameraRotationSpeed * Time.deltaTime; ;
        Quaternion yaw = Quaternion.AngleAxis(rotationAngleYaw, Vector3.up);

        float rotationAnglePitch = mousey * cameraRotationSpeed * Time.deltaTime;
        Quaternion pitch = Quaternion.AngleAxis(rotationAnglePitch, Vector3.right);

        Quaternion appliedPitch = cameraLookout.transform.rotation * pitch;

        cameraLookout.transform.rotation =  yaw * appliedPitch;
    }

    private void Update()
    {
        CameraMovement();
        float movx = Input.GetAxis("Horizontal");
        float movz = Input.GetAxis("Vertical");
        Vector3 movementVector = new(movx, 0, movz);

        if (movementVector.magnitude > 0)
        {

            characterController.SimpleMove(movementVector * speed);
            transform.forward = movementVector.normalized;
            //transform.forward = cameraLookout.transform.forward;
        }
    }
}
