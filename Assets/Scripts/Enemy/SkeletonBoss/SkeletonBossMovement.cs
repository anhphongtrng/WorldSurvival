using UnityEngine;

public class SkeletonBossMovement : EnemyMovement
{
    protected SkeletonBossController skeletonBossController;
    protected bool isFacingRight = true;
    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 2f;
        skeletonBossController = GetComponent<SkeletonBossController>();
    }

    void Update()
    {
        Move();
    }

    protected override void FlipEnemy()
    {
        if (PlayerController.instance.transform.position.x > transform.position.x && !isFacingRight)
        {
            isFacingRight = true;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        else if (PlayerController.instance.transform.position.x < transform.position.x && isFacingRight)
        {
            isFacingRight = false;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public override void Move()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < 10f)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, moveSpeed * Time.deltaTime);
            skeletonBossController.skeletonBossAnimation.SetWalking(true);
            FlipEnemy();
        }
        else
        {
            skeletonBossController.skeletonBossAnimation.SetWalking(false);
        }
    }
}
