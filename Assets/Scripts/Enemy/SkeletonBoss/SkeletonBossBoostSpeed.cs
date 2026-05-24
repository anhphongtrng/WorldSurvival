using System.Collections;
using UnityEngine;

public class SkeletonBossBoostSpeed : EnemySkill
{
    [Header("Components")]
    [SerializeField] protected SkeletonBossAnimation skeletonBossAnimation;
    [SerializeField] protected SkeletonBossMovement movement;

    [Header("Distance")]
    [SerializeField] protected float minDistance = 4f;
    [SerializeField] protected float maxDistance = 8f;

    [Header("Timing")]
    [SerializeField] protected float speedMultiplier = 2f;
    [SerializeField] protected float boostDuration = 3f;

    protected override void Awake()
    {
        base.Awake();

        skeletonBossAnimation = GetComponent<SkeletonBossAnimation>();
        movement = GetComponent<SkeletonBossMovement>();
    }

    private void Start()
    {
        stopMovementWhenUseSkill = false;
        cooldown = 10f;
    }

    public override bool CanUse()
    {
        if (!base.CanUse())
            return false;

        float distance = Vector2.Distance(transform.position, brain.target.position);

        return distance >= minDistance && distance <= maxDistance;
    }

    public override IEnumerator Execute()
    {
        StartCoroutine(StartCooldown());
        movement.isBoosting = true;
        yield return new WaitForSeconds(0.5f);
        movement.SetSpeedMultiplier(speedMultiplier);
        Debug.Log("Boost Speed");
        StartCoroutine(RemoveBoost());
    }

    private IEnumerator RemoveBoost()
    {
        yield return new WaitForSeconds(boostDuration);
        movement.ResetSpeed();
        movement.isBoosting = false;
    }
}
