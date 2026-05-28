using UnityEngine;

public class SandBossHealingSkill : EnemySkills
{
    protected SandBossController sandBossController;
    [SerializeField] protected GameObject healingEffectPrefab;
    protected override void Awake()
    {
        base.Awake();
        skillCooldown = 10f;
        sandBossController = GetComponent<SandBossController>();
    }

    protected override void Update()
    {
        Healing();
    }

    protected override void OnSkill()
    {
        GameObject healingEffect = Instantiate(healingEffectPrefab, transform.position, Quaternion.identity);
        healingEffect.transform.SetParent(transform);
        float missingHP = sandBossController.damageReceiver.GetMaxHP() - sandBossController.damageReceiver.GetCurrentHP();
        float healAmount = missingHP * 0.3f;
        Debug.Log($"Healing for {healAmount} HP");
        sandBossController.damageReceiver.ChangeHP(healAmount);
        Destroy(healingEffect, 1f);
    }

    protected void Healing()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        if (distanceToPlayer <= 10f)
        {
            UseSkill();
        }
    }
}
