using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector3 currentPosition;
    private Quaternion currentrotation;
    private Vector3 oldposition;
    private Quaternion oldrotation;
	// Use this for initialization

	void Start () {
        oldrotation = transform.rotation;
        currentrotation = oldrotation;
        oldposition = transform.position;
        currentPosition = oldposition;
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 150.0f;
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
        currentPosition = transform.position;
        currentrotation = transform.rotation;

        if (currentPosition != oldposition)
        {
            NetworkManager.CammandMove(currentPosition);
            oldposition = currentPosition;
        }
        if (currentrotation != oldrotation)
        {
            NetworkManager.CammandRotate(transform.rotation);
            Debug.Log("currentrotaiton " + transform.rotation);
            oldrotation = currentrotation;
        }
    }
}
