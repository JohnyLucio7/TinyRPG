using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapaScript : MonoBehaviour
{
    public Transform roomsParent;
    public GameObject[] rooms;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject r in rooms)
        {
            r.SetActive(false);
        }

        rooms[0].SetActive(true);
    }

    public void updateMiniMapa(int idRoom)
    {
        rooms[idRoom].SetActive(true);
        Vector2 posMapa = new Vector2(rooms[idRoom].transform.localPosition.x, rooms[idRoom].transform.localPosition.y) * -1;
        roomsParent.localPosition = posMapa;
    }
}
