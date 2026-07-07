using UnityEngine;

public class GoblinBossBrain : EnemyBrain
{
    [Header("Goblin Boss – Components")]
    public GoblinBossMovement movement;
    public GoblinBossAnimation animations;
    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<GoblinBossMovement>();
        animations = GetComponent<GoblinBossAnimation>();
    }

    protected override void Start()
    {
        base.Start();
        chaseRange = 15f;
    }

    protected override void StopMovement()
    {
        movement.StopMove();
    }

    protected override void SetWalkingAnimation(bool isWalking)
    {
        animations.SetWalking(isWalking);
    }

    protected override void StartMoveToTarget()
    {
        movement.MoveToTarget();
    }
}
