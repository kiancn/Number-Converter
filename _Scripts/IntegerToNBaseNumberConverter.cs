using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class IntegerToNBaseNumberConverter
{
    private static readonly char[] BASEINDEXES = new[]
     {   '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
         'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
         'K', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
         'V', 'W', 'Z', 'X', 'Y', 'Z'};
    // Method ~ iterates over integer; calculation moves from least to most significant
    // digit. Returns a string representation of value in requested number system base.
    public string IntegerToGenericBase(int integer, int systemBase)
    {
        if (systemBase < 2 || systemBase > 36)
        { return "Base number out of bounds (range: 2-36)"; }
        
        var stringBuilder = new StringBuilder();
        int sign = Math.Sign(integer); // store sign of integer
        integer = Math.Abs(integer); // set integer to absolute value of itself
        
        // each step, divide by number base - this reveals 'moves' calculation 'up 
        for (; integer > 0; integer /= systemBase) // one digit'.
        {
            int tempInteger = integer % systemBase; // remainder is next value
            char c = BASEINDEXES[tempInteger]; // get char from []; value is index 
            stringBuilder.Append(c.ToString()); // add to string
        }   
        // reversing string (: least sig digit was calc'ed first), then adding store sign
        return (sign < 0 ? "-" : "") + ReverseString(stringBuilder.ToString());
    }
    // Method convert an int to binary
    // Note: These next two methods have been retained for posterty; I write them before I wrote the more general one above.
    public string IntegerToBinary(int integer)
    {
        StringBuilder stringBuilder = new StringBuilder();

        int sign = Math.Sign(integer);
        integer = Math.Abs(integer);

        // stop this process when integer 0 or <, for each step, divide by two (effect of right-bitshift 1)
        for (; integer > 0; integer >>= 1)
        {
            int tempResult = integer % 2;
            stringBuilder.Append(tempResult);
        }

        if (sign < 0) { stringBuilder.Append("-"); }

        return ReverseString(stringBuilder.ToString().Trim());
    }

    public string IntegerToHex(int integer)
    {
        StringBuilder stringBuilder = new StringBuilder();
        int sign = Math.Sign(integer);
        integer = Math.Abs(integer);

        for (; integer > 0; integer >>= 4) // divide by 16
        {
            int tempInteger = integer % 16;
            char c = BASEINDEXES[tempInteger];
            stringBuilder.Append(c.ToString());
            Debug.Log(stringBuilder.ToString());
        }

        return ReverseString(stringBuilder.ToString());
    }
    
    /// Method reverses an input string
    private string ReverseString(string input)
    {
        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new String(chars);
    }
}
