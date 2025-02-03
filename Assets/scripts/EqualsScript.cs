using System;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework;
using UnityEngine;

public class EqualsScript : MonoBehaviour{
    string leftStringResult;
    string rightStringResult;
    int? leftIntResult;
    int? rightIntResult;
    CharacterScript ThisCharacterScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        ThisCharacterScript = this.GetComponent<CharacterScript>();
    }

    // Update is called once per frame
    void LateUpdate() {
        int? leftIntResult = null;
        int? rightIntResult = null;
        if (Input.GetMouseButtonUp(0)) {
            CalculateCalculations();
        }
    }
    int numberOfSymbols;
    List<int> symbolPositions;
    string temporaryString;
    int CalculateResultingNumber(string input) {
        numberOfSymbols = 0;
        symbolPositions = new();
        for (int i = 0; i < input.Length; i++) {
            try {
                Convert.ToInt16(input[i]);
            }
            catch {
                symbolPositions.Add(i);
            }
        }
        if (numberOfSymbols > 0) {
            return Convert.ToInt32(new DataTable().Compute(input, null).ToString());
        }
        else {
            return Convert.ToInt32(input);
        }
    }
    void CalculateCalculations() {
        if (GetComponent<CharacterScript>().LeftThing != null) {
            leftStringResult = OperationsHub.GetEntireNumberFromLeftToEquals(ThisCharacterScript.LeftThing);
            leftIntResult = CalculateResultingNumber(leftStringResult);
        }
        if (GetComponent<CharacterScript>().RightThing != null) {
            rightStringResult = OperationsHub.GetEntireNumberFromRightToEquals(ThisCharacterScript.RightThing);
            rightIntResult = CalculateResultingNumber(rightStringResult);
        }
        if (leftIntResult == rightIntResult && leftIntResult != null && rightIntResult != null) {

        }
        else if (leftIntResult != null) {

        }
    }
}
