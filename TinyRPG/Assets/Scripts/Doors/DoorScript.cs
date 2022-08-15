using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform exit;
    public Transform PosCam;
    public int idNextRoom;
    public bool isLoked;
    public bool openWithKey;
    public Sprite[] doorsSprites;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (isLoked) spriteRenderer.sprite = doorsSprites[1];
        else spriteRenderer.sprite = doorsSprites[0];
    }

    public void openDoor()
    {
        isLoked = false;
        spriteRenderer.sprite = doorsSprites[0];
    }
}
