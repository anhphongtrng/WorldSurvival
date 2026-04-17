using UnityEngine;

public class FlyController : MonoBehaviour
{
    public DamageReceiver damageReceiver;
    public FlyAnimation flyAnimation;

    protected void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        flyAnimation = GetComponent<FlyAnimation>();
    }
}
