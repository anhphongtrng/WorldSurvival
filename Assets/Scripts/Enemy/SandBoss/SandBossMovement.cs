using UnityEngine;

public class SandBossMovement : EnemyMovement
{
    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 1f;
    }

    void Update()
    {
        Move();
    }

    public override void Move()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < 10f)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, moveSpeed * Time.deltaTime);
            FlipEnemy();
        }
    }
}
