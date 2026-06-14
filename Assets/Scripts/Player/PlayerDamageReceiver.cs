using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    [Header("Music")]
    public AudioClip playerHitClip;

    protected override void Start()
    {
        StatsController.instance.currentHealth = StatsController.instance.maxHealth;
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        StatsController.instance.currentHealth -= dmg;
        AudioController.instance.PlaySFX(playerHitClip);
    }

    public override float GetCurrentHP()
    {
        return StatsController.instance.currentHealth;
    }

    public override float GetMaxHP()
    {
        return StatsController.instance.maxHealth;
    }

    public override void SetHP(float newHp)
    {
        StatsController.instance.currentHealth = Mathf.Clamp(newHp, 0, StatsController.instance.maxHealth);
    }

    public override void ChangeHP(float amount)
    {
        if (IsDead()) return;

        StatsController.instance.currentHealth += amount;
        StatsController.instance.currentHealth = Mathf.Clamp(StatsController.instance.currentHealth, 0, StatsController.instance.maxHealth);
    }

    public override bool IsDead()
    {
        return StatsController.instance.currentHealth <= 0;
    }
}
