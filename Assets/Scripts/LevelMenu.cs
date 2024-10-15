using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public TextMeshProUGUI currentLevelText;

    public GameObject gamePanel;
    public GameObject levelPanel;
    public GameObject menuPanel;
    public GameObject loadingPanel;

    public Button[] buttons;


    public NumberSorting numberSortingScript;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        UpdateLevelButtons();
    }

    public void UpdateLevelButtons()
    {
         int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
            int level = i + 1;  // Since levels start from 1, not 0
            buttons[i].onClick.AddListener(() => OpenLevel(level));
        }

    }
    public void OpenLevel(int level)
    {

        currentLevelText.text = "Level " + level.ToString();
        numberSortingScript.SetCurrentLevel(level);
        


        //SceneManager.LoadScene("GameScene");
        gamePanel.SetActive(true);
        levelPanel.SetActive(false);
        menuPanel.SetActive(false);
        loadingPanel.SetActive(false);
    }

}
