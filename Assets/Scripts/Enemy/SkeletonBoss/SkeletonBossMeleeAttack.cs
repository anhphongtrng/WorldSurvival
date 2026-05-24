using System.Collections;
using UnityEngine;

public class SkeletonBossMeleeAttack : EnemySkill
{
    [Header("Components")]
    [SerializeField] protected SkeletonBossAnimation skeletonBossAnimation;
    
    [Header("Distance")]
    [SerializeField] protected float attackRange = 2f;
    
    [Header("Timing")]
    [SerializeField] protected float attackDelay = 0.4f;
    [SerializeField] protected float animationEndDelay = 0.3f;

    protected override void Awake()
    {
        base.Awake();

        skeletonBossAnimation = GetComponent<SkeletonBossAnimation>();
    }

    private void Start()
    {
        stopMovementWhenUseSkill = true;
        cooldown = 3f;
    }

    public override bool CanUse()
    {
        if (!base.CanUse())
            return false;

        float distance =
            Vector2.Distance(
                transform.position,
                brain.target.position
            );

        return distance <= attackRange;
    }

    public override IEnumerator Execute()
    {
        StartCoroutine(StartCooldown());

        skeletonBossAnimation.TriggerAttack2();

        yield return new WaitForSeconds(attackDelay);

        Debug.Log("Melee Damage");

        yield return new WaitForSeconds(animationEndDelay);
    }
}
