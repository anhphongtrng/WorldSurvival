using UnityEngine;

public class DeceasedSelfDestroy : SelfDestroy
{
    protected DamageReceiver damageReceiver;
    protected DeceasedAnimation deceasedAnimation;
    protected DeceasedBrain deceasedBrain;
    protected void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        deceasedAnimation = GetComponent<DeceasedAnimation>();
        deceasedBrain = GetComponent<DeceasedBrain>();
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
            deceasedBrain.Dead();
            deceasedAnimation.TriggerDeath();
            EnemySpawner.instance.OnEnemiesKilled(gameObject);
            EnemySpawner.instance.aliveEnemies.Remove(gameObject);
            DropItems();
            Destroy(gameObject, 0.5f);
        }
    }
}
