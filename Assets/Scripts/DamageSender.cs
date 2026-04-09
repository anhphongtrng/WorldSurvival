using UnityEngine;

public class DamageSender : MonoBehaviour
{
    public float damage = 0.1f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ColliderDamageSender(collision);
    }

    protected virtual void ColliderDamageSender(Collider2D collision)
    {
        DamageReceiver damageReceiver = collision.gameObject.GetComponent<DamageReceiver>();
        damageReceiver.TakeDamage(damage);
    }

}