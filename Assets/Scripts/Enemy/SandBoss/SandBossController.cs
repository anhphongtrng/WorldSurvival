using UnityEngine;

public class SandBossController : MonoBehaviour
{
    public DamageReceiver damageReceiver;

    protected void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
    }
}
