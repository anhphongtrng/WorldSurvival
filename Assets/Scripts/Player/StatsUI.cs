using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class StatsUI : MonoBehaviour
{
    public static StatsUI instance;
    [SerializeField] protected GameObject[] statsSlot;
    public bool isCanvasOpen = false;
    [SerializeField] protected Canvas statsCanvas;

    protected void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void Start()
    {
        statsCanvas.enabled = false;
    }

    protected void Update()
    {
        OpenStatsCanvas();
    }
    public void UpdateBeamDamageUI()
    {
        statsSlot[0].GetComponentInChildren<TextMeshProUGUI>().text = "Beam Damage: " + StatsController.instance.beamWeaponDamage.ToString();
    }

    public void UpdateGunDamageUI()
    {
        statsSlot[1].GetComponentInChildren<TextMeshProUGUI>().text = "Gun Damage: " + StatsController.instance.gunWeaponDamage.ToString();
    }

    public void UpdateMoveSpeedUI()
    {
        statsSlot[2].GetComponentInChildren<TextMeshProUGUI>().text = "Move Speed: " + StatsController.instance.moveSpeed.ToString();
    }

    public void UpdateHealItemRateUI()
    {
        statsSlot[3].GetComponentInChildren<TextMeshProUGUI>().text = "Heal Item Rate: " + StatsController.instance.healItemDropRate.ToString();
    }

    public void UpdateBeamDamageBuffItemRateUI()
    {
        statsSlot[4].GetComponentInChildren<TextMeshProUGUI>().text = "Beam DMG Buff Item Rate: " + StatsController.instance.beamDamageBuffItemDropRate.ToString();
    }

    public void UpdateBonusTimeItemRateUI()
    {
        statsSlot[5].GetComponentInChildren<TextMeshProUGUI>().text = "Bonus Time Item Rate: " + StatsController.instance.bonusTimeItemDropRate.ToString();
    }

    public void UpdateAllStats()
    {
        UpdateBeamDamageUI();
        UpdateGunDamageUI();
        UpdateMoveSpeedUI();
        UpdateHealItemRateUI();
        UpdateBeamDamageBuffItemRateUI();
        UpdateBonusTimeItemRateUI();
    }

    public void OpenStatsCanvas()
    {
        if(Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if(isCanvasOpen)
            {
                GamePauseController.instance.SetStatPause(false);
                UpdateAllStats();
                statsCanvas.enabled = false;
                isCanvasOpen = false;
            }
            else
            {
                GamePauseController.instance.SetStatPause(true);
                UpdateAllStats();
                statsCanvas.enabled = true;
                isCanvasOpen = true;
            }
        }
    }
}
