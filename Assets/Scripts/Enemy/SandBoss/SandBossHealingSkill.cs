using UnityEngine;

public class SandBossHealingSkill : EnemySkills
{
    protected SandBossController sandBossController;
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
        float missingHP = sandBossController.damageReceiver.GetMaxHP() - sandBossController.damageReceiver.GetCurrentHP();
        float healAmount = missingHP * 0.3f;
        Debug.Log($"Healing for {healAmount} HP");
        sandBossController.damageReceiver.ChangeHP(healAmount);
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
