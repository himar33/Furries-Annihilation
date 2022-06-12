using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LooseScreenUI : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMP_Text>().text = "ROUND: " + LevelManager.round.ToString();
    }
}
