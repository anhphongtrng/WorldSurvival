using UnityEngine;

public class GoblinBossMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private float defaultSpeed;
    public bool isFacingRight = true;
    public bool isWalking = true;
    public bool isBoosting = false;

    private Rigidbody2D rb;
    private EnemyBrain brain;
    private GoblinBossAnimation goblinBossAnimation;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        brain = GetComponent<EnemyBrain>();
        goblinBossAnimation = GetComponent<GoblinBossAnimation>();
        defaultSpeed = moveSpeed;
    }

    public void MoveToTarget()
    {
        if (brain.target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, moveSpeed * Time.deltaTime);

        goblinBossAnimation.SetWalking(true);
        goblinBossAnimation.SetSpeedBoosting(isBoosting);
        FlipEnemy();
    }

    public void StopMove()
    {
        rb.linearVelocity = Vector2.zero;

        goblinBossAnimation.SetWalking(false);
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        moveSpeed = defaultSpeed * multiplier;
    }

    public void ResetSpeed()
    {
        moveSpeed = defaultSpeed;
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
}
