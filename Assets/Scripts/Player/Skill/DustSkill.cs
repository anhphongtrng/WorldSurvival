using UnityEngine;

public class DustSkill : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.CompareTag("Enemy"))
        {
            DamageReceiver damageReceiver = collision.gameObject.GetComponent<DamageReceiver>();
            if (damageReceiver != null)
                damageReceiver.TakeDamage(10);
            else return;
        }
    }
}
