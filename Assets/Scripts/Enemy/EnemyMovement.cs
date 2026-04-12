using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] protected float moveSpeed;

    protected virtual void Awake()
    {
        moveSpeed = 2f;
    }

    public abstract void Move();

    protected void FlipEnemy()
    {
        transform.localScale = new Vector3(PlayerController.instance.transform.position.x < transform.position.x ? 1 : -1, 1, 1);
    }

}
