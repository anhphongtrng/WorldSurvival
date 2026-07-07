using UnityEngine;

public class GoblinBossSelfDestroy : SelfDestroy
{
    [SerializeField] protected float delayBeforeEndGame = 3f;

    protected GoblinBossAnimation goblinBossAnimation;
    protected DamageReceiver damageReceiver;
    protected GoblinBossBrain goblinBossBrain;

    protected void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        goblinBossAnimation = GetComponent<GoblinBossAnimation>();
        goblinBossBrain = GetComponent<GoblinBossBrain>();
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
            goblinBossBrain.Dead();
            goblinBossAnimation.TriggerDeath();
            DropItems();
            StartCoroutine(GameController.instance.OnFinalBossDeadOrWinGame(gameObject));
        }
    }
}
