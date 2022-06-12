using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    private int round;
    public int currEnemiesAlive;
    private int enemiesRound;

    public float stateTime;
    public float stateTimeSpeed;

    //Transition variables
    private float transitionTime;
    public float transitionTimeSpeed;

    [SerializeField]
    private EnemySpawner[] spawns;

    void Start()
    {
        round = 0;

        currStateTime = stateTime;
        transitionTime = 1.0f;
        currEnemiesAlive = 0;
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
            currStateTime = stateTime;
            state = GameState.NIGHT;
            lastState = GameState.NIGHT;

            round++;
            enemiesRound = round * 2 + 5;
            foreach (EnemySpawner spawn in spawns)
            {
                int n = enemiesRound / spawns.Length;
                spawn.Spawn(state, n);
                currEnemiesAlive += n;
            }
        }
        else if (transitionTime <= 0)
        {
            currStateTime = stateTime;
            state = GameState.DAY;
            lastState = GameState.DAY;

            round++;
            enemiesRound = round * 2 + 5;
            foreach (EnemySpawner spawn in spawns)
            {
                int n = enemiesRound / spawns.Length;
                spawn.Spawn(state, n);
                currEnemiesAlive += n;
            }
        }
    }

    private void SetSkyboxTime(float value)
    {
        RenderSettings.skybox.SetFloat("_CubemapTransition", value);
    }
}
