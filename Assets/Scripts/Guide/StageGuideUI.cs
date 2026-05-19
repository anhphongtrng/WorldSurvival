using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageGuideUI : MonoBehaviour
{
    [SerializeField] private List<GuidePage> pages;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image guideImage;

    private int currentPage;

    private void Start()
    {
        ShowPage(0);
        GamePauseController.instance.SetGuidePause(true);
    }

    public void NextPage()
    {
        currentPage++;

        if (currentPage >= pages.Count)
        {
            CloseGuide();
            return;
        }

        ShowPage(currentPage);
    }

    public void PreviousPage()
    {
        currentPage--;

        if (currentPage < 0)
            currentPage = 0;

        ShowPage(currentPage);
    }

    void ShowPage(int index)
    {
        descriptionText.text = pages[index].description;
        guideImage.sprite = pages[index].image;
    }

    public void CloseGuide()
    {
        gameObject.SetActive(false);
        GamePauseController.instance.SetGuidePause(false);
    }
}