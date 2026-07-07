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
            case "Beam Attack Boost":
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
            case "Gun Damage Buff Item Drop Rate Boost":
                StatsController.instance.UpdateGunDamageBuffItemDropRate(0.01f);
                break;
            case "Bonus Time Item Drop Rate Boost":
                StatsController.instance.UpdateBonusTimeItemDropRate(0.01f);
                break;
            case "Beam Attack Range Boost":
                StatsController.instance.UpdateBeamAttackRange(0.5f);
                break;
            case "Beam Count Add":
                StatsController.instance.UpdateBeamLineCount(1);
                break;
            case "Gun Attack Boost":
                StatsController.instance.UpdateGunDamage(1);
                break;
            case "Max Bullet Per Shot Boost":
                StatsController.instance.UpdateMaxBulletsPerShot(1);
                break;
        }
    }
}
