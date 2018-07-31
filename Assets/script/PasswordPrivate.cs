using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordPrivate : MonoBehaviour {
    public GameObject password_Input;
    public Toggle Public;
    public void Visible_Input_password()
    {
        
    }

    public void Update()
    {
        if (this.gameObject.GetComponent<Toggle>().isOn)
        {
            password_Input.SetActive(true);
            Public.GetComponent<Toggle>().isOn = false;
        }
        else
        {
            password_Input.SetActive(false);
            Public.GetComponent<Toggle>().isOn = true;
        }
 
    }
}
