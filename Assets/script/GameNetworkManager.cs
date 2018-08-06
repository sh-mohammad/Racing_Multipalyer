using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class GameNetworkManager : MonoBehaviour {
    private static SocketIOComponent Socket;
    public GameObject MyPlayer;
    private Dictionary<string, Players> players;
    public GameObject PlayerPrefabe;


    private void Awake()
    {
        Socket = GameObject.FindObjectOfType<NetworkManager>().GetComponent<SocketIOComponent>();
        players = new Dictionary<string, Players>();
    }
    private void Start()
    {

        Socket.Emit("OnGameScene");

        Socket.On("requestposition", onrequestposition);
        Socket.On("spawn_joiner", SpawnJoiner);
        Socket.On("updatap", OnUpdateP);
        Socket.On("move", OnMove);
        Socket.On("rotate", OnRotate);
        Socket.On("client_disconnect", Onclientdisconnect);
        Socket.On("otherspawner", Onotherspawn);
        Socket.On("OnFire", OnFire);

    }

    private void OnFire(SocketIOEvent obj)
    {
        ShootMissile shot = players[JsonHelper.GetStringFromJson(obj.data["id"].ToString())].Player.GetComponent<ShootMissile>();
        Debug.Log("shot ");
        shot.Fire();
    }

    private void onrequestposition(SocketIOEvent obj)
    {
        Debug.Log("server is requestposition " + obj.data["room"].ToString());
        Socket.Emit("updateposition", new JSONObject(JsonHelper.VectorToJson(MyPlayer.transform.position)));
    }
    public static void CammandFire()
    {
        Socket.Emit("Fire");
    }
    public static void CammandMove(Vector3 vec3)
    {
        Socket.Emit("player move", new JSONObject(JsonHelper.VectorToJson(vec3)));
    }
    //when myplayer rotated this function called
    public static void CammandRotate(Quaternion quat)

    {
        Socket.Emit("player rotate", new JSONObject(JsonHelper.QuaternionToJson(quat)));
    }
    private void SpawnJoiner(SocketIOEvent obj)

    {
        Debug.Log("salam");


        var player = Instantiate(PlayerPrefabe);
        Players p = new Players();
        p.Player = player;
        p.Room_Name = obj.data["room"].ToString();
        players.Add(JsonHelper.GetStringFromJson(obj.data["id"].ToString()), p);
        Debug.Log("joiner spawned to room: " + obj.data["room"] + "by id : " + obj.data["id"]);
    }
    private void Onclientdisconnect(SocketIOEvent obj)
    {
        Debug.Log("client disconnect " + obj.data["id"].ToString());
        Destroy(players[JsonHelper.GetStringFromJson(obj.data["id"].ToString())].Player);
        players.Remove(JsonHelper.GetStringFromJson(obj.data["id"].ToString()));
    }



    private void OnRotate(SocketIOEvent obj)
    {
        Vector3 r = new Vector3(JsonHelper.GetFloatFromJson(obj.data, "X"), JsonHelper.GetFloatFromJson(obj.data, "Y"), JsonHelper.GetFloatFromJson(obj.data, "Z"));
        Debug.Log(r);
        players[JsonHelper.GetStringFromJson(obj.data["id"].ToString())].Player.transform.eulerAngles = r;
        Debug.Log("player rotate with id " + obj.data["id"].ToString() + "to " + players[JsonHelper.GetStringFromJson(obj.data["id"].ToString())].Player.transform.rotation);
    }

    private void OnMove(SocketIOEvent obj)
    {
        Debug.Log("player move with id " + obj.data["id"].ToString());
        Vector3 pos = new Vector3(JsonHelper.GetFloatFromJson(obj.data, "X"), JsonHelper.GetFloatFromJson(obj.data, "Y"), JsonHelper.GetFloatFromJson(obj.data, "Z"));

        players[JsonHelper.GetStringFromJson(obj.data["id"].ToString())].Player.transform.position = pos;
        Debug.Log("move player to " + pos + "id: " + obj.data["id"].ToString());
    }

    //when myplayer moved this function called

    private void OnUpdateP(SocketIOEvent obj)
    {

        Vector3 pos = new Vector3(JsonHelper.GetFloatFromJson(obj.data, "X"), JsonHelper.GetFloatFromJson(obj.data, "Y"), JsonHelper.GetFloatFromJson(obj.data, "Z"));

        players[JsonHelper.GetStringFromJson(obj.data["id"].ToString())].Player.transform.position = pos;
        Debug.Log("update Position" + obj.data + "id " + obj.data["id"].ToString() + "Go to :" + pos);


    }

    private void Onotherspawn(SocketIOEvent obj)
    {
        Debug.Log("hi back");
        //int n = Int32.Parse( obj.data["number"].ToString());
        var player = Instantiate(PlayerPrefabe);
        Players p = new Players();
        p.Player = player;
        p.Room_Name = obj.data["room"].ToString();
        players.Add(JsonHelper.GetStringFromJson(obj.data["id"].ToString()), p);
        Debug.Log("in other spawner player spawn with id  " + obj.data["id"].ToString());
    }

}
