using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    new Transform transform;
    new Collider2D collider;

    Vector3 inputDirection;
    Vector3 lastNonzeroInputDirection;

    Vector3 velocity;

    [SerializeField] float moveSpeed;

    void Awake()
    {
        transform = GetComponent<Transform>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        GetMovementInput();
        UpdateRotation();
    }

    void GetMovementInput()
    {
        // get input
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");

        // update lastNonzeroInputDirection if necessary
        if (inputDirection != lastNonzeroInputDirection && inputDirection != Vector3.zero)
        {
            lastNonzeroInputDirection = inputDirection;
        }

        // set velocity
        velocity = moveSpeed * inputDirection.normalized;

        // apply movement
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

    // set z-rotation based on lastNonzeroInputDirection (to-do: remove eventually)
    void UpdateRotation()
    {
        float zRot = Mathf.Atan2(-lastNonzeroInputDirection.x, lastNonzeroInputDirection.y) * Mathf.Rad2Deg;
        transform.eulerAngles = zRot * Vector3.forward;
    }
}