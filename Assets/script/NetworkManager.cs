using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using System;


public class NetworkManager : MonoBehaviour {
    static SocketIOComponent Socket;
    
    void Start () {
		Socket = GetComponent<SocketIOComponent> ();
		Socket.On ("open", OnConnected);

    }



    void OnConnected(SocketIOEvent e){
		Debug.Log ("this clint is connected with id : " + e.data["id"]);
	}






}

 

