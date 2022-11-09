using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BoostAdsWindowTimerOfficer : MonoBehaviour
{
    public BoostAdsWindowActor boostAdsWindowActor;
    public BoostAdsWindowTimerType timerType;
    public int durationForSeconds = 0;
    public Image progressImage;
    public TextMeshProUGUI durationText;

    private void OnEnable()
    {
        ChangeDurationText(durationForSeconds);
    }

    public void StartTimer()
    {
        StopCoroutine(LevelManager.instance.levelPowerUpOfficer.lastDeactiveAdsBoostCoroutine);
        StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        int timer = durationForSeconds;
        DOTween.To(value => progressImage.fillAmount = value, 1f, 0f, durationForSeconds);
        while (timer > 0)
        {
            timer--;
            ChangeDurationText(timer);
            yield return new WaitForSeconds(1);
        }

        // boostAdsWindowActor.ChangeBoostAdsWindowState(BoostAdsWindowState.Nan);
        boostAdsWindowActor.DeactiveState(true, null);
    }

    private void ChangeDurationText(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        durationText.text = time.ToString(@"mm\:ss");
    }
}

public enum BoostAdsWindowTimerType { Speed, Revenue, Cashier }