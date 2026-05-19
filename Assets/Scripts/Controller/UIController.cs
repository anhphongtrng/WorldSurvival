using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] protected TextMeshProUGUI textTimer;
    [SerializeField] protected Timer timer;
    [SerializeField] protected Canvas talentsCanvas;
    [SerializeField] protected TextMeshProUGUI bossProgressText;
    [SerializeField] protected TextMeshProUGUI skillPointProgressText;
    [SerializeField] protected GameObject gamePauseMenu;
    [SerializeField] protected GameObject gameOverMenu;

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        talentsCanvas = GameObject.Find("TalentsCanvas").GetComponent<Canvas>();    
    }

    protected void Start()
    {
        talentsCanvas.enabled = false;
        ShowGameOverMenu(false);
        ShowGamePauseMenu(false);
    }

    protected void Update()
    {
        SetTextTimer();
        SetTalentsCanvas();
        SetBossProgressText();
        SetSkillPointProgressText();
    }

    public void SetTextTimer()
    {
        textTimer.text = string.Format("{0:00}:{1:00}", timer.minutes, timer.seconds);
    }

    public void SetTalentsCanvas()
    {
        if (timer.IsOverTime())
        {
            talentsCanvas.enabled = true;
        }
        else
        {
            talentsCanvas.enabled = false;
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

    public void ShowGameOverMenu(bool value)
    {
        gameOverMenu.SetActive(value);
    }

    public void ShowGamePauseMenu(bool value)
    {
        gamePauseMenu.SetActive(value);
    }
}
