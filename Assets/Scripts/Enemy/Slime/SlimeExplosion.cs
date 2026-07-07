using UnityEngine;

public class SlimeExplosion : MonoBehaviour
{
    protected DamageReceiver damageReceiver;
    [SerializeField] protected GameObject explosionEffectPrefab;
    private bool isDead = false;

    private void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
    }

    private void Update()
    {
        if (isDead) return;
        if (damageReceiver.IsDead())
        {
            isDead = true;
            GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            explosion.transform.SetParent(transform);
            Destroy(explosion, 0.5f);
        }
    }
}
