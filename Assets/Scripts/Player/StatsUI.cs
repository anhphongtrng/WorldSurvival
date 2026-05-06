using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class StatsUI : MonoBehaviour
{
    [SerializeField] protected GameObject[] statsSlot;
    protected bool isCanvasOpen = false;
    [SerializeField] protected Canvas statsCanvas;

    protected void Start()
    {
        statsCanvas.enabled = false;
    }

    protected void Update()
    {
        OpenStatsCanvas();
    }
    public void UpdateBeamDamage()
    {
        statsSlot[0].GetComponentInChildren<TextMeshProUGUI>().text = "Beam Damage: " + StatsController.instance.beamWeaponDamage.ToString();
    }

    public void UpdateGunDamage()
    {
        statsSlot[1].GetComponentInChildren<TextMeshProUGUI>().text = "Gun Damage: " + StatsController.instance.gunWeaponDamage.ToString();
    }

    public void UpdateMoveSpeed()
    {
        statsSlot[2].GetComponentInChildren<TextMeshProUGUI>().text = "Move Speed: " + StatsController.instance.moveSpeed.ToString();
    }

    public void UpdateAllStats()
    {
        UpdateBeamDamage();
        UpdateGunDamage();
        UpdateMoveSpeed();
    }

    public void OpenStatsCanvas()
    {
        if(Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if(isCanvasOpen)
            {
                Time.timeScale = 1f;
                UpdateAllStats();
                statsCanvas.enabled = false;
                isCanvasOpen = false;
            }
            else
            {
                Time.timeScale = 0f;
                UpdateAllStats();
                statsCanvas.enabled = true;
                isCanvasOpen = true;
            }
        }
    }
}
