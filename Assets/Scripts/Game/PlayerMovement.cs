using UnityEngine;

public class PlayerMovement : Actor
{
    Camera mainCam;

    [SerializeField] FloatObject moveSpeed;
    const float MovementThreshold = 0.01f;

    protected override void Awake()
    {
        base.Awake();
        mainCam = Camera.main;
    }

    void Update()
    {
        GetMovementInput();
    }

    void GetMovementInput()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 moveDirection = Vector3.ClampMagnitude(mousePos - transform.position, 1f);
        moveDirection.z = 0f;

        if (moveDirection.magnitude > MovementThreshold)
        {
            float zRot = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
            transform.eulerAngles = zRot * Vector3.forward;

            transform.Translate(moveSpeed.Value * Time.deltaTime * transform.up, Space.World);
        }
    }
}