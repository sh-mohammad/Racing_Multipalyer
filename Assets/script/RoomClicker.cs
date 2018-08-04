using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
public class RoomClicker : MonoBehaviour {
    GameObject panelpasseord;
    public SocketIOComponent socket;
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

            JoinManager.JoinRoome(C_R.Name.text);

        }

    }
    void panelup()
    {
        Animator anim = panelpasseord.GetComponent<Animator>();
        anim.SetBool("IsOpen", true);
    }
    
}
