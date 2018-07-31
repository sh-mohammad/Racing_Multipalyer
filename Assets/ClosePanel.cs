using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePanel : MonoBehaviour {
    public InputField Password;
    public InputField room;
    public void paneldown()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetBool("IsOpen", false);
    }
    public void joinwithpassword()
    {

        NetworkManager.JoinRoome(room.text, Password.text);
    }
}
