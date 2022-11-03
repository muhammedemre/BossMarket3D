using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeAdsWindowTimerOfficer : MonoBehaviour
{
    public UpgradeAdsWindowActor upgradeAdsWindowActor;
    public UpgradeAdsWindowTimerType timerType;
    public int durationForSeconds = 0;
    public TextMeshProUGUI durationText;

    private void OnEnable()
    {
        ChangeDurationText(durationForSeconds);
    }

    public void StartTimer()
    {
        StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        int timer = durationForSeconds;
        while (timer > 0)
        {
            timer--;
            ChangeDurationText(timer);
            yield return new WaitForSeconds(1);
        }

        upgradeAdsWindowActor.ChangeUpgradeAdsWindowState(UpgradeAdsWindowState.Nan);
    }

    private void ChangeDurationText(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        durationText.text = time.ToString(@"mm\:ss");
    }
}

public enum UpgradeAdsWindowTimerType { Speed, Revenue }