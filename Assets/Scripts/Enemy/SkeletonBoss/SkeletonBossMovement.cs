using UnityEngine;

public class SkeletonBossMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeedd = 2f;

    private float defaultSpeed;
    private bool isFacingRight = true;
    public bool isWalking = true;
    public bool isBoosting = false;

    private Rigidbody2D rb;

    private EnemyBrain brain;

    private SkeletonBossAnimation skeletonBossAnimation;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        brain = GetComponent<EnemyBrain>();

        skeletonBossAnimation = GetComponent<SkeletonBossAnimation>();

        defaultSpeed = moveSpeedd;
    }

    public void MoveToTarget()
    {
        if (brain.target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, moveSpeedd * Time.deltaTime);

        skeletonBossAnimation.SetWalking(true);
        skeletonBossAnimation.SetSpeedBoosting(isBoosting);
        FlipEnemy();
    }

    public void StopMove()
    {
        rb.linearVelocity = Vector2.zero;

        skeletonBossAnimation.SetWalking(false);
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        moveSpeedd = defaultSpeed * multiplier;
    }

    public void ResetSpeed()
    {
        moveSpeedd = defaultSpeed;
    }

    protected void FlipEnemy()
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

    public void Move()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < 10f)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, moveSpeedd * Time.deltaTime);
            skeletonBossAnimation.SetWalking(true);
            FlipEnemy();
        }
        else
        {
            skeletonBossAnimation.SetWalking(false);
        }
    }
}
