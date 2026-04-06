using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public DamageReceiver damageReceiver;

    private void Awake()
    {
        instance = this;
        damageReceiver = GetComponent<DamageReceiver>();
    }
}
