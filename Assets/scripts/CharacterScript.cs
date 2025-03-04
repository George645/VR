using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour{
#nullable enable
    GameObject? followingDuplicate = null;
    GameObject? currentEquals = null;
#nullable disable

    #region Public variables
    public CharacterScript rightCharacter;
    public CharacterScript leftCharacter;
    #endregion

    #region Static number variables
    public static List<GameObject> allTheNumberPrefabs;
    #endregion

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
    public CharacterScript RightThing { get { return rightCharacter; } }
    public CharacterScript LeftCharacter { get { return leftCharacter; } }
    #endregion

    private void Start() {
        try {
            if (name.Length > 1) {
                character = Convert.ToString(Convert.ToInt16(name.Remove(1)));
                isNumber = true;
            }
            else {
                character = Convert.ToString(Convert.ToInt16(name));
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
    }
    private void Update() {

        FaceCamera();
        if (followingDuplicate != null) {
            UpdateFolloweePosition();
        }
        if (parent != null && isNumber) {
            CheckEqualsAvailiability();
        }
    }
    private void LateUpdate() {
        if (!Input.GetMouseButton(0)) {
            RealignCharacters();
        }
    }

    #region Face camera

    void FaceCamera(){
        transform.rotation = Quaternion.FromToRotation(transform.position, Camera.main.transform.position);
    }

    #endregion

    #region move followee to this position

    private void UpdateFolloweePosition() {
        followingDuplicate.transform.position = transform.position;
        followingDuplicate.transform.rotation = transform.rotation;
    }

    #endregion

    #region selected
    private bool beenSelected = false;
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
        if (currentEquals != null) {
            Destroy(currentEquals);
        }
        followingDuplicate = null;
    }

    #endregion

    #region Creating equals between numbers
    private void CheckEqualsAvailiability() {
        if (Math.Abs(parent.transform.position.z - transform.position.z) < 20 && Math.Abs(parent.transform.position.x - transform.position.x) < 5 && Math.Abs(parent.transform.position.y - transform.position.y) < 10) {
            if (currentEquals == null) {
                foreach (GameObject number in allTheNumberPrefabs) {
                    if (number.name[0] == '=') {
                        currentEquals = Instantiate(number, (transform.position + parent.transform.position) / 2, Quaternion.Lerp(transform.rotation, parent.transform.rotation, 0.5f));
                    }
                }
            }
            else {
                currentEquals.transform.position = (transform.position + parent.transform.position) / 2;
            }
        }
        else if (currentEquals != null) {
            Destroy(currentEquals);
            currentEquals = null;
        }
    }
    #endregion

    #region Destroy those on the right
    public void DestroyThoseOnTheRight() {
        if (rightCharacter != null) {
            rightCharacter.DestroyThoseOnTheRight();
        }
        Destroy(this.gameObject);
    }
    #endregion

    #region Realign the objects on the sides
    private void RealignCharacters() {
        if (rightCharacter != null & leftCharacter != null) {
            transform.position = (rightCharacter.transform.position + leftCharacter.transform.position) / 2;
        }
        else if (rightCharacter != null) {
            transform.position = rightCharacter.transform.position + rightCharacter.gameObject.transform.right.normalized * 5;
        }
        else if (leftCharacter != null) {
            transform.position = leftCharacter.transform.position + leftCharacter.gameObject.transform.right.normalized * -5;
        }
    }
    #endregion

}
