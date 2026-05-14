using UnityEngine;

public class PlayerGetItem : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealItem"))
        {
            StatsController.instance.UpdateCurrentHealth(1f);
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("BeamDamageBuffItem"))
        {
            float buffDuration = 10f;
            float realDuration = Mathf.Min(buffDuration, Timer.instance.remaningTime);
            StatsController.instance.AddTemporaryBeamDamage(5, realDuration);
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("BonusTimeItem"))
        {
            Timer.instance.AddTime(10f);
            Destroy(collision.gameObject);
        }
    }
}
