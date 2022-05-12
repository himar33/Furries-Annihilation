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
    
    [SerializeField]
    private GameState state;
    [SerializeField]
    private float time;
    [SerializeField]
    private float timeSpeed;

    void Start()
    {
        state = GameState.DAY;
        time = 0.0f;
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
            default:
                break;
        }

        UpdateLighting(time);

        CheckTimeValue();
    }

    private void UpdateNight()
    {
        time -= timeSpeed * Time.deltaTime;
    }

    private void UpdateDay()
    {
        time += timeSpeed * Time.deltaTime;
    }

    private void CheckTimeValue()
    {
        if (time >= 1)
        {
            state = GameState.NIGHT;
        }
        else if (time <= 0)
        {
            state = GameState.DAY;
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.skybox.SetFloat("_CubemapTransition", timePercent);
    }
}
