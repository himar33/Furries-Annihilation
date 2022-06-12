using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private int minutes;

    [SerializeField]
    private int seconds;

    private int m, s;

    [SerializeField]
    private TMP_Text timerText;

    private LevelManager levelManager;

    void Start()
    {
        levelManager = gameObject.GetComponent<LevelManager>();
    }

    public void StartTimer()
    {
        m = minutes;
        s = seconds;
        WriteTimer(m, s);
        Invoke("UpdateTimer", 1f);
    }

    public void UpdateTimer()
    {
        s--;
        if (s < 0)
        {
            if (m == 0)
            {
                levelManager.EndRound();
                return;
            }
            else
            {
                m--;
                s = 59;
            }
        }

        WriteTimer(m, s);
        Invoke("UpdateTimer", 1f);
    }

    private void WriteTimer(int m, int s)
    {
        if (s < 10)
        {
            timerText.text = m.ToString() + ":0" + s.ToString();
        }
        else
        {
            timerText.text = m.ToString() + ":" + s.ToString();
        }
    }

}
