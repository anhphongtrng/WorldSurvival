using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textTimer;
    [SerializeField] protected Timer timer;
    [SerializeField] protected Canvas skillsCanvas;
    [SerializeField] protected TextMeshProUGUI bossProgressText;

    protected void Start()
    {
        skillsCanvas.enabled = false;
    }

    protected void Update()
    {
        SetTextTimer();
        SetSkillsCanvas();
        SetBossProgressText();
    }

    public void SetTextTimer()
    {
        textTimer.text = string.Format("{0:00}:{1:00}", timer.minutes, timer.seconds);
    }

    public void SetSkillsCanvas()
    {
        if (timer.IsOverTime())
        {
            skillsCanvas.enabled = true;
        }
    }

    public void SetBossProgressText()
    {
        bossProgressText.text = "Defeat enemies to summon Boss: " + EnemySpawner.instance.enemiesKilled + "/20";
    }
}
