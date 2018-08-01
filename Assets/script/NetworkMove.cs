using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
public class NetworkMove : MonoBehaviour {
    public SocketIOComponent Socket;

    // Use this for initialization
    public  void CammandMove(Vector3 vec3)
    {
         Socket.Emit("player move", new JSONObject(JsonHelper.VectorToJson(vec3)));
    }
    //when myplayer rotated this function called
    public void CammandRotate(Quaternion quat)
    {
        Socket.Emit("player rotate", new JSONObject(JsonHelper.QuaternionToJson(quat)));
    }

}
