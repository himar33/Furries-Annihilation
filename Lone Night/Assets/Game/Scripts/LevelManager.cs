using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public enum GameState
    {
        DAY,
        NIGHT,
        TRANSTION,
    }
    
    //Day state variables
    public GameState state;
    private GameState lastState;
    [SerializeField]
    private float currStateTime;

    public static int round;
    public int currEnemiesAlive;
    private int enemiesRound;

    public float stateTime;
    public float stateTimeSpeed;

    //Transition variables
    private float transitionTime;
    public float transitionTimeSpeed;

    [SerializeField]
    private EnemySpawner[] spawns;

    public TMP_Text enemiesUI;
    [SerializeField]
    private TMP_Text dayUI;
    private Timer timer;

    void Start()
    {
        timer = gameObject.GetComponent<Timer>();

        round = 0;
        dayUI.text = "ROUND: " + round.ToString();

        currStateTime = stateTime;
        transitionTime = 1.0f;

        currEnemiesAlive = 0;
        enemiesUI.text = "ENEMIES LEFT: " + currEnemiesAlive;

        lastState = GameState.NIGHT;
        state = GameState.TRANSTION;

        SetSkyboxTime(transitionTime);
    }

    void Update()
    {
        switch (state)
        {
            case GameState.DAY:
                UpdateDay();
                break;
            case GameState.NIGHT:
                UpdateNight();
                break;
            case GameState.TRANSTION:
                UpdateTransition();
                break;
            default:
                break;
        }
    }

    private void UpdateTransition()
    {
        if (lastState == GameState.DAY)
        {
            transitionTime += transitionTimeSpeed * Time.deltaTime;
        }
        else if (lastState == GameState.NIGHT)
        {
            transitionTime -= transitionTimeSpeed * Time.deltaTime;
        }

        SetSkyboxTime(transitionTime);

        CheckTimeValue();
    }

    private void UpdateNight()
    {
        if (currEnemiesAlive <= 0)
        {
            state = GameState.TRANSTION;
        }
    }

    private void UpdateDay()
    {
        if (currEnemiesAlive <= 0)
        {
            state = GameState.TRANSTION;
        }
    }

    private void CheckTimeValue()
    {
        if (transitionTime >= 1)
        {
            timer.StartTimer();
            currStateTime = stateTime;
            state = GameState.NIGHT;
            lastState = GameState.NIGHT;

            round++;
            dayUI.text = "ROUND: " + round.ToString();

            enemiesRound = round * 2 + 5;

            foreach (EnemySpawner spawn in spawns)
            {
                int n = enemiesRound / spawns.Length;
                spawn.Spawn(state, n);
                currEnemiesAlive += n;
                enemiesUI.text = "ENEMIES LEFT: " + currEnemiesAlive;
            }
        }
        else if (transitionTime <= 0)
        {
            timer.StartTimer();
            currStateTime = stateTime;
            state = GameState.DAY;
            lastState = GameState.DAY;

            round++;
            dayUI.text = "ROUND: " + round.ToString();

            enemiesRound = round * 2 + 5;

            foreach (EnemySpawner spawn in spawns)
            {
                int n = enemiesRound / spawns.Length;
                spawn.Spawn(state, n);
                currEnemiesAlive += n;
                enemiesUI.text = "ENEMIES LEFT: " + currEnemiesAlive;
            }
        }
    }

    private void SetSkyboxTime(float value)
    {
        RenderSettings.skybox.SetFloat("_CubemapTransition", value);
    }

    public void EndRound()
    {
        state = GameState.TRANSTION;
        GameObject g = GameObject.Find("EnemiesRoot");
        for (var i = g.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(g.transform.GetChild(i).gameObject);
        }
    }
}
