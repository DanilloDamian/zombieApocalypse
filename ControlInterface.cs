using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControlInterface : MonoBehaviour
{
    private ControlPlayer scriptControlPlayer;
    public Slider SliderPlayerLiFe;
    public GameObject GameOverPanel;
    public TMP_Text SurvivalTimeText;
    public TMP_Text MaximumScoreText;
    public TMP_Text TextNumberOfZombiesKilled;
    public TMP_Text BossTextAppears;
    private float timeScoreSaved;
    private int amountOfZombiesKilled;

    void Start()
    {
        Time.timeScale =1;
        scriptControlPlayer = GameObject.FindWithTag(Tags.Player).GetComponent<ControlPlayer>();
        SliderPlayerLiFe.maxValue = scriptControlPlayer.Status.Life;       
        UpdateSliderPlayerLiFe();        
    }
    
    void Awake(){
        timeScoreSaved = PlayerPrefs.GetFloat("Record");  
    }

    public void UpdateSliderPlayerLiFe()
    {
        SliderPlayerLiFe.value = scriptControlPlayer.Status.Life;
    }
    
    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;

        int minutes = (int)(Time.timeSinceLevelLoad/60);
        int seconds = (int)(Time.timeSinceLevelLoad%60);
        SurvivalTimeText.text = minutes+"m "+seconds+"s!";
        AdjustPunctuationMaximum(minutes,seconds);        
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene1");
    }

    void AdjustPunctuationMaximum(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > timeScoreSaved)
        {
            timeScoreSaved = Time.timeSinceLevelLoad;
            MaximumScoreText.text = "NEW RECORD!!!!!!!\n"+min+"m "+seg+"s!";
            PlayerPrefs.SetFloat("Record", timeScoreSaved);
        }
        if(MaximumScoreText.text == "")
        {
            min = (int)timeScoreSaved/60;
            seg = (int)timeScoreSaved%60;
            MaximumScoreText.text = "Record: "+min+"m "+seg+"s!";
        }
    }

    public void UpdateNumberOfZombiesKilled()
    {
        amountOfZombiesKilled++;
        TextNumberOfZombiesKilled.text = amountOfZombiesKilled.ToString();
    }

    public void AppearCreatedBossText()
    {
        StartCoroutine(DisappearText(1, BossTextAppears));
    }

    IEnumerator DisappearText(float disappearingTime, TMP_Text textToDisappear)
    {
        textToDisappear.gameObject.SetActive(true);
        Color textColor = textToDisappear.color;
        textColor.a =1;
        textToDisappear.color = textColor;
        yield return new WaitForSeconds(3);
        float meter =0;
        while (textToDisappear.color.a > 0)
        {
            meter +=(Time.deltaTime/ disappearingTime);
            textColor.a = Mathf.Lerp(1, 0, meter);
            textToDisappear.color = textColor;
            if(textToDisappear.color.a <= 0)
            {
                textToDisappear.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");        
    }
   
}

