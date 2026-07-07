using UnityEngine;

public class SnowBossBrain : EnemyBrain
{
    [Header("SnowBoss – Components")]
    public SnowBossMovement movement;
    public SnowBossAnimation animations;

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<SnowBossMovement>();
        animations = GetComponent<SnowBossAnimation>();
    }

    protected override void StopMovement()
    {
        movement.StopMove();
    }

    protected override void SetWalkingAnimation(bool isWalking)
    {
        animations.SetRunning(isWalking);
    }

    protected override void StartMoveToTarget()
    {
        movement.MoveToTarget();
    }
}
