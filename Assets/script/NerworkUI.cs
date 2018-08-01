using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;
public class NerworkUI : MonoBehaviour {

    public SocketIOComponent Socket;
    public InputField room_name;
    public InputField Password_room;
    public Toggle Private;
    public InputField Password;
    public InputField room;
    public GameObject panel;
    public GameObject scrolcontent;
    public void CreateRoome()
    {
        bool Status;
        if (Private.GetComponent<Toggle>().isOn)
        {

            Status = true;
        }
        else
        {
            Status = false;
        }

        Socket.Emit("createroom", new JSONObject(string.Format(@"{{""name"":""{0}"",""Password_Romm"":""{1}"",""Statuse"":""{2}""}}", room_name.text, Password_room.text, Status)));
    }
    
    
    public void paneldown()
    {
        Animator anim = panel.GetComponent<Animator>();
        anim.SetBool("IsOpen", false);
    }
    public void joinwithpassword()
    {
        NetworkManager.JoinRoome(room.text, Password.text);
    }
    public void Refresh()
    {
        foreach (Transform child in scrolcontent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Socket.Emit("Refresh_room");
    }
}
