using UnityEngine;

public class EqualsScript : MonoBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        
    }

    // Update is called once per frame
    void LateUpdate(){
        if (Input.GetMouseButtonUp(0)) {
            CalculateCalculations();
        }
    }
    void CalculateCalculations() {
        if (GetComponent<CharacterScript>().LeftThing != null && GetComponent<CharacterScript>().RightThing != null) {
            //if (OperationsHub.GetEntireNumberFromLeft(GetComponent<CharacterScript>().LeftThing) = )
        }
        else if (GetComponent<CharacterScript>().LeftThing != null) {

        }
        else if (GetComponent<CharacterScript>().LeftThing != null) {

        }

    }
}
