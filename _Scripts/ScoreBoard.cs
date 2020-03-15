using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Text nBaseScoreText;
    [SerializeField] private Text integerScoreText;
    [SerializeField, Range(2,36)] private int scoreNumberSystemBase = 10;

    [SerializeField] private int currentScore;
    [SerializeField] private int newScore;

    [SerializeField] private Text errorText; // user for user end error output

    [SerializeField] private bool pointsUpdating = false;
    [SerializeField] private float pointIncreaseSpeed = 1f;

    private float pointsMultiplier = 1;

    [SerializeField] private IntegerToNBaseNumberConverter _converter;
    
    private void Awake()
    {
        _converter = new IntegerToNBaseNumberConverter();
    }

    public void addToScore(int points)
    {
        pointsMultiplier += 0.1f;
        
        newScore += (int)(points*pointsMultiplier);

        if (pointsUpdating) { return; }

        StartCoroutine(UpdatePoints());
    }

    private IEnumerator UpdatePoints()
    {
        pointsUpdating = true;

        float partOfTheWay = 0;

        int fontStartSize = nBaseScoreText.fontSize;

        int textFontMaxSize = 300;

        while (partOfTheWay <= 0.95f)
        {
            partOfTheWay += Time.deltaTime * pointIncreaseSpeed; 

            currentScore = (int)Mathf.Lerp(currentScore, newScore, partOfTheWay);

            integerScoreText.text = currentScore.ToString();
            
            nBaseScoreText.fontSize += 1;
            
            nBaseScoreText.color = Color.Lerp(Color.black, Color.red, partOfTheWay);
            
            nBaseScoreText.text = _converter.IntegerToGenericBase(currentScore,scoreNumberSystemBase);
            
            yield return new WaitForFixedUpdate();
        }


        nBaseScoreText.fontSize = fontStartSize;

        nBaseScoreText.color = Color.black;
        
        currentScore = newScore;

        pointsUpdating = false;
    }
    
    
    public void SetNumberBaseWithString(string input)
    {
        int newBase;

        if (int.TryParse(input, out newBase))
        {
            if (newBase > 1 && newBase < 37) { scoreNumberSystemBase = newBase; }
            else
            {
                errorText.text += "\n Number" + newBase.ToString() + " base out of bounds. Limits: 2 - 36." +
                                  "\n Keeping " + scoreNumberSystemBase.ToString() + " as base number.";
            }
        }
        else
        {
            errorText.text += "\n That's not an integer, now is it?";
        }
    }
}