using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private CharacterController characterController;
    private float cameraRotationSpeed = 200f;
    [SerializeField]
    private GameObject cameraLookout;
    private Quaternion rotationQuat = Quaternion.identity;
    private float characterRotationTime = 0;
    private float characterRotationSpeed = 5f;
    [SerializeField]
    private GameObject model;
    [SerializeField]
    private bool thirdPersonActive = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void CameraMovement()
    {
        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");

        float rotationAngleYaw = mousex * cameraRotationSpeed * Time.deltaTime;
        Quaternion yaw = Quaternion.AngleAxis(rotationAngleYaw, Vector3.up);

        float rotationAnglePitch = mousey * cameraRotationSpeed * Time.deltaTime;
        Quaternion pitch = Quaternion.AngleAxis(rotationAnglePitch, Vector3.right);

        Quaternion appliedPitch = cameraLookout.transform.rotation * pitch;

        if (FormatAngle(appliedPitch.eulerAngles.x) > 45)
        {
            appliedPitch = Quaternion.Euler(45f, appliedPitch.eulerAngles.y, appliedPitch.eulerAngles.z);
        } else if (FormatAngle(appliedPitch.eulerAngles.x) < -5)
        {
            appliedPitch = Quaternion.Euler(-5f, appliedPitch.eulerAngles.y, appliedPitch.eulerAngles.z);
        }

        cameraLookout.transform.rotation = yaw * appliedPitch;
    }

    private void Update()
    {
        CameraMovement();
        float movx = Input.GetAxis("Horizontal");
        float movz = Input.GetAxis("Vertical");
        Vector3 movementVector = Vector3.zero;

        if (thirdPersonActive)
        {

            movementVector = (movx * cameraLookout.transform.right) + (movz * cameraLookout.transform.forward);
            movementVector = new Vector3(movementVector.x, 0, movementVector.z);
            movementVector.Normalize();
        } else
        {
            movementVector = new Vector3(movx, 0, movz);
        }





        if (movementVector.magnitude > 0)
        {

            characterController.SimpleMove(movementVector * speed);
            rotationQuat = Quaternion.LookRotation(movementVector);
            characterRotationTime = 0;
        }

        if (thirdPersonActive)
        {

            if (characterRotationTime * characterRotationSpeed < 1)
            {
                characterRotationTime += Time.deltaTime;
                model.transform.rotation = Quaternion.Lerp(model.transform.rotation, rotationQuat, characterRotationSpeed * characterRotationTime);
            }
        }
    }


    private float FormatAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
        {
            angle -= 360;
        }

        return angle;
    }
}
