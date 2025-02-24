using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterScript : MonoBehaviour{
    CharacterScript rightThing;
    CharacterScript tempRightThing;
    CharacterScript leftThing;
    CharacterScript tempLeftThing;
    LayerMask generalNumberLayer;
    [SerializeField]
    GameObject? followingDuplicate = null;

    #region StaticNumberVariables
    public static List<GameObject> allTheNumberPrefabs;
    #endregion

    [SerializeField]
    private bool beenSelected = false;
    [SerializeField]
    private GameObject parent = null;

    #region Serialized
    [SerializeField]
    private GameObject thisPrefab;

    [Tooltip("this only needs to be made once, but the more times, the better")]
    [SerializeField]
    public List<GameObject> allTheNumberPrefabsLocal;

    [Tooltip("Not entirely necessary to fill in, or at least, shouldn't be")]
    [SerializeField]
    public bool isNumber = true;
    [Tooltip("Not entirely necessary to fill in, or at least, shouldn't be")]
    [SerializeField]
    public string character;

    #endregion

    #region get things
    public CharacterScript RightThing { get { return rightThing; } }
    public CharacterScript LeftThing { get { return leftThing; } }
    #endregion

    private void Start() {
        generalNumberLayer = 6;
        try {
            if (name.Length > 1) {
                character = System.Convert.ToString(System.Convert.ToInt16(name.Remove(1)));
                isNumber = true;
            }
            else {
                character = System.Convert.ToString(System.Convert.ToInt16(name));
            }
        }
        catch {
            character = name.Remove(1);
            isNumber = false;
        }
        finally {
            character = name;
            isNumber = false;
        }
        try {
            Debug.Log(allTheNumberPrefabsLocal[0]); //THIS IS IMPORTANT -- IF IT IS REMOVED, THERE IS NO CHECK FOR IF THE LIST HAS ANYTHING IN
            allTheNumberPrefabs = allTheNumberPrefabsLocal;
        }
        catch {

        }
        foreach (GameObject number in CharacterScript.allTheNumberPrefabs) {
            if (number.name[0] == name[0]) {
                thisPrefab = number;
                break;
            }
        }
    }
    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            CheckLeft();
            CheckRight();
        }
        if (followingDuplicate != null) {
            UpdateFolloweePosition();
        }
    }
    private void LateUpdate() {
        if (!Input.GetMouseButton(0)) {
            RealignCharacters();
        }
    }

    #region move followee to this position

    private void UpdateFolloweePosition() {
        followingDuplicate.transform.position = transform.position;
        followingDuplicate.transform.rotation = transform.rotation;
    }

    #endregion

    #region selected

    public void Selected() {
        //work on this
        if (thisPrefab == null) { 
            foreach (GameObject number in allTheNumberPrefabs) {
                if (number.name[0] == name[0]) {
                    thisPrefab = number;
                    break;
                }
            }
        }
        followingDuplicate = Instantiate(thisPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        followingDuplicate.GetComponent<CharacterScript>().parent = gameObject;
        followingDuplicate.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
        if (parent != null) {
            parent.GetComponent<CharacterScript>().followingDuplicate = null;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    #endregion

    #region deseleft

    public void DeSelect() {
        if (!followingDuplicate.GetComponent<CharacterScript>().beenSelected) {
            Destroy(followingDuplicate);
        }
        if (parent != null) {
            parent = null;
        }
        followingDuplicate = null;
    }

    #endregion

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
        //GetComponent<BoxCollider>().enabled = false;
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
