using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonHelper : MonoBehaviour {

    public static string VectorToJson(Vector3 posion)
    {
        //        Debug.Log("in vector to json room :" + room);
        return string.Format(@"{{""X"":""{0}"",""Y"":""{1}"",""Z"":""{2}""}}", posion.x, posion.y, posion.z);
    }
    public static string QuaternionToJson(Quaternion quat)
    {
        return string.Format(@"{{""X"":""{0}"",""Y"":""{1}"",""Z"":""{2}"",""W"":{3}}}", quat.x, quat.y, quat.z, quat.w);
    }


    public static float GetFloatFromJson(JSONObject data, string key)
    {
        return float.Parse(data[key].ToString().Replace("\"", ""));
    }
    public static string GetStringFromJson(string line)
    {
        return line.Replace("\"", "");
    }
}
