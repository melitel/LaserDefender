using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTxt;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start()
    {
        if (scoreKeeper != null)
        {
            scoreTxt.text = "You scored:\n" + scoreKeeper.GetScore().ToString();
        }
        else 
        {
            scoreTxt.text = "0";
        }
    }
}
