using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapaScript : MonoBehaviour
{
    public Transform roomsParent;
    public GameObject[] rooms;
    public GameObject[] roomsGamePlay;

    private int currentIdRoom = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject r in roomsGamePlay)
        {
            r.SetActive(false);
        }

        foreach (GameObject r in rooms)
        {
            r.SetActive(false);
        }

        rooms[currentIdRoom].SetActive(true);
        roomsGamePlay[currentIdRoom].SetActive(true);
    }

    public void updateMiniMapa(int idRoom)
    {
        rooms[idRoom].SetActive(true);

        roomsGamePlay[idRoom].SetActive(true);

        roomsGamePlay[currentIdRoom].SetActive(false);

        currentIdRoom = idRoom;

        Vector2 posMapa = new Vector2(rooms[idRoom].transform.localPosition.x, rooms[idRoom].transform.localPosition.y) * -1;

        roomsParent.localPosition = posMapa;
    }
}
