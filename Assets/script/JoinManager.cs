using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using System;

public class JoinManager : MonoBehaviour {
    private static SocketIOComponent J_Socket;
    public InputField Password_room;
    public InputField name_room;
    public GameObject panel;
    public GameObject scrolcontent;
    public GameObject roomprefabe;
    public Sprite[] lock_unluck;
    public LevelManager levelmanager;
    Dictionary<string, RoomContoller> RoomsPrefabe;
    // Use this for initialization
    void Start () {
        J_Socket = GameObject.FindObjectOfType<NetworkManager>().GetComponent<SocketIOComponent>();
        J_Socket.On("listrooms", OnListRoom);
        J_Socket.On("room_not_exist", Room_Not_Exist);
        J_Socket.On("room_is_full", RoomIsFull);
        J_Socket.On("password_errore", OnPasswordErrore);
        J_Socket.On("Go_To_Game", ChangeScene);

       
        RoomsPrefabe = new Dictionary<string, RoomContoller>();
    }

    private void ChangeScene(SocketIOEvent obj)
    {
        
        levelmanager.LoadLevel("Game");
        Debug.Log("load levele");
    }

    private void RoomIsFull(SocketIOEvent obj)
    {
        Debug.Log("room is full");
    }

    private void Room_Not_Exist(SocketIOEvent obj)
    {
        Debug.Log("can not join to room :" + obj.data["room"].ToString());
    }
    private void OnPasswordErrore(SocketIOEvent obj)
    {
        Debug.Log("password not correct");

    }


    public void paneldown()
    {
        Animator anim = panel.GetComponent<Animator>();
        anim.SetBool("IsOpen", false);
    }

    public static void JoinRoome(string name, string password = "")
    {

        J_Socket.Emit("jointoroom", new JSONObject(string.Format(@"{{""name"":""{0}"",""password"":""{1}""}}", name, password)));
    }
    public void joinprivate()
    {
        JoinRoome(name_room.text, Password_room.text);
    }



    public void Refresh()
    {
        foreach (Transform child in scrolcontent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        J_Socket.Emit("Refresh_room");
    }
    private void OnListRoom(SocketIOEvent obj)
    {

        // Debug.Log("in Onlistroom name room is " + obj.data["name_room"].ToString().Trim() + obj.data["status"].ToString().Trim());
        GameObject newroom = Instantiate(roomprefabe) as GameObject;
        RoomContoller controller = newroom.GetComponent<RoomContoller>();
        string name = JsonHelper.GetStringFromJson(obj.data["name_room"].ToString());
        controller.Name.text = name;
        if (JsonHelper.GetStringFromJson(obj.data["status"].ToString().Trim()) == "True")
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
        if (!RoomsPrefabe.ContainsKey(name))
        {
            RoomsPrefabe.Add(controller.Name.text, controller);
        }
        
    }
    public void Searchfild(GameObject input)
    {
        if (input.GetComponent<InputField>().text == "")
        {
            Refresh();

        }
        else
        {

            foreach (Transform child in scrolcontent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            if (RoomsPrefabe.ContainsKey(input.GetComponent<InputField>().text))
            {
                GameObject newroom = Instantiate(roomprefabe) as GameObject;
                RoomContoller controller = newroom.GetComponent<RoomContoller>();
                controller.Name.text = RoomsPrefabe[input.GetComponent<InputField>().text].Name.text;
                if (RoomsPrefabe[input.GetComponent<InputField>().text].status)
                {
                    controller.Icon.sprite = lock_unluck[0];
                    controller.status = true;
                }
                else
                {
                    controller.Icon.sprite = lock_unluck[1];
                    controller.status = false;
                }

                controller.Description.text = RoomsPrefabe[input.GetComponent<InputField>().text].Description.text;
                newroom.transform.parent = scrolcontent.transform;
                newroom.transform.localScale = Vector3.one;

                Debug.Log("Find");
            }
            else
            {
                Debug.Log("Not Find" + input.GetComponent<InputField>().text);
            }

        }

    }
}
