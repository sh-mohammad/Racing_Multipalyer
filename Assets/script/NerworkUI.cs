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
    public GameObject roomprefabe;
    public Sprite[] lock_unluck;


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
            if (NetworkManager.RoomsPrefabe.ContainsKey(input.GetComponent<InputField>().text))
            {
                GameObject newroom = Instantiate(roomprefabe) as GameObject;
                RoomContoller controller = newroom.GetComponent<RoomContoller>();
                controller.Name.text = NetworkManager.RoomsPrefabe[input.GetComponent<InputField>().text].Name.text;
                if (NetworkManager.RoomsPrefabe[input.GetComponent<InputField>().text].status)
                {
                    controller.Icon.sprite = lock_unluck[0];
                    controller.status = true;
                }
                else
                {
                    controller.Icon.sprite = lock_unluck[1];
                    controller.status = false;
                }

                controller.Description.text = NetworkManager.RoomsPrefabe[input.GetComponent<InputField>().text].Description.text;
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
