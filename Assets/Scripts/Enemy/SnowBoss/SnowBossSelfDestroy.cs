using UnityEngine;

public class SnowBossSelfDestroy : SelfDestroy
{
    protected DamageReceiver damageReceiver;
    protected SnowBossAnimation snowBossAnimation;

    protected void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        snowBossAnimation = GetComponent<SnowBossAnimation>();
    }

    void Update()
    {
        DestroySelf();
    }

    public override void DestroySelf()
    {
        if (isDead) return;

        if (damageReceiver.IsDead())
        {
            isDead = true;
            GameController.instance.OnMiniBossDead();
            snowBossAnimation.TriggerDeath();
            DropItems();
            Destroy(gameObject, 0.5f);
        }
    }
}
