using UnityEngine;

public class CheckingZonesForother : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.transform.parent != transform.parent) {
            Vector3 otherRelativePosition = transform.InverseTransformDirection(other.transform.position);
            if (otherRelativePosition.x > 0) {
                if (transform.parent.GetComponent<CharacterScript>().rightCharacter == null) {
                    transform.parent.GetComponent<CharacterScript>().rightCharacter = other.transform.parent.GetComponent<CharacterScript>();
                }
            }
            else {
                if (transform.parent.GetComponent<CharacterScript>().leftCharacter == null) {
                    Debug.Log(other.transform);
                    transform.parent.GetComponent<CharacterScript>().leftCharacter = other.transform.parent.GetComponent<CharacterScript>();
                }
            }
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
