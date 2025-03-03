using UnityEngine;

public class CheckingZonesForother : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.transform.parent != transform.parent) {
            if (other.transform.parent.transform.position.z > transform.position.z) {
                if (transform.parent.GetComponent<CharacterScript>().rightThing == null) {
                    transform.parent.GetComponent<CharacterScript>().rightThing = other.transform.parent.GetComponent<CharacterScript>();
                }
            }
            else {
                if (transform.parent.GetComponent<CharacterScript>().leftThing == null) {
                    transform.parent.GetComponent<CharacterScript>().leftThing = other.transform.parent.GetComponent<CharacterScript>();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        Debug.Log(transform.parent.GetComponent<CharacterScript>().rightThing);
        if (GetComponentInParent<CharacterScript>().rightThing != null) {
            if (other.transform.parent.gameObject == transform.parent.GetComponent<CharacterScript>().rightThing.gameObject) {
                transform.parent.GetComponent<CharacterScript>().rightThing = null;
            }
        }
        else if (GetComponentInParent<CharacterScript>().leftThing != null) { 
            if (other.transform.parent.gameObject == transform.parent.GetComponent<CharacterScript>().leftThing.gameObject) {
                transform.parent.GetComponent<CharacterScript>().leftThing = null;
            }
        }
    }
}
