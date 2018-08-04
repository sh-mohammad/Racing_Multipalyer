using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using System;

public class HostManager : MonoBehaviour {

    private SocketIOComponent H_Socket;
    public InputField room;
    public InputField password;
    public Toggle Status;
    public LevelManager levelmanager;
    private void Start()
    {
        H_Socket = GameObject.FindObjectOfType<NetworkManager>().GetComponent<SocketIOComponent>();
        H_Socket.On("room_exist", Room_Exist);
        H_Socket.On("spawn_hoster", SpawnHoster);

        
        
    }

    private void Room_Exist(SocketIOEvent obj)
    {
        Debug.Log("Room has exist id : " + obj.data["id"].ToString());
    }

    public void CreatRoom()
    {

        bool status;
        if (Status.GetComponent<Toggle>().isOn)
        {

            status = true;
        }
        else
        {
            status = false;
        }

        H_Socket.Emit("createroom", new JSONObject(string.Format(@"{{""name"":""{0}"",""Password_Romm"":""{1}"",""Statuse"":""{2}""}}", room.text, password.text, status)));
    }
    private void SpawnHoster(SocketIOEvent obj)
    {

        Debug.Log("Hoster spawned with id : " + obj.data["id"]);
        levelmanager.LoadLevel("Game");
    }
}
