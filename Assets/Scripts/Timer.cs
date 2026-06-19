using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public float remaningTime;
    private float runStartTime; // Thoi gian khi game bat dau chay
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
        remaningTime = 60f;
        runStartTime = Time.time; // Luu thoi gian khi game bat dau chay
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

    public void ResetTime()
    {
        remaningTime = 60f;
    }

    public void SetTimeToZero()
    {
        remaningTime = 0f;
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

    public float GetElapsedTime()
    {
        return Time.time - runStartTime; // Tinh thoi gian da troi qua tu khi game bat dau chay
    }
}
