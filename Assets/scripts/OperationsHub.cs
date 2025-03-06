using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEditor.ShaderGraph.Legacy;

public class OperationsHub : MonoBehaviour {
    public static GameObject number0;
    public static GameObject number1;
    public static GameObject number2;
    public static GameObject number3;
    public static GameObject number4;
    public static GameObject number5;
    public static GameObject number6;
    public static GameObject number7;
    public static GameObject number8;
    public static GameObject number9;
    private void Start() {
        NumbersContainter canvas = FindAnyObjectByType<Canvas>().gameObject.GetComponent<NumbersContainter>();
        number0 = canvas.number0;
        number1 = canvas.number1;
        number2 = canvas.number2;
        number3 = canvas.number3;
        number4 = canvas.number4;
        number5 = canvas.number5;
        number6 = canvas.number6;
        number7 = canvas.number7;
        number8 = canvas.number8;
        number9 = canvas.number9;
        Destroy(canvas);
    }

    /*public static int Addition(CharacterScript input1, CharacterScript input2) { //the added plus will be the thing to run this function
       int firstInt = GetEntireNumber(input1);
       int secondInt = GetEntireNumber(input2);
       return firstInt + secondInt;
   }*/
    public static int GetEntireNumberFromLeftNoSymbols(CharacterScript input) {
        if (input.LeftCharacter != null){
            try {
                Convert.ToInt32(input.character);
                int returnInt = GetEntireNumberFromLeftNoSymbols(input.LeftCharacter) + Convert.ToInt32(input.character);
                return returnInt;
            }
            catch {
                return Convert.ToInt32(input.character);
            }
        }
        else {
            return Convert.ToInt32(input.character);
        }
    }
    public static string GetEntireNumberFromLeftToEquals(CharacterScript input) {
        if (input.LeftCharacter.character == "=") {
            return input.character;
        }
        if (input.LeftCharacter != null) {
            return GetEntireNumberFromLeftToEquals(input.LeftCharacter) + input.character;
        }
        else {
            return input.character;
        }
    }
    public static int GetEntireNumberFromRightNoSymbols(CharacterScript input) {
        if (input.rightCharacter != null) {
            try {
                Convert.ToInt32(input.character);
                int returnInt = GetEntireNumberFromRightNoSymbols(input.rightCharacter) + Convert.ToInt32(input.character);
                return returnInt;
            }
            catch {
                return Convert.ToInt32(input.character);
            }
        }
        else {
            return Convert.ToInt32(input.character);
        }
    }
    public static string GetEntireNumberFromRightToEquals(CharacterScript input) {
        if (input.rightCharacter != null) {
            return GetEntireNumberFromRightToEquals(input.rightCharacter) + input.character;
        }
        else {
            return input.character;
        }
    }
    public static GameObject NumberToPrefab(int number) {
        switch (number) {
            case 0:
                return number0;
            case 1:
                return number1;
            case 2:
                return number2;
            case 3:
                return number3;
            case 4:
                return number4;
            case 5:
                return number5;
            case 6:
                return number6;
            case 7:
                return number7;
            case 8:
                return number8;
            case 9:
                return number9;
            default:
                return null;
        }
    }
}
