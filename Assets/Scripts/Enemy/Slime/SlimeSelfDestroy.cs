using UnityEngine;

public class SlimeSelfDestroy : SelfDestroy
{
    protected DamageReceiver damageReceiver;
    protected SlimeAnimation slimeAnimation;
    protected SlimeBrain slimeBrain;

    protected void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        slimeAnimation = GetComponent<SlimeAnimation>();
        slimeBrain = GetComponent<SlimeBrain>();
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
            slimeBrain.Dead();
            slimeAnimation.TriggerDeath();
            EnemySpawner.instance.OnEnemiesKilled(gameObject);
            EnemySpawner.instance.aliveEnemies.Remove(gameObject);
            DropItems();
            Destroy(gameObject, 0.5f);
        }
    }
}
