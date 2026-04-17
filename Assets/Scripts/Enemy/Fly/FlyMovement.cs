using UnityEngine;

public class FlyMovement : EnemyMovement
{
    [SerializeField] protected Transform posA;
    [SerializeField] protected Transform posB;

    private Transform target;

    protected void Start()
    {
        target = posA;
    }

    protected void Update()
    {
        Move();
    }

    protected override void FlipEnemy()
    {
        float direction = target.position.x - transform.position.x;
        transform.localScale = new Vector3(
            direction < 0 ? 1 : -1,
            1,
            1
        );
    }

    public override void Move()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            target = (target == posA) ? posB : posA;
            FlipEnemy();
        }
    }
}