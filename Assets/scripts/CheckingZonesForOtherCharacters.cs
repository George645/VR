using UnityEngine;
using UnityEngine.Rendering;

public class CheckingZonesForother : MonoBehaviour{
    private LayerMask noCollision;
    private void Start() {
        noCollision = 7;
    }
    private void OnTriggerEnter(Collider other) {
        if (gameObject.layer != noCollision) { 
            if (other.transform.parent != transform.parent) {
                Vector3 otherRelativePosition = transform.InverseTransformDirection(other.transform.position);
                if (otherRelativePosition.x > 0) {
                    if (transform.parent.GetComponent<CharacterScript>().rightCharacter == null) {
                        transform.parent.GetComponent<CharacterScript>().rightCharacter = other.transform.parent.GetComponent<CharacterScript>();
                    }
                }
                else {
                    if (transform.parent.GetComponent<CharacterScript>().leftCharacter == null) {
                        transform.parent.GetComponent<CharacterScript>().leftCharacter = other.transform.parent.GetComponent<CharacterScript>();
                    }
                }
            }
        }
    }
    private void Update() {
        if (gameObject.layer == noCollision) {
            transform.parent.GetComponent<CharacterScript>().rightCharacter = null;
            transform.parent.GetComponent<CharacterScript>().leftCharacter = null;
        }
    }
    private void OnTriggerExit(Collider other) {
        //Debug.Log(transform.parent.GetComponent<CharacterScript>().rightCharacter);
        if (GetComponentInParent<CharacterScript>().rightCharacter != null) {
            if (other.transform.parent.gameObject == transform.parent.GetComponent<CharacterScript>().rightCharacter.gameObject) {
                transform.parent.GetComponent<CharacterScript>().rightCharacter = null;
            }
        }
        else if (GetComponentInParent<CharacterScript>().leftCharacter != null) { 
            if (other.transform.parent.gameObject == transform.parent.GetComponent<CharacterScript>().leftCharacter.gameObject) {
                transform.parent.GetComponent<CharacterScript>().leftCharacter = null;
            }
        }
    }
}
