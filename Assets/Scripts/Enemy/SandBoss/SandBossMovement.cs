using UnityEngine;

public class SandBossMovement : EnemyMovement
{
    protected override void Awake()
    {
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

    protected override void FlipEnemy()
    {
        Vector3 scale = transform.localScale;

        scale.x = Mathf.Abs(scale.x) *
                  (PlayerController.instance.transform.position.x < transform.position.x ? 1 : -1);

        transform.localScale = scale;
    }
}
