using UnityEngine;

public class BulletSelfDestroy : SelfDestroy
{
    protected float lifetime = 3f;

    void Update()
    {
        DestroySelf();
    }

    public override void DestroySelf()
    {
        Destroy(gameObject, lifetime);
    }
}
