using UnityEngine;

public class PlayerGetItem : MonoBehaviour
{
    [Header("Music")]
    public AudioClip getItemClip; // Music played when player gets an item

    [Header("Prefabs")]
    public GameObject healingPrefab;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealItem"))
        {
            AudioController.instance.PlaySFX(getItemClip);
            StatsController.instance.UpdateCurrentHealth(1f);
            GameObject healingEffect = Instantiate(healingPrefab, transform.position, Quaternion.identity);
            healingEffect.transform.SetParent(transform);
            Destroy(collision.gameObject);
            Destroy(healingEffect, 1f);
        }

        else if (collision.CompareTag("BeamDamageBuffItem"))
        {
            AudioController.instance.PlaySFX(getItemClip);
            float buffDuration = 10f;
            float realDuration = Mathf.Min(buffDuration, Timer.instance.remaningTime);
            StatsController.instance.AddTemporaryBeamDamage(5, realDuration);
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("GunDamageBuffItem"))
        {
            AudioController.instance.PlaySFX(getItemClip);
            float buffDuration = 10f;
            float realDuration = Mathf.Min(buffDuration, Timer.instance.remaningTime);
            StatsController.instance.AddTemporaryGunDamage(5, realDuration);
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("BonusTimeItem"))
        {
            AudioController.instance.PlaySFX(getItemClip);
            Timer.instance.AddTime(10f);
            Destroy(collision.gameObject);
        }
    }
}
