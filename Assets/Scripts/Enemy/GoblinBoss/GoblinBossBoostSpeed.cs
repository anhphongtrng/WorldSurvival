using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GoblinBossBoostSpeed : EnemySkill
{
    [Header("Components")]
    [SerializeField] protected GoblinBossAnimation goblinBossAnimation;
    [SerializeField] protected GoblinBossMovement movement;
    [Header("Distance")]
    [SerializeField] protected float minDistance = 4f;
    [SerializeField] protected float maxDistance = 8f;

    [Header("Timing")]
    [SerializeField] protected float speedMultiplier = 2f;
    [SerializeField] protected float boostDuration = 3f;

    protected override void Awake()
    {
        base.Awake();

        goblinBossAnimation = GetComponent<GoblinBossAnimation>();
        movement = GetComponent<GoblinBossMovement>();
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
