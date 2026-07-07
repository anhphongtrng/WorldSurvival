using UnityEngine;

public class GoblinBossController : MonoBehaviour
{
    public DamageReceiver damageReceiver;
    public GoblinBossAnimation goblinBossAnimation;
    public EnemyBrain enemyBrain;

    private void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        goblinBossAnimation = GetComponent<GoblinBossAnimation>();
        enemyBrain = GetComponent<EnemyBrain>();
    }
}
