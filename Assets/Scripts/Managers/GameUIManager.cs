using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : UnitySingleton<GameUIManager>
{
    private void Start()
    {
        timeCountDown=timeMax;
            
        YTEventManager.Instance.AddEventListener(EventStrings.GAME_OVER, GameOver);
    }

    private void Update()
    {
        if (Time.timeScale > 0 && timeCountDown > 0)
        {
            timeCountDown -= Time.deltaTime;
        }
    }


    private void OnDestroy()
    {
        YTEventManager.Instance.RemoveEventListener(EventStrings.GAME_OVER, GameOver);
    }

    public GameObject pausePanel;
    public GameObject settingsPanel;

    public void TimePause()
    {
        YTEventManager.Instance.TriggerEvent(EventStrings.TIME_PAUSE);
        pausePanel.SetActive(true);
        Time.timeScale = 0;

    }

    public void TimeContinue()
    {
        YTEventManager.Instance.TriggerEvent(EventStrings.TIME_CONTINUE);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }


    // 倒计时
    public int timeMax=10;
    public RectTransform timeCountDownMask;
    public Text timeCountDownText;
    private float _timeCountDown;

    public float timeCountDown
    {
        get => _timeCountDown;
        set
        {
            _timeCountDown = value;
            timeCountDownText.text = _timeCountDown>0?((int)_timeCountDown+1).ToString():0.ToString();
            //如果改变了倒计时UI大小，这里要跟着改
            timeCountDownMask.sizeDelta= new Vector2(1300*_timeCountDown/timeMax, 30);
            if (_timeCountDown<0f)
            {
                YTEventManager.Instance.TriggerEvent(EventStrings.GAME_OVER);
            }
        }
    }



    //游戏结束
    public GameObject gameOverPanel;
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void GameRestart()
    {
        //重新加载当前场景
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}