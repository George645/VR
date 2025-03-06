using System;
using System.Collections.Generic;
using Unity.Mathematics;
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
    private LayerMask noCollision;
    private LayerMask collision;

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
    public CharacterScript RightCharacter { get { return rightCharacter; } }
    public CharacterScript LeftCharacter { get { return leftCharacter; } }
    #endregion

    private void Start() {
        noCollision = LayerMask.NameToLayer("No character collision");
        collision = LayerMask.NameToLayer("character collision layer");
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
        catch { }
    }
    private void Update() {

        FaceCamera();
        if (followingDuplicate != null) {
            UpdateFollowerPosition();
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
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    #endregion

    #region move follower to this position

    private void UpdateFollowerPosition() {
        followingDuplicate.transform.position = transform.position;
        followingDuplicate.transform.rotation = transform.rotation;
    }

    #endregion

    #region selected
    private bool beenSelected = false;
    public void Selected() {
        CreateFollowingDuplicate();
        DetachFromParentCharacter();
        DisableAllColliders(gameObject);
    }

    #region Disable colliders temporarily
    void DisableAllColliders(GameObject parentObject) {
        parentObject.transform.GetChild(0).gameObject.SetActive(false);
        if (parentObject == gameObject) {
            parentObject.transform.GetChild(1).GetComponent<MeshCollider>().enabled = false;
        }
        else {
            Debug.Log("dutjfd");
            parentObject.transform.GetChild(0).gameObject.layer = noCollision;
        }
    }
    #endregion

    #region Detach child from parent
    void DetachFromParentCharacter(){
        if (parent != null) {
            parent.GetComponent<CharacterScript>().followingDuplicate = null;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    #endregion

    #region Create a duplicate that follows this object
    void CreateFollowingDuplicate(){
        if (thisPrefab == null) { 
            foreach (GameObject number in allTheNumberPrefabs) {
                if (number.name[0] == name[0]) {
                    thisPrefab = number;
                    break;
                }
            }
        }
        followingDuplicate = Instantiate(thisPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        DisableAllColliders(followingDuplicate);
        followingDuplicate.GetComponent<CharacterScript>().parent = gameObject;
        followingDuplicate.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }
    #endregion

    #endregion

    #region deseleft

    public void DeSelect() {
        DestroyFollower();
        DisconnectFromParent();
        ReEnableColliders();
        RealignPosition();
        if (currentEquals != null) {
            Destroy(currentEquals);
        } //not sure what this if statement is for, TEST IT LATER
    }

    #region destroy follower should it not be selected and moved
    void DestroyFollower(){
        if (followingDuplicate != null && !followingDuplicate.GetComponent<CharacterScript>().beenSelected) {
            Destroy(followingDuplicate);
        }
    }
    #endregion

    #region Disconnect from parent
    void DisconnectFromParent(){
        if (parent != null) {
            parent = null;
        }
        followingDuplicate = null;
    }
    #endregion

    #region ReEnable colliders
    
    void ReEnableColliders(){
        transform.GetChild(1).GetComponent<MeshCollider>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.layer = collision;
    }

    #endregion

    #region realign position

    void RealignPosition() {
        Vector3 normalizedXZ = new Vector2(transform.position.x, transform.position.z);
        normalizedXZ = normalizedXZ.normalized * 10;
        Debug.Log(transform.position.x + ", " + normalizedXZ.x + ", " + transform.position.z + ", " + normalizedXZ.y);
        transform.position = new Vector3(normalizedXZ.x, Math.Clamp(transform.position.y, -20f, 20f), normalizedXZ.y);

    }
    #endregion

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
        double circumpherenceOfTheCircle = 20 * math.PI;
        if (rightCharacter != null & leftCharacter != null) {
            transform.position = (rightCharacter.transform.position + leftCharacter.transform.position) / 2;
        }
        else if (rightCharacter != null) {
            transform.position = new Vector3(rightCharacter.transform.position.x * (float)Math.Cos(28.6479f) - rightCharacter.transform.position.z * (float)Math.Sin(28.6479f), transform.position.y, rightCharacter.transform.position.x * (float)Math.Sin(28.6479f) + rightCharacter.transform.position.z * (float)Math.Cos(28.6479f));
        }
        else if (leftCharacter != null) {
            transform.position = new Vector3(leftCharacter.transform.position.x * (float)-Math.Cos(28.6479f) - leftCharacter.transform.position.z * (float)-Math.Sin(28.6479f), transform.position.y, leftCharacter.transform.position.x * (float)-Math.Sin(28.6479f) + leftCharacter.transform.position.z * (float)-Math.Cos(28.6479f));
        }
    }
    #endregion

}
