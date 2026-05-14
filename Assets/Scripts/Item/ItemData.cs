using UnityEngine;

public class ItemData : MonoBehaviour
{
    public float GetDropRate()
    {
        if (CompareTag("HealItem"))
        {
            return StatsController.instance.healItemDropRate;
        }
        else if (CompareTag("BeamDamageBuffItem"))
        {
            return StatsController.instance.beamDamageBuffItemDropRate;
        }
        else if (CompareTag("BonusTimeItem"))
        {
            return StatsController.instance.bonusTimeItemDropRate;
        }

        return 0f;
    }
}