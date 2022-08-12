using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASlime : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private bool isLockPlayer;

    public Vector2 moveDirection;

    public LayerMask whatIsPlayer;

    [Range(0f, 10f)] public float maxTimeToMove;
    [Range(0f, 10f)] public float minTimeToMove;
    [Range(0f, 10f)] public float waitMax;
    [Range(0f, 10f)] public float waitMin;
    [Range(0.01f, 1f)] public float speed;
    [Range(0.01f, 1f)] public float radiusVision;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine("moveSlime");
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = moveDirection * speed;

        if (isLockPlayer) moveDirection = Vector3.Normalize(PlayerController.instance.transform.position - transform.position);
    }

    private void FixedUpdate()
    {
        isLockPlayer = Physics2D.OverlapCircle(transform.position, radiusVision, whatIsPlayer);
    }

    IEnumerator moveSlime()
    {
        moveDirection = new Vector2(0, 0);

        yield return new WaitForSeconds(Random.Range(waitMin, waitMax));

        if (!isLockPlayer)
        {
            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);

            moveDirection = new Vector2(x, y);

            yield return new WaitForSeconds(Random.Range(minTimeToMove, maxTimeToMove));
        }

        StartCoroutine("moveSlime");
    }
}
