using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public float remaningTime;
    public int minutes;
    public int seconds;

    protected void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    protected void Start()
    {
        remaningTime = 30f;
    }

    protected void Update()
    {
        UpdateTime();
        StopGame();
    }

    public void UpdateTime()
    {
        remaningTime -= Time.deltaTime;
        minutes = Mathf.FloorToInt(remaningTime / 60f);
        seconds = Mathf.FloorToInt(remaningTime % 60f);
    }

    public bool IsOverTime()
    {
        if (remaningTime <= 0f)
        {
            remaningTime = 0f;
            return true;
        }
        return false;
    }

    public void StopGame()
    {
        if(IsOverTime())
        {
            GamePauseController.instance.SetOverTimePause(true);
        }
        else
        {
            GamePauseController.instance.SetOverTimePause(false);
        }
    }

    public void AddTime(float amount)
    {
        remaningTime += amount;
    }
}
