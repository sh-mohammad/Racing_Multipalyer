using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClicker : MonoBehaviour {
    GameObject panelpasseord;
    private void Start()
    {
        panelpasseord = GameObject.Find("PanelPassword");

    }
    public void JoinRoomeWithPrefabe(GameObject room)
    {
        RoomContoller C_R = room.GetComponent<RoomContoller>();
        if (C_R.status)
        {
            panelup();


        }
        else
        {

            NetworkManager.JoinRoome(C_R.Name.text);
        }

    }
    void panelup()
    {
        Animator anim = panelpasseord.GetComponent<Animator>();
        anim.SetBool("IsOpen", true);
    }
    
}
