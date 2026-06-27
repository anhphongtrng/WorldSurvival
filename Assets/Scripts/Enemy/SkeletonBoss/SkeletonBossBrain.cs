using System.Collections;
using UnityEngine;


public class SkeletonBossBrain : EnemyBrain
{
    [Header("Skeleton Boss – Components")]
    public SkeletonBossMovement movement;
    public SkeletonBossAnimation animations;

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<SkeletonBossMovement>();
        animations = GetComponent<SkeletonBossAnimation>();
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