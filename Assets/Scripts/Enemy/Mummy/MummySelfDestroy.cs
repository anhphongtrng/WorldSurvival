using UnityEngine;

public class MummySelfDestroy : SelfDestroy
{
    protected MummyController mummyController;

    protected void Awake()
    {
        mummyController = GetComponent<MummyController>();
    }

    void Update()
    {
        DestroySelf();
    }

    public override void DestroySelf()
    {
        if (mummyController.damageReceiver.IsDead())
        {
            mummyController.mummyAnimation.SetDead(true);
            Destroy(gameObject, 1f);
        }
    }
}
