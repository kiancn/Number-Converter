using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsToScoreBoardOnClick : MonoBehaviour
{
    [SerializeField] private ScoreBoard scoreBoard;

    [SerializeField] private int pointsOnClick;

    private void OnEnable() { scoreBoard = GetComponentInParent<ScoreBoard>(); }

    private void OnMouseDown()
    {
        if (scoreBoard != null)
        {
            scoreBoard.addToScore(pointsOnClick);
        }
        else
        {
            Debug.Log("Scoreboard not found; you probably forgot to attach a Scoreboard" +
                      "to parent GameObject.");
        }
    }
}