using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private MiniMapaScript miniMapa;
    private Rigidbody2D rigidbody;
    private Animator animator;


    public Transform mainCamera;
    public Transform rayPoint;
    public LayerMask rayInteractLayer;

    public float speed;
    private bool isWalk;
    private bool isDoor;
    private bool isAttack;
    private bool isLoockLeft;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;
        miniMapa = FindObjectOfType(typeof(MiniMapaScript)) as MiniMapaScript;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Debug.DrawRay(rayPoint.position, new Vector2(h, v) * 0.12f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, new Vector2(h, v), 0.12f, rayInteractLayer);

        if (hit && !isDoor)
        {
            isDoor = true;
            DoorScript tmp = hit.transform.gameObject.GetComponent<DoorScript>();
            teleport(tmp.exit, tmp.PosCam, tmp.idNextRoom);


        }

        if (h != 0 || v != 0) isWalk = true;
        if (h == 0 && v == 0) isWalk = false;

        // Layer 0 = front, Layer 1 = back, Layer 2 = side

        if (v > 0) // movimento para cima
        {
            animator.SetLayerWeight(1, 1);
            animator.SetLayerWeight(2, 0);
        }
        else if (v < 0) // movimento para baixo
        {
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(2, 0);
        }

        if (v == 0 && h != 0)
        {
            animator.SetLayerWeight(2, 1);

            if (h > 0 && isLoockLeft) flip();
            else if (h < 0 && !isLoockLeft) flip();
        }

        if (v != 0)
        {
            spriteRenderer.flipX = false;
            isLoockLeft = false;
        }

        if (Input.GetButtonDown("Fire1")) animator.SetTrigger("attack");

        rigidbody.velocity = new Vector2(h * speed, v * speed);
        animator.SetBool("walk", isWalk);
    }

    void flip()
    {
        isLoockLeft = !isLoockLeft;
        spriteRenderer.flipX = isLoockLeft;
    }

    public void onAttackEnd()
    {
        isAttack = false;
    }

    void teleport(Transform posPlayer, Transform posCam, int idRoom)
    {
        transform.position = posPlayer.position;
        mainCamera.position = new Vector3(posCam.position.x, posCam.position.y, -10);
        miniMapa.updateMiniMapa(idRoom);
        isDoor = false;
    }
}
