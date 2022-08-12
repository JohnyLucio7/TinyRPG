using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASlime : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public Vector2 moveDirection;

    [Range(0f, 10f)] public float maxTimeToMove;
    [Range(0f, 10f)] public float minTimeToMove;
    [Range(0f, 10f)] public float waitMax;
    [Range(0f, 10f)] public float waitMin;
    [Range(0.01f, 1f)] public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine("moveSlime");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rigidbody.velocity = moveDirection * speed;
    }

    IEnumerator moveSlime()
    {
        yield return new WaitForSeconds(Random.Range(waitMin, waitMax));
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);

        moveDirection = new Vector2(x, y);

        yield return new WaitForSeconds(Random.Range(minTimeToMove, maxTimeToMove));
        moveDirection = new Vector2(0, 0);

        StartCoroutine("moveSlime");
    }
}
