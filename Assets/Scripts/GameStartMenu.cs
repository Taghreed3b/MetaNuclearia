using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject about;
    public GameObject PlayerName;
    public GameObject InfoGame;

    [Header("Main Menu Buttons")]
    public Button startButton;
    public Button optionButton;
    public Button aboutButton;
    public Button quitButton;
    public Button skipButton;
    public Button nextButton;

    [Header("Sound Info")]
    public AudioSource Welcome;
    public AudioSource WhatIDo;
    public AudioSource Info1;
    public AudioSource Info2;





    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        startButton.onClick.AddListener(StartGame);
        skipButton.onClick.AddListener(SkipButton);
        nextButton.onClick.AddListener(NextButton);
        optionButton.onClick.AddListener(EnableOption);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitGame);

        // إخفاء زر Skip في البداية
        skipButton.gameObject.SetActive(false);

        // بدء Coroutine لإظهار زر Skip بعد 15 ثانية
        StartCoroutine(ShowSkipButtonAfterDelay(15f));

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        InfoGame.SetActive(true);
        PlayerName.SetActive(false);
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
        InfoGame.SetActive(true);


        // بدء تشغيل الأصوات بالتسلسل
        StartCoroutine(PlaySoundsSequentially());

    }


    public void SkipButton()
    {
        InfoGame.SetActive(false);
        PlayerName.SetActive(true);
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
        Welcome.Play();
    }


    IEnumerator PlaySoundsSequentially()
    {
        // تشغيل الصوت الترحيبي وانتظار انتهائه
        Welcome.Play();
        yield return new WaitForSeconds(Welcome.clip.length);

        // تشغيل الصوت الأول وانتظار انتهائه
        WhatIDo.Play();
        yield return new WaitForSeconds(WhatIDo.clip.length);

        // تشغيل الصوت الثاني وانتظار انتهائه
        Info1.Play();
        yield return new WaitForSeconds(Info1.clip.length);

        // تشغيل الصوت الثالث
        Info2.Play();
        yield return new WaitForSeconds(Info2.clip.length);
    }


    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);


    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
        about.SetActive(false);
        PlayerName.SetActive(false);

    }
    public void EnableOption()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        about.SetActive(false);
        PlayerName.SetActive(false);

    }
    public void EnableAbout()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(true);
        PlayerName.SetActive(false);


    }

    public void NextButton()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    // Coroutine لإظهار زر Skip بعد تأخير
    IEnumerator ShowSkipButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        skipButton.gameObject.SetActive(true);
    }

}

