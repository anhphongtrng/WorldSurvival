using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankBoardUI : MonoBehaviour
{
    [System.Serializable]
    public class WorldRankInfo
    {
        public string sceneName;
        public string displayName;
        public TextMeshProUGUI timeText;
    }

    [SerializeField] private List<WorldRankInfo> worldRankInfos;

    private void Start()
    {
        ShowWorldRankByTime();
    }

    public void ShowWorldRankByTime()
    {
        foreach (var worldRankInfo in worldRankInfos)
        {
            if (RankController.HasRecord(worldRankInfo.sceneName))
            {
                float bestTime = RankController.GetBestTime(worldRankInfo.sceneName);
                worldRankInfo.timeText.text = worldRankInfo.displayName + ": " + bestTime.ToString("F2") + "s";
            }
            else
            {
                worldRankInfo.timeText.text = worldRankInfo.displayName + ": No record";
            }
        }
    }
}
