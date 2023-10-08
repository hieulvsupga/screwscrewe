using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public int timeInSeconds;
    private bool isPaused;
    private float timeCounter;
    private int totalTime;
    private float sleepTime;
    private Color timeInitialColor;
    public bool runOnStart;
    public static Timer instance;
    // private void Start()
    // {
    //     if (this.runOnStart)
    //     {
    //         this.Run();
    //     }
    // }

    private void Awake()
    {
        if (Timer.instance == null)
        {
            Timer.instance = this;
        }
        this.timeInitialColor = this.uiText.color;
        this.Pause();
    }

    public void Run()
    {
        // if (LevelsManager.instance.GetCurrentLevel() == null)
        // {
        //     return;
        // }
        this.isPaused = false;
        this.timeCounter = 0f;
        this.sleepTime = 1f;
        this.totalTime = Controller.Instance.rootlevel.totalTime;
        //LevelsManager.instance.GetCurrentLevel().starsTimePeriod * 4;
        this.timeInSeconds = this.totalTime;
        base.InvokeRepeating("Wait", 0f, this.sleepTime);
    }
    public void Stop()
    {
        base.CancelInvoke();
    }

    public void Reset()
    {
        this.Stop();
        this.Run();
    }

    public void Pause()
    {
        this.isPaused = true;
    }

    public void Resume()
    {
        this.isPaused = false;
    }

    private void Wait()
    {
        if (!this.isPaused)
        {
            this.timeCounter += this.sleepTime;
            this.timeInSeconds = this.totalTime - (int)this.timeCounter;
            this.ApplyTime();         
        }
        if(this.timeInSeconds == 0){
            Stop();
            UIEvents.Instance.ShowDefaultUI();
        }
    }

    private void ApplyTime()
    {
        if (this.uiText == null)
        {
            return;
        }
        int number = this.timeInSeconds / 60;
        int number2 = this.timeInSeconds % 60;
        if (this.timeInSeconds < 11)
        {
            this.uiText.color = Color.red;
        }
        else
        {
            this.uiText.color = this.timeInitialColor;
        }
        this.uiText.text = Timer.GetNumberWithZeroFormat(number) + ":" + Timer.GetNumberWithZeroFormat(number2);
    }

    public void IncreaseTime(float factor)
	{
		this.timeCounter -= factor;
		//this.timeCounter = Mathf.Clamp(this.timeCounter, 0f, float.PositiveInfinity);
	}

    public static string GetNumberWithZeroFormat(int number)
    {
        string text = string.Empty;
        if (number < 10)
        {
            text += "0";
        }
        return text + number;
    }
    public bool IsPaused()
    {
        return this.isPaused;
    }

    private void OnDestroy()
    {
        base.CancelInvoke();
    }
}
