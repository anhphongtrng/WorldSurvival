using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        moveSpeed = 2f;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract void Move();

    protected virtual void FlipEnemy()
    {
        spriteRenderer.flipX = PlayerController.instance.transform.position.x > transform.position.x;
    }

}
