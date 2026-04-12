using UnityEngine;

public class DamageSender : MonoBehaviour
{
    [SerializeField] protected float baseDamage;
    [SerializeField] protected float bonusDamage = 0f;

    protected virtual void Awake()
    {
        baseDamage = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ColliderDamageSender(collision);
    }

    protected virtual void ColliderDamageSender(Collider2D collision)
    {
        DamageReceiver damageReceiver = collision.gameObject.GetComponent<DamageReceiver>();
        damageReceiver.TakeDamage(baseDamage);
    }

}