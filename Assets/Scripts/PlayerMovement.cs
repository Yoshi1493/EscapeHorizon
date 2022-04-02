using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    new Transform transform;
    Camera mainCam;

    [SerializeField] float moveSpeed;
    const float MovementThreshold = 0.01f;

    void Awake()
    {
        transform = GetComponent<Transform>();
        mainCam = Camera.main;
    }

    void Update()
    {
        GetMovementInput();
    }

    void GetMovementInput()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 distance = Vector3.ClampMagnitude(mousePos - transform.position, 1f);
        distance.z = 0f;

        if (distance.magnitude > MovementThreshold)
        {
            float zRot = Mathf.Atan2(-distance.x, distance.y) * Mathf.Rad2Deg;
            transform.eulerAngles = zRot * Vector3.forward;

            transform.Translate(moveSpeed * transform.up * Time.deltaTime, Space.World);
        }
    }
}