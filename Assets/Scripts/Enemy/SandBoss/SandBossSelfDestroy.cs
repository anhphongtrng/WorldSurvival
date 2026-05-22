using UnityEngine;

public class SandBossSelfDestroy : SelfDestroy
{
    protected SandBossController sandBossController;

    protected void Awake()
    {
        sandBossController = GetComponent<SandBossController>();
    }

    void Update()
    {
        DestroySelf();
    }

    public override void DestroySelf()
    {
        if (isDead) return;

        if (sandBossController.damageReceiver.IsDead())
        {
            isDead = true;
            GameController.instance.OnMiniBossDead();
            DropItems();
            Destroy(gameObject, 1f);
        }
    }
}
