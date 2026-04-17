using UnityEngine;

public class FlySelfDestroy : SelfDestroy
{
    protected FlyController flyController;

    protected void Awake()
    {
        flyController = GetComponent<FlyController>();
    }

    void Update()
    {
        DestroySelf();
    }

    public override void DestroySelf()
    {
        if (flyController.damageReceiver.IsDead())
        {
            flyController.flyAnimation.SetDead(true);
            Destroy(gameObject, 1f);
        }
    }
}
