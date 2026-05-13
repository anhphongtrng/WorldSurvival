using UnityEngine;

public class FlySelfDestroy : SelfDestroy
{
    protected FlyController flyController;

    protected void Awake()
    {
        flyController = GetComponent<FlyController>();
    }

    void Update()
    {
        DestroySelf();
    }

    public override void DestroySelf()
    {
        if(isDead) return;

        if (flyController.damageReceiver.IsDead())
        {
            isDead = true;
            flyController.flyAnimation.SetDead(true);
            EnemySpawner.instance.OnEnemiesKilled(gameObject);
            EnemySpawner.instance.aliveEnemies.Remove(gameObject);
            Destroy(transform.parent.gameObject, 1f);
        }
    }
}
