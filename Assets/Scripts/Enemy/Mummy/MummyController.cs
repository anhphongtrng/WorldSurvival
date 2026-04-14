using UnityEngine;

public class MummyController : MonoBehaviour
{
    public DamageReceiver damageReceiver;
    public MummyAnimation mummyAnimation;

    private void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        mummyAnimation = GetComponent<MummyAnimation>();
    }
}
