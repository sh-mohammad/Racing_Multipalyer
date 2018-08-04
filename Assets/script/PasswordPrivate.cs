using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordPrivate : MonoBehaviour {
    public GameObject password_Input;
    public Toggle Public;

    public void Update()
    {
        if (this.gameObject.GetComponent<Toggle>().isOn)
        {
            password_Input.SetActive(true);
        }
        else
        {
            password_Input.SetActive(false);
        }
 
    }
}
