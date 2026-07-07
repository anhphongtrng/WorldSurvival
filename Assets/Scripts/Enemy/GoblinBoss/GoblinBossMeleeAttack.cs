using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GoblinBossMeleeAttack : EnemySkill
{
    [Header("Components")]
    [SerializeField] protected GoblinBossAnimation goblinBossAnimation;

    [Header("Distance")]
    [SerializeField] protected float attackRange = 2f;

    [Header("Timing")]
    [SerializeField] protected float attackDelay = 0.4f;
    [SerializeField] protected float animationEndDelay = 0.3f;

    protected override void Awake()
    {
        base.Awake();
        goblinBossAnimation = GetComponent<GoblinBossAnimation>();
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
        float distance = Vector2.Distance(transform.position, brain.target.position);
        return distance <= attackRange;
    }

    public override IEnumerator Execute()
    {
        StartCoroutine(StartCooldown());
        goblinBossAnimation.TriggerAttack2();
        yield return new WaitForSeconds(attackDelay);
        Debug.Log("Melee Damage");
        yield return new WaitForSeconds(animationEndDelay);
    }
}
