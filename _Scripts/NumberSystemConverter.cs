using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
// using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NumberSystemConverter : MonoBehaviour
{
    [SerializeField] private Text integerToBinaryText;
    [SerializeField] private Text integerToHexText;
    [SerializeField] private Text errorOutputText;

    [SerializeField] private Text integerToNNumberSystemInputField;
    [SerializeField] private Text systemNumberBaseInputField;
    [SerializeField] private Text integerToNBaseSystemOutText;

    private IntegerToNBaseNumberConverter _numberConverter;
    
    private void OnEnable()
    {
        _numberConverter = new IntegerToNBaseNumberConverter();
        // comprehensive null-checks for the presence of text and input fields
    }

    public void ConvertThis(string input)
    {
        int integer;

        // in case the try clause fails, this ensures that the representation of the count does not get stuck
        integerToBinaryText.text = "-";
        integerToHexText.text = "-";

        if (int.TryParse(input, out integer))
        {
            integerToBinaryText.text = integer != 0 ? _numberConverter.IntegerToBinary(integer) : "-";
            integerToHexText.text = integer != 0 ? _numberConverter.IntegerToHex(integer) : "-";
        }
        else
        {
            errorOutputText.text += "\nThis isn't at proper number.";
        }
    }

    public void ConvertIntegerToNBaseNumberSystem()
    {
        int numberToConvert = 0;
        int numberSystemBaseN = 10;

        if (int.TryParse(integerToNNumberSystemInputField.text, out numberToConvert)
            && int.TryParse(systemNumberBaseInputField.text, out numberSystemBaseN)
            && numberSystemBaseN > 1)
        {
            integerToNBaseSystemOutText.text =_numberConverter.IntegerToGenericBase(numberToConvert, numberSystemBaseN);
        }
        else
        {
            integerToNBaseSystemOutText.text = "-";
        }
    }

}