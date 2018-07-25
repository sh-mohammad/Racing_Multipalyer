using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using System;

public class NetworkManager : MonoBehaviour {
	static SocketIOComponent Socket;
    public InputField room_name;
    public InputField join_name;
    public GameObject PlayerPrefabe;
    public GameObject Canves;
    public GameObject MyPlayer;
    Players players = new Players();
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
       // Socket.On("requestposition", onrequestposition);
       // Socket.On("updateposition", onupdateposition);
    }

    //private void onupdateposition(SocketIOEvent obj)
    //{
        //Debug.Log("Update Position" + obj.data);
      //  var position = new Vector3 (GetFloatFromJson(obj.data, "X"), 0, GetFloatFromJson(obj.data, "Y")); 
    //}

    //private void onrequestposition(SocketIOEvent obj)
    //{
      //  Debug.Log("server is requestposition " + obj.data["room"].ToString());
        //VectorToJson(MyPlayer.transform.position, obj.data["room"].ToString());
        //Socket.Emit("updateposition", new JSONObject(VectorToJson(MyPlayer.transform.position, obj.data["room"].ToString())));
    //}

    private void Onotherspawn(SocketIOEvent obj)
    {   

        //int n = Int32.Parse( obj.data["number"].ToString());
        var player = Instantiate(PlayerPrefabe);
        players.Player = player;
        players.Room_Name = obj.data["room"].ToString();
        players.ID = obj.data["id"].ToString();
    }

    private void RoomIsFull(SocketIOEvent obj)
    {
        Debug.Log("room is full");
        Canves.SetActive(true);
    }

    private void SpawnJoiner(SocketIOEvent obj)
    {
        Debug.Log("joiner spawned to room: " + obj.data["room"] + "by id : " + obj.data["id"]);
        var player = Instantiate(PlayerPrefabe);
        players.Player = player;
        players.Room_Name = obj.data["room"].ToString();
        players.ID = obj.data["id"].ToString();
    }

    private void SpawnHoster(SocketIOEvent obj)
    {
        Debug.Log("Hoster spawned with id : " + obj.data["id"]);
        Canves.SetActive(false);
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
        
        Socket.Emit ("createroom", new JSONObject (string.Format(@"{{""name"":""{0}""}}", room_name.text)));
	}
    public void JoinRoome()
    {
        Socket.Emit("jointoroom", new JSONObject(string.Format(@"{{""name"":""{0}""}}", join_name.text)));
        Canves.SetActive(false);
    }
    string VectorToJson(Vector3 posion, string room)
    {
        Debug.Log("in vector to json room :" + room);
        string t_room = room;
        return string.Format(@"{{""X"":""{0}"",""Y"":""{1}"",""Z"":""{2}"",""room"":""{3}""}}", posion.x, posion.y, posion.z, t_room);
    }
    float GetFloatFromJson(JSONObject data, string key)
    {
        return float.Parse(data[key].ToString().Replace("\"", ""));
    }

}
