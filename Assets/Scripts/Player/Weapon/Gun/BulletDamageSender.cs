using UnityEngine;

public class BulletDamageSender : DamageSender
{
    protected override void ColliderDamageSender(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageReceiver damageReceiver = collision.gameObject.GetComponent<DamageReceiver>();
            if (damageReceiver != null)
            {
                damageReceiver.TakeDamage(StatsController.instance.gunWeaponDamage);
                Destroy(gameObject);
            }
        }
        
    }
}
