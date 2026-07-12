using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RankBoardUI : MonoBehaviour
{
    [System.Serializable]
    public class WorldRankInfo
    {
        public string sceneName;
        public string worldLabel; // ten hien thi cua world (VD: "World 1")
        public TextMeshProUGUI timeText;       // hien thi thoi gian cua CHINH BAN
        public TextMeshProUGUI globalBestText; // hien thi nguoi giu ky luc toan server (co the de trong neu khong can)
    }

    [Serializable]
    private class FirebaseRankData
    {
        public float time;
        public string displayName;
        public long timestamp;
    }

    [Serializable]
    private class RankEntry
    {
        public string userId;
        public float time;
        public string displayName;
    }

    [SerializeField] private List<WorldRankInfo> worldRankInfos;

    private void Start()
    {
        ShowWorldRankByTime();

        if (FirebaseController.instance != null && !string.IsNullOrEmpty(FirebaseAuthController.CurrentUserId))
        {
            FetchRanksFromFirebase();
            FetchGlobalBestForAllWorlds();
        }
    }

    // Hien thi truoc bang du lieu local (da scope theo userId, chac chan dung cua acc dang login)
    public void ShowWorldRankByTime()
    {
        string playerName = GetCurrentPlayerName();

        foreach (var worldRankInfo in worldRankInfos)
        {
            if (RankController.HasRecord(worldRankInfo.sceneName))
            {
                float bestTime = RankController.GetBestTime(worldRankInfo.sceneName);
                worldRankInfo.timeText.text = worldRankInfo.worldLabel + ": " + playerName + " - " + bestTime.ToString("F2") + "s";
            }
            else
            {
                worldRankInfo.timeText.text = worldRankInfo.worldLabel + ": No record";
            }
        }
    }

    // Goi Firebase cho tung world, cap nhat lai UI cua CHINH BAN neu Firebase co thoi gian tot hon local
    private void FetchRanksFromFirebase()
    {
        string userId = FirebaseAuthController.CurrentUserId;

        foreach (var worldRankInfo in worldRankInfos)
        {
            WorldRankInfo info = worldRankInfo; // capture bien local, tranh loi closure trong vong lap
            string path = "leaderboards/" + info.sceneName + "/" + userId;

            FirebaseController.instance.dbController.GetData(path, (success, jsonResponse) =>
            {
                if (!success || string.IsNullOrEmpty(jsonResponse) || jsonResponse == "null")
                {
                    return;
                }

                FirebaseRankData data = JsonUtility.FromJson<FirebaseRankData>(jsonResponse);

                float localTime = RankController.HasRecord(info.sceneName)
                    ? RankController.GetBestTime(info.sceneName)
                    : float.MaxValue;

                if (data.time < localTime)
                {
                    string playerName = string.IsNullOrEmpty(data.displayName) ? "Player" : data.displayName;
                    info.timeText.text = info.worldLabel + ": " + playerName + " - " + data.time.ToString("F2") + "s";

                    PlayerPrefs.SetFloat("BestTimeOf" + info.sceneName + "_" + userId, data.time);
                    PlayerPrefs.Save();
                }
            });
        }
    }

    // Goi Firebase de lay TOAN BO nguoi choi cua tung world, tim nguoi co thoi gian tot nhat (global best)
    private void FetchGlobalBestForAllWorlds()
    {
        foreach (var worldRankInfo in worldRankInfos)
        {
            WorldRankInfo info = worldRankInfo;

            if (info.globalBestText == null) continue; // world nao khong gan globalBestText thi bo qua

            string path = "leaderboards/" + info.sceneName;

            FirebaseController.instance.dbController.GetData(path, (success, jsonResponse) =>
            {
                if (!success || string.IsNullOrEmpty(jsonResponse) || jsonResponse == "null")
                {
                    info.globalBestText.text = "Global best: No record";
                    return;
                }

                List<RankEntry> entries = ParseLeaderboardJson(jsonResponse);
                if (entries.Count == 0)
                {
                    info.globalBestText.text = "Global best: No record";
                    return;
                }

                RankEntry best = entries.OrderBy(e => e.time).First();
                string bestName = string.IsNullOrEmpty(best.displayName) ? "Player" : best.displayName;
                info.globalBestText.text = "Global best: " + bestName + " - " + best.time.ToString("F2") + "s";
            });
        }
    }

    // Parse thu cong JSON dang { "uid1": {"time":..,"displayName":".."}, "uid2": {...} }
    // vi JsonUtility khong ho tro parse dictionary voi key dong
    private List<RankEntry> ParseLeaderboardJson(string json)
    {
        List<RankEntry> result = new();

        json = json.Trim();
        if (json.StartsWith("{")) json = json[1..];
        if (json.EndsWith("}")) json = json[..^1];

        int index = 0;
        while (index < json.Length)
        {
            int keyStart = json.IndexOf('"', index);
            if (keyStart == -1) break;
            int keyEnd = json.IndexOf('"', keyStart + 1);
            if (keyEnd == -1) break;
            string userId = json.Substring(keyStart + 1, keyEnd - keyStart - 1);

            int objStart = json.IndexOf('{', keyEnd);
            if (objStart == -1) break;

            int depth = 1;
            int objEnd = objStart + 1;
            while (depth > 0 && objEnd < json.Length)
            {
                if (json[objEnd] == '{') depth++;
                else if (json[objEnd] == '}') depth--;
                objEnd++;
            }

            string objJson = json[objStart..objEnd];

            RankEntry entry = JsonUtility.FromJson<RankEntry>(objJson);
            entry.userId = userId;
            result.Add(entry);

            index = objEnd;
        }

        return result;
    }

    // Lay ten nguoi choi hien tai: uu tien displayName tu Firebase (neu da login), fallback "Player" neu chua co
    private string GetCurrentPlayerName()
    {
        if (!string.IsNullOrEmpty(FirebaseAuthController.CurrentDisplayName))
        {
            return FirebaseAuthController.CurrentDisplayName;
        }
        return "Player";
    }
}