using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NumberSorting : MonoBehaviour
{
    public AudioSource soundAudioSource;
    public AudioSource victoryAudioSource;

    public Button[] buttons;
    public Sprite[] numberSprites;

    public GameObject gamePanel;
    public GameObject winPanel;
    public GameObject LevelPanel;
    public GameObject menuPanel;

    public TextMeshProUGUI TimerRecord;

    private int firstSelectedIndex = -1;
    private int lastSelectedIndex = -1;

    private int[] buttonValues;

    public TextMeshProUGUI timerText;  

    private float elapsedTime = 0f;
    private bool isGameRunning = true;

    private int currentLevel = 1;

    public LevelMenu levelMenu;

    private void Start()
    {
        
        InitializeButtons();
    }


    void Update()
    {
        if (isGameRunning)
        {
            elapsedTime += Time.deltaTime;
            timerText.text = FormatTime(elapsedTime);
        }
    }

    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
        //currentLevelText.text = "Level " + level.ToString();  // Update the displayed level number
    }

    void InitializeButtons()
    {
        buttonValues = new int[buttons.Length];
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };
 
        numbers = ShuffleArray(numbers);
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttonValues[i] = numbers[i];
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].GetComponent<Image>().sprite = numberSprites[buttonValues[i] - 1];
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    void OnButtonClick(int index)
    {
        if (firstSelectedIndex == -1)
        {
            firstSelectedIndex = index;  // Store the first button clicked
        }
        else if (lastSelectedIndex == -1)
        {
            soundAudioSource.Play();
            lastSelectedIndex = index; // Store the second button clicked

            // Swap the buttons (images and values)
            SwapButtons(firstSelectedIndex, lastSelectedIndex);

            // Reset the selections
            firstSelectedIndex = -1;
            lastSelectedIndex = -1;
            // Logic to handle button clicks, such as swapping buttons or checking order
            Debug.Log("Button clicked: " + index);

            if (CheckIfSorted())
            {
                victoryAudioSource.Play();
                int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
                Debug.Log("You have sorted the numbers!" + unlockedLevel );

                
                if (currentLevel == unlockedLevel &&  unlockedLevel <= buttons.Length)
                {
                    unlockedLevel += 1;
                    PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
                    PlayerPrefs.Save();
                    Debug.Log("next level" + unlockedLevel);
                    levelMenu.UpdateLevelButtons();
                }
                PlayerPrefs.Save();
                //SceneManager.LoadScene("MenuScene");
                TimerRecord.text = timerText.text;
                winPanel.SetActive(true);
                gamePanel.SetActive(false);
            }
        }
    }

    bool CheckIfSorted()
    {
        for (int i = 0; i < buttonValues.Length - 1; i++)
        {
            if (buttonValues[i] > buttonValues[i + 1])
            {
                return false; // If any pair is out of order, the list is not sorted
            }
        }
        return true; // All numbers are sorted
    }

    void SwapButtons(int firstIndex, int secondIndex)
    {
        // Check for valid indices to prevent index out of range errors
        if (firstIndex >= 0 && firstIndex < buttonValues.Length && secondIndex >= 0 && secondIndex < buttonValues.Length)
        {
            // Swap the images (sprites) between the two buttons
            Sprite tempSprite = buttons[firstIndex].GetComponent<Image>().sprite;
            buttons[firstIndex].GetComponent<Image>().sprite = buttons[secondIndex].GetComponent<Image>().sprite;
            buttons[secondIndex].GetComponent<Image>().sprite = tempSprite;

            // Swap the values (numbers) between the two buttons
            int tempValue = buttonValues[firstIndex];
            buttonValues[firstIndex] = buttonValues[secondIndex];
            buttonValues[secondIndex] = tempValue;
        }
    }


    // Shuffle the array of numbers
    int[] ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
        return array;
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void WinPanelAccept()
    {
        //PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
        elapsedTime = 0f;
        firstSelectedIndex = -1;
        lastSelectedIndex = -1;
        InitializeButtons();

        gamePanel.SetActive(false);
        LevelPanel.SetActive(true);
        winPanel.SetActive(false);
    }

    public void WinPanelDecline()
    {
        //PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
        elapsedTime = 0f;
        firstSelectedIndex = -1;
        lastSelectedIndex = -1;
        InitializeButtons();
        gamePanel.SetActive(false);
        menuPanel.SetActive(true);
        winPanel.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}

