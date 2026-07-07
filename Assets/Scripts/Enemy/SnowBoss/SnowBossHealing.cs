using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SnowBossHealing : EnemySkill
{
    [Header("Components")]
    [SerializeField] protected SnowBossAnimation snowBossAnimation;
    [SerializeField] protected DamageReceiver damageReceiver;

    [Header("Prefab")]
    [SerializeField] protected GameObject healingEffectPrefab;

    [Header("Distance")]
    [SerializeField] protected float minDistance = 1f;
    [SerializeField] protected float maxDistance = 8f;

    [Header("Timing")]
    [SerializeField] protected float throwDelay = 0.5f;


    protected override void Awake()
    {
        base.Awake();
        snowBossAnimation = GetComponent<SnowBossAnimation>();
        damageReceiver = GetComponent<DamageReceiver>();
    }

    private void Start()
    {
        stopMovementWhenUseSkill = true;
        cooldown = 10f;
    }

    public override bool CanUse()
    {
        if (!base.CanUse())
            return false;
        float distance = Vector2.Distance(transform.position, brain.target.position);
        return distance >= minDistance && distance <= maxDistance && damageReceiver.GetCurrentHP() < damageReceiver.GetMaxHP() * 0.5f; // Only use healing skill if HP is below 50%
    }

    public override IEnumerator Execute()
    {
        StartCoroutine(StartCooldown());
        snowBossAnimation.TriggerAttack();
        yield return new WaitForSeconds(throwDelay);
        HealingSkill();
    }

    public void HealingSkill()
    {
        GameObject healingEffect = Instantiate(healingEffectPrefab, transform.position, Quaternion.identity);
        healingEffect.transform.SetParent(transform);
        float missingHP = damageReceiver.GetMaxHP() - damageReceiver.GetCurrentHP();
        float healAmount = missingHP * 0.3f;
        Debug.Log($"Healing for {healAmount} HP");
        damageReceiver.ChangeHP(healAmount);
        Destroy(healingEffect, 1f);
    }
}
