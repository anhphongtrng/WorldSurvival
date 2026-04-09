using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] protected Vector2 moveInput;
    protected float moveSpeed = 5f;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MoveInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void MoveInput()
    {
        moveInput = Vector2.zero;

        if(Keyboard.current.wKey.isPressed)
        {
            moveInput.y = 1f;
        }
        if(Keyboard.current.sKey.isPressed)
        {
            moveInput.y = -1f;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            moveInput.x = -1f;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            moveInput.x = 1f;
        }

        moveInput = moveInput.normalized;
    }

    public void Move()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
}
