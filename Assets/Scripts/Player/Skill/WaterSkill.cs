using Unity.VisualScripting;
using UnityEngine;

public class WaterSkill : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == null) return;
        if (collision.CompareTag("Enemy"))
        {
            DamageReceiver damageReceiver = collision.gameObject.GetComponent<DamageReceiver>();
            damageReceiver.TakeDamage(10);
        }
    }
}