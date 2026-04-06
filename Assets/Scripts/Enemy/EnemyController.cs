using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public DamageReceiver damageReceiver;

    private void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
    }
}
