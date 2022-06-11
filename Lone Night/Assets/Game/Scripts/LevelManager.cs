using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    enum GameState
    {
        DAY,
        NIGHT,
        TRANSTION,
    }
    
    //Day state variables
    [SerializeField]
    private GameState state;
    private GameState lastState;
    [SerializeField]
    private float currStateTime;

    public float stateTime;
    public float stateTimeSpeed;

    //Transition variables
    private float transitionTime;
    public float transitionTimeSpeed;

    [SerializeField]
    private EnemySpawner[] spawns;

    void Start()
    {
        state = GameState.DAY;
        currStateTime = stateTime;
        transitionTime = 0.0f;
        SetSkyboxTime(transitionTime);

        foreach (EnemySpawner spawn in spawns)
        {
            spawn.Spawn(EnemySpawner.EnemyType.EASY, 2);
        }
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
        UpdateStateTime();
        //Night logic
    }

    private void UpdateDay()
    {
        UpdateStateTime();
        //Day logic
    }

    private void UpdateStateTime()
    {
        currStateTime -= stateTimeSpeed * Time.deltaTime;
        if (currStateTime <= 0)
        {
            lastState = state;
            state = GameState.TRANSTION;
        }
    }

    private void CheckTimeValue()
    {
        if (transitionTime >= 1)
        {
            currStateTime = stateTime;
            state = GameState.NIGHT;
        }
        else if (transitionTime <= 0)
        {
            currStateTime = stateTime;
            state = GameState.DAY;
        }
    }

    private void SetSkyboxTime(float value)
    {
        RenderSettings.skybox.SetFloat("_CubemapTransition", value);
    }
}
