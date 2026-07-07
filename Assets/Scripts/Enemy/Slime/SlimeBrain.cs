using UnityEngine;

public class SlimeBrain : EnemyBrain
{
    [Header("Slime – Components")]
    public SlimeMovement movement;
    public SlimeAnimation animations;

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<SlimeMovement>();
        animations = GetComponent<SlimeAnimation>();
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
