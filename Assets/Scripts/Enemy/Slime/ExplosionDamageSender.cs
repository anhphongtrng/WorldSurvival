using UnityEngine;

public class ExplosionDamageSender : DamageSender
{
    protected override void ColliderDamageSender(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            base.ColliderDamageSender(collision);
            Destroy(gameObject);
        }
    }
}
