using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textTimer;
    [SerializeField] protected Timer timer;
    [SerializeField] protected Canvas skillsCanvas;
    [SerializeField] protected TextMeshProUGUI bossProgressText;
    [SerializeField] protected TextMeshProUGUI skillPointProgressText;

    protected void Awake()
    {
        skillsCanvas = GameObject.Find("SkillsCanvas").GetComponent<Canvas>();
    }

    protected void Start()
    {
        skillsCanvas.enabled = false;
    }

    protected void Update()
    {
        SetTextTimer();
        SetSkillsCanvas();
        SetBossProgressText();
        SetSkillPointProgressText();
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
        else
        {
            skillsCanvas.enabled = false;
        }
    }

    public void SetBossProgressText()
    {
        int current = Mathf.Min(EnemySpawner.instance.enemiesKilled, 20);

        bossProgressText.text =
            "- Defeat enemies to summon Boss: " + current + "/20";

        if (GameController.instance.isBossSpawned)
        {
            bossProgressText.text = "- Boss is summoned!";
        }
    }

    public void SetSkillPointProgressText()
    {
        int current = Mathf.Min(EnemySpawner.instance.enemiesKilled, 10);

        skillPointProgressText.text =
            "- Defeat enemies to get Skill Point: " + EnemySpawner.instance.enemiesKilled % 10 + "/10";

        if (EnemySpawner.instance.enemiesKilled % 10 == 0)
        {
            current = EnemySpawner.instance.enemiesKilled % 10;
        }
    }
}
