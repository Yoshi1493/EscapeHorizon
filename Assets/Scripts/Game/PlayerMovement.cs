using UnityEngine;

public class PlayerMovement : Actor
{
    Camera mainCam;

    [SerializeField] FloatObject moveSpeed;
    const float MovementThreshold = 0.01f;

    float smoothDampAngle = 0f;
    const float SmoothingAmount = 10f;

    protected override void Awake()
    {
        base.Awake();
        mainCam = Camera.main;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        moveDirection = Vector3.ClampMagnitude(mousePos - transform.position, 1f);
        moveDirection.z = 0f;

        float magnitude = moveDirection.magnitude;

        if (magnitude > MovementThreshold)
        {
            float targetRotZ = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
            float zRot = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetRotZ, ref smoothDampAngle, SmoothingAmount * Time.deltaTime);

            transform.eulerAngles = zRot * Vector3.forward;

            transform.Translate(moveSpeed.Value * Time.deltaTime * transform.up, Space.World);
        }
    }
}