using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenWhenEnemiesDeath : MonoBehaviour
{
    public DoorScript door;
    public List<GameObject> enemies;

    public void removeEnemy(GameObject e)
    {
        enemies.Remove(e);
        if (enemies.Count == 0) door.openDoor();
    }
}
