using UnityEngine;

public class HitBoxDamageSender : DamageSender
{
    protected override void ColliderDamageSender(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            base.ColliderDamageSender(collision);
        }
    }

}
