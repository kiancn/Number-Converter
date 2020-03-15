using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class will track when content is changed,
/// and assure that content is displayed for a limited time only.
/// It will delete text one letter a time, at a specifiable interval.
/// After removing all text, it will write a supplied placeholder "".
/// </summary>
[RequireComponent(typeof(Text))]
public class TextLifeLimiter : MonoBehaviour
{
    [SerializeField] private Text text; // text object displaying a string

    [SerializeField] private float contentLifeTime = 12f; // desired lifetime of messages from 
    // an update to erasure of text-object string content.

    [SerializeField] private string placeholderTextContent;

    [SerializeField, Range(0.05f,0.4f)] private float signDeletionIntervalInSeconds = 0.32f;
    
    [SerializeField] float currentContentLifeTime; // used to measure if time is up for content
    private string mostRecentTextContent = String.Empty; // used to compare with actual content to determine changes.

    
  [SerializeField]  private bool deletingContent;

    private void Awake()
    {
        text = GetComponent<Text>();
        
        if (text == null)
        {
            Debug.LogError("Text element missing, critical error.");
        }
        else
        {
            deletingContent = false;
        }
    }


    private void FixedUpdate()
    {
        // a result of not-0 means that the there has been a string change since last update
        int stringsAreDifferent = String.CompareOrdinal(text.text, mostRecentTextContent);
        // if new text.text value has changed OR the value is that of the placeholder text,
        // reset timer.
        if (stringsAreDifferent != 0 ||
            text.text.Equals(placeholderTextContent))
        {
            currentContentLifeTime = 0;
        }
        // react to string difference
        else { currentContentLifeTime += Time.deltaTime; }

        // if time is up for content
        if (currentContentLifeTime > contentLifeTime && !deletingContent)
        {
            StartCoroutine(DeleteContentIncrementally());
        }

        if (currentContentLifeTime < contentLifeTime && deletingContent)
        {
            StopCoroutine(DeleteContentIncrementally());
            deletingContent = false;
        }

        // updating most to recent string content - AFTER modification has been made
        mostRecentTextContent = text.text;
    }

    private IEnumerator DeleteContentIncrementally()
    {
        deletingContent = true;
        
        Debug.Log("Started deleting content.");
        
        while (text.text != "")
        {
           text.text = text.text.Remove(text.text.Length - 1);
            yield return new WaitForSecondsRealtime(signDeletionIntervalInSeconds);
        }

        text.text = placeholderTextContent;

        deletingContent = false;
    }
}