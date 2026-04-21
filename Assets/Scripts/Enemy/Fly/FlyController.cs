using UnityEngine;

public class FlyController : MonoBehaviour
{
    public DamageReceiver damageReceiver;
    public FlyAnimation flyAnimation;
    public FLyRangeSkill flyRangeSkill;

    protected void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        flyAnimation = GetComponent<FlyAnimation>();
        flyRangeSkill = GetComponent<FLyRangeSkill>();
    }
}
