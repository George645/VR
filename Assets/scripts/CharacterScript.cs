using Unity.VisualScripting;
using UnityEngine;

public class CharacterScript : MonoBehaviour{
    CharacterScript rightThing;
    CharacterScript tempRightThing;
    CharacterScript leftThing;
    CharacterScript tempLeftThing;
    LayerMask generalNumberLayer;

    #region Serialized
    [Header("Not entirely necessary to fill in, or at least, shouldn't be")]
    [SerializeField]
    public bool isNumber = true;
    [SerializeField]
    public string character;
    #endregion
    private void Start() {
        generalNumberLayer = 6;
        try {
            character = System.Convert.ToString(System.Convert.ToInt16(this.name.Remove(1)));
            isNumber = true;
        }
        catch {
            character = this.name.Remove(1);
            isNumber = false;
        }
    }
    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            CheckLeft();
            CheckRight();
        }
    }
    private void LateUpdate() {
        if (!Input.GetMouseButton(0)) {
            RealignCharacters();
        }
    }

    #region Destroy those on the right
    public void DestroyThoseOnTheRight() {
        if (rightThing != null) {
            rightThing.DestroyThoseOnTheRight();
        }
        Destroy(this.gameObject);
    }
    #endregion

    #region Realign sides
    private void RealignCharacters() {
        if (rightThing != null & leftThing != null) {
            transform.position = (rightThing.transform.position + leftThing.transform.position) / 2;
        }
        else if (rightThing != null) {
            transform.position = new Vector3(rightThing.transform.position.x, rightThing.transform.position.y, rightThing.transform.position.z + 5);
        }
        else if (leftThing != null) {
            transform.position = new Vector3(rightThing.transform.position.x, rightThing.transform.position.y, rightThing.transform.position.z - 5);
        }
    }
    #endregion

    #region Scanning sides
    private CharacterScript CheckLeft() {
        tempLeftThing = null;
        GetComponent<BoxCollider>().enabled = false;
        for (float i = -0.5f; i <= 0.25; i += 0.3f) {
            for (float j = -0.5f; i <= 0.25; i += 0.3f) {
                Ray checkingLeft = new Ray(transform.position, new Vector3(j, i, -1)); //not sure if it is meant to be -1 or 1
                Physics.Raycast(checkingLeft, out RaycastHit hitInfo, 5, generalNumberLayer);
                if (hitInfo.collider != null) {
                    if (leftThing = hitInfo.collider.gameObject.GetComponent<CharacterScript>()) {
                        return leftThing;
                    }
                    else {
                        if (tempLeftThing == null) {
                            tempLeftThing = hitInfo.collider.gameObject.GetComponent<CharacterScript>();
                        }
                    }
                }
            }
        }
        GetComponent<BoxCollider>().enabled = true;
        return tempLeftThing;
    }
    private CharacterScript CheckRight() {
        tempRightThing = null;
        GetComponent<BoxCollider>().enabled = false;
        for (float i = -0.5f; i <= 0.25; i += 0.3f) {
            for (float j = -0.5f; i <= 0.25; i += 0.3f) {
                Ray checkingLeft = new Ray(transform.position, new Vector3(j, i, 1)); //not sure if it is meant to be -1 or 1
                Physics.Raycast(checkingLeft, out RaycastHit hitInfo, 5, generalNumberLayer);
                if (hitInfo.collider != null) {
                    if (rightThing = hitInfo.collider.gameObject.GetComponent<CharacterScript>()) {
                        return rightThing;
                    }
                    else {
                        if (tempRightThing == null) {
                            tempRightThing = hitInfo.collider.gameObject.GetComponent<CharacterScript>();
                        }
                    }
                }
            }
        }
        GetComponent<BoxCollider>().enabled = true;
        return tempRightThing;

    }
    #endregion
}
