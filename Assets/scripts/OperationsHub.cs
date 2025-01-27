using System;
using UnityEngine;
using Unity.VisualScripting;

[IncludeInSettings(true)]
public class OperationsHub : MonoBehaviour{
    /*public static int Addition(CharacterScript input1, CharacterScript input2) { //the added plus will be the thing to run this function
        int firstInt = GetEntireNumber(input1);
        int secondInt = GetEntireNumber(input2);
        return firstInt + secondInt;
    }*/
    public static int GetEntireNumberFromLeft(GameObject input) {
        GameObject leftThing = (GameObject)Variables.Object(input).Get("leftthing");
        if (leftThing != null && (bool)Variables.Object(leftThing).Get("isnumber")) {
            int returnInt = GetEntireNumberFromLeft(leftThing) + (int)Variables.Object(input).Get("character");
            return returnInt;
        }
        else {
            return (int)Variables.Object(input).Get("character");
        }
    }
    public static int GetEntireNumberFromRight(GameObject input) {
        return 123456;
        /*string returningNumber = Convert.ToString(input.character);
        if (input.rightThing != null && input.rightThing.isNumber) {
            returningNumber += Convert.ToString(GetEntireNumber(input.rightThing));
            return Convert.ToInt32(returningNumber);
        }*/
    }
}
