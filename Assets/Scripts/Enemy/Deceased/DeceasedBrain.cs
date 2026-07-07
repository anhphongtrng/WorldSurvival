using UnityEngine;

public class DeceasedBrain : EnemyBrain
{
    [Header("Deceased – Components")]
    public DeceasedMovement movement;
    public DeceasedAnimation animations;

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<DeceasedMovement>();
        animations = GetComponent<DeceasedAnimation>();
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
