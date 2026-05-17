using UnityEngine;

public class TalentController : MonoBehaviour
{
    protected void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += SkillPointSpentFor;
    }

    protected void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= SkillPointSpentFor;
    }

    public void SkillPointSpentFor(SkillSlot skillSlot)
    {
        string skillName = skillSlot.skillSO.skillName;

        switch (skillName)
        {
            case "Max Health Boost":
                StatsController.instance.UpdateMaxHealth(1);
                break;
            case "Attack Boost":
                StatsController.instance.UpdateBeamDamage(1);
                break;
            case "Speed Boost":
                StatsController.instance.UpdateMoveSpeed(1f);
                break;
            case "Heal Item Drop Rate Boost":
                StatsController.instance.UpdateHealItemDropRate(0.01f);
                break;
            case "Beam Damage Buff Item Drop Rate Boost":
                StatsController.instance.UpdateBeamDamageBuffItemDropRate(0.01f);
                break;
            case "Bonus Time Item Drop Rate Boost":
                StatsController.instance.UpdateBonusTimeItemDropRate(0.01f);
                break;
        }
    }
}
