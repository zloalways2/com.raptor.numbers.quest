using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sound;
    public AudioSource victorySound;


    public GameObject OptionPanel;
    public GameObject MenuPanel;
    public GameObject WinPanel;
    public GameObject levelPanel;

    public Button musicPlusButton;
    public Button musicMinusButton;
    public Image[] musicBoxes;


    public Button soundPlusButton;
    public Button soundMinusButton;
    public Image[] soundBoxes;


    public Sprite activeSprite;
    public Sprite inactiveSprite;

    public Button acceptChanges;
    public Button cancelChanges;

    private int currentMusicLevel = 5;
    private int maxMusicLevel = 6;

    private int currentSoundLevel = 5;
    private int maxSoundLevel = 6;


    private void Start()
    {

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", currentMusicLevel);
        float soundVolume = PlayerPrefs.GetFloat("SoundVolume", currentSoundLevel);

        musicPlusButton.onClick.AddListener(IncreaseMusic);
        musicMinusButton.onClick.AddListener(DecreaseMusic);

        soundPlusButton.onClick.AddListener(IncreaseSound);
        soundMinusButton.onClick.AddListener(DecreaseSound);

        acceptChanges.onClick.AddListener(AcceptChanges);
        cancelChanges.onClick.AddListener(CancelChanges);
        

        UpdateMusicBoxes();
        UpdateSoundBoxes();


    }

    public void AcceptChanges()
    {
        PlayerPrefs.SetFloat("MusicVolume", currentMusicLevel);
        PlayerPrefs.SetFloat("SoundVolume", currentSoundLevel);

        OptionPanel.SetActive(false);
        UpdateMusicBoxes();
        UpdateSoundBoxes();
    }

    public void CancelChanges()
    {
        currentMusicLevel = (int)PlayerPrefs.GetFloat("MusicVolume", currentMusicLevel);
        currentSoundLevel = (int)PlayerPrefs.GetFloat("SoundVolume", currentSoundLevel);

        // Update the actual audio sources based on the reverted values
        music.volume = (float)currentMusicLevel / maxMusicLevel;
        sound.volume = (float)currentSoundLevel / maxSoundLevel;
        victorySound.volume = (float)currentSoundLevel / maxSoundLevel;

        // Hide the option panel and update the UI boxes to reflect the reverted values
        OptionPanel.SetActive(false);
        UpdateMusicBoxes();
        UpdateSoundBoxes();
    }

    void IncreaseMusic()
    {
        Debug.Log("music + ");
        if (currentMusicLevel < maxMusicLevel)
        {
            currentMusicLevel++;
            music.volume = (float)currentMusicLevel / maxMusicLevel; ;
            UpdateMusicBoxes();

        }
    }

    void DecreaseMusic()
    {
        if (currentMusicLevel > 0)
        {
            currentMusicLevel--;
            music.volume = (float)currentMusicLevel / maxMusicLevel; ;
            UpdateMusicBoxes();
        }
    }

    void UpdateMusicBoxes()
    {
        Debug.Log("boxes changes " + currentMusicLevel);
        for (int i = 0; i < musicBoxes.Length; i++)
        {
            if (i < currentMusicLevel)
            {
                musicBoxes[i].sprite = activeSprite;
            }
            else
            {
                musicBoxes[i].sprite = inactiveSprite;
            }
        }
    }

    void IncreaseSound()
    {
        if (currentSoundLevel < maxSoundLevel)
        {
            currentSoundLevel++;
            sound.volume = (float)currentSoundLevel / maxSoundLevel;
            victorySound.volume = (float)currentSoundLevel / maxSoundLevel;
            UpdateSoundBoxes();
        }
    }

    void DecreaseSound()
    {
        if (currentSoundLevel > 0)
        {
            currentSoundLevel--;
            sound.volume = (float)currentSoundLevel / maxSoundLevel; ;
            victorySound.volume = (float)currentSoundLevel / maxSoundLevel;
            UpdateSoundBoxes();
        }
    }

    void UpdateSoundBoxes()
    {
        for (int i = 0; i < soundBoxes.Length; i++)
        {
            if (i < currentSoundLevel)
            {
                soundBoxes[i].sprite = activeSprite;
            }
            else
            {
                soundBoxes[i].sprite = inactiveSprite;
            }
        }
    }
}
