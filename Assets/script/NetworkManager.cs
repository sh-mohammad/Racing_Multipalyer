﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using System;


public class NetworkManager : MonoBehaviour {
    static SocketIOComponent Socket;
    public InputField room_name;
    public GameObject PlayerPrefabe;
    public GameObject Canves;
    public GameObject MyPlayer;
    public InputField Password_room;
    public GameObject scrolcontent;
    public Toggle Private;
    private Dictionary<string, Players> players;
    public GameObject roomprefabe;
    public Sprite[] lock_unluck;
    public GameObject panel_password;
    
    // Use this for initialization


    void Start () {
		Socket = GetComponent<SocketIOComponent> ();
		Socket.On ("open", OnConnected);
        Socket.On("room_exist", Room_Exist);
        Socket.On("room_not_exist", Room_Not_Exist);
        Socket.On("spawn_hoster", SpawnHoster);
        Socket.On("spawn_joiner", SpawnJoiner);
        Socket.On("room_is_full", RoomIsFull);
        Socket.On("otherspawn", Onotherspawn);
        Socket.On("requestposition", onrequestposition);
        Socket.On("updatap", OnUpdateP);
        Socket.On("move", OnMove);
        Socket.On("rotate", OnRotate);
        Socket.On("listrooms", OnListRoom);
        Socket.On("password_errore", OnPasswordErrore);
        players = new Dictionary<string, Players>();
    }



    private void OnListRoom(SocketIOEvent obj)
    {
       // Debug.Log("in Onlistroom name room is " + obj.data["name_room"].ToString().Trim() + obj.data["status"].ToString().Trim());
        GameObject newroom = Instantiate(roomprefabe) as GameObject;
        RoomContoller controller = newroom.GetComponent<RoomContoller>();
        controller.Name.text = JsonHelper.GetStringFromJson(obj.data["name_room"].ToString());
        if (JsonHelper.GetStringFromJson( obj.data["status"].ToString().Trim()) == "True")
        {
            controller.Icon.sprite = lock_unluck[0];
            controller.status = true;
        }
        else
        {
            controller.Icon.sprite = lock_unluck[1];
            controller.status = false;
        }
        
        controller.Description.text = "number of player in this room " + obj.data["number_player_in_room"].ToString();
        
        newroom.transform.parent = scrolcontent.transform;
        newroom.transform.localScale = Vector3.one;
    }

    private void OnRotate(SocketIOEvent obj)
    {        
        players[obj.data["id"].ToString()].Player.transform.Rotate(JsonHelper.GetFloatFromJson(obj.data, "X"), JsonHelper.GetFloatFromJson(obj.data, "Y"), JsonHelper.GetFloatFromJson(obj.data, "Z"));
        Debug.Log("player rotate with id " + obj.data["id"].ToString() + "to ");
    }

    private void OnMove(SocketIOEvent obj)
    {
        Debug.Log("player move with id " + obj.data["id"].ToString());
        Vector3 pos = new Vector3(JsonHelper.GetFloatFromJson(obj.data, "X"), JsonHelper.GetFloatFromJson(obj.data, "Y"), JsonHelper.GetFloatFromJson(obj.data, "Z"));

        players[obj.data["id"].ToString()].Player.transform.position = pos;
        Debug.Log("move player to " + pos + "id: " + obj.data["id"].ToString());
    }

    //when myplayer moved this function called
    public static void CammandMove(Vector3 vec3)
    {
        Socket.Emit("player move", new JSONObject(JsonHelper.VectorToJson(vec3)));
    }
    //when myplayer rotated this function called
    public static void CammandRotate(Quaternion quat)
    {
        Socket.Emit("player rotate", new JSONObject(JsonHelper.QuaternionToJson(quat)));
    }
    private void OnUpdateP(SocketIOEvent obj)
    {
        
        Vector3 pos = new Vector3(JsonHelper.GetFloatFromJson(obj.data, "X"), JsonHelper.GetFloatFromJson(obj.data, "Y"), JsonHelper.GetFloatFromJson(obj.data, "Z"));
        
        players[obj.data["id"].ToString()].Player.transform.position = pos;
        Debug.Log("update Position" + obj.data + "id " + obj.data["id"].ToString() +  "Go to :" + pos);


    }

    private void onrequestposition(SocketIOEvent obj)
    {
        Debug.Log("server is requestposition " + obj.data["room"].ToString());
        Socket.Emit("updateposition", new JSONObject(JsonHelper.VectorToJson(MyPlayer.transform.position)));
    }

    private void Onotherspawn(SocketIOEvent obj)
    {

        Debug.Log("hi back");
        //int n = Int32.Parse( obj.data["number"].ToString());
        var player = Instantiate(PlayerPrefabe);
        Players p = new Players();
        p.Player = player;
        p.Room_Name = obj.data["room"].ToString();
        players.Add(obj.data["id"].ToString(), p);
        Debug.Log("in other spawner player spawn with id  " + obj.data["id"].ToString());
    }

    private void RoomIsFull(SocketIOEvent obj)
    {
        Debug.Log("room is full");
        Canves.SetActive(true);
    }

    private void SpawnJoiner(SocketIOEvent obj)

    {
        Debug.Log("salam");


        var player = Instantiate(PlayerPrefabe);
        Players p = new Players();
        p.Player = player;
        p.Room_Name = obj.data["room"].ToString();
        players.Add(obj.data["id"].ToString(), p);
        Debug.Log("joiner spawned to room: " + obj.data["room"] + "by id : " + obj.data["id"]);
    }

    private void SpawnHoster(SocketIOEvent obj)
    {
        
        Debug.Log("Hoster spawned with id : " + obj.data["id"]);
        Canves.SetActive(false);

    }
    private void OnPasswordErrore(SocketIOEvent obj)
    {
        Debug.Log("password not correct");
        Canves.SetActive(true);
    }

    private void Room_Not_Exist(SocketIOEvent obj)
    {
        Debug.Log("can not join to room :" + obj.data["room"].ToString());
        
        Canves.SetActive(true);
    }

    private void Room_Exist(SocketIOEvent obj)
    {
        Debug.Log("Room has exist id : " + obj.data["id"].ToString());
    }

    void OnConnected(SocketIOEvent e){
		Debug.Log ("this clint is connected with id : " + e.data["id"]);
	}
	// Update is called once per frame
	public void CreateRoome(){
        bool Status;
        if (Private.GetComponent<Toggle>().isOn)
        {
     
            Status = true;
        }
        else
        {
            Status = false;
        }
        
        Socket.Emit ("createroom", new JSONObject (string.Format(@"{{""name"":""{0}"",""Password_Romm"":""{1}"",""Statuse"":""{2}""}}", room_name.text, Password_room.text, Status)));
	}

    static public void JoinRoome(string name, string password = "")
    {

        Socket.Emit("jointoroom", new JSONObject(string.Format(@"{{""name"":""{0}"",""password"":""{1}""}}", name, password)));
        GameObject canvesss = GameObject.Find("Canvas Manager");
        canvesss.SetActive(false);
    }
     


   


}

 

