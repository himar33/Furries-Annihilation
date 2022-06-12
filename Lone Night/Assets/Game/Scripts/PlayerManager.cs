using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float currHP;
    [SerializeField]
    private float recoveryMult;
    [SerializeField]
    private float hpToVign;

    [SerializeField]
    private Vignette vign;
    [SerializeField]
    private PostProcessVolume volume;

    private void Start()
    {
        volume = FindObjectOfType<PostProcessVolume>();
        volume.profile.TryGetSettings(out vign);

        currHP = maxHP;
        hpToVign = 1.0f / maxHP;
    }

    private void Update()
    {
        if (currHP <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
        if (currHP < maxHP)
        {
            currHP += recoveryMult * Time.deltaTime;
            vign.intensity.value -= (recoveryMult * hpToVign * Time.deltaTime) / 2;
        }

    }

    public void Hitted(float dmg)
    {
        //if (currHP > dmg)
        currHP -= dmg;
        if (vign.intensity.value < (0.5f - (dmg * hpToVign) / 2)) vign.intensity.value += (dmg * hpToVign) / 2;
    }
}
