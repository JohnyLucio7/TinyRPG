using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private SpriteRenderer spriteRenderer;
    private MiniMapaScript miniMapa;
    private new Rigidbody2D rigidbody;
    private FadeTransition fade;
    private Animator animator;

    public Transform mainCamera;
    public Transform rayPoint;
    public LayerMask rayInteractLayer;

    public float speed;
    public bool isDoor;

    public GameObject slashFront, slashBack, slashSideR, slashSideL;
    public int idDirection; // 0 - Front, 1 - Back, 2 - Side

    public int keys;

    private bool isWalk;
    private bool isAttack;
    private bool isLoockLeft;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;
        miniMapa = FindObjectOfType(typeof(MiniMapaScript)) as MiniMapaScript;
        fade = FindObjectOfType(typeof(FadeTransition)) as FadeTransition;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Verificando colisão com a porta

        Debug.DrawRay(rayPoint.position, new Vector2(h, v) * 0.12f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, new Vector2(h, v), 0.12f, rayInteractLayer);

        if (hit && !isDoor)
        {
            DoorScript tmp = hit.transform.gameObject.GetComponent<DoorScript>();

            if (tmp.isLoked && tmp.openWithKey)
            {
                if (keys > 0)
                {
                    keys--;
                    tmp.openDoor();
                }
            }
            else if (!tmp.isLoked)
            {
                isDoor = true;
                fade.startFade(tmp);
            }

        }

        if (h != 0 || v != 0) isWalk = true;
        if (h == 0 && v == 0) isWalk = false;

        // Layer 0 = front, Layer 1 = back, Layer 2 = side

        if (v > 0) // movimento para cima
        {
            idDirection = 1;
            animator.SetLayerWeight(1, 1);
            animator.SetLayerWeight(2, 0);
        }
        else if (v < 0) // movimento para baixo
        {
            idDirection = 0;
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(2, 0);
        }

        if (v == 0 && h != 0) // movimento para o lado
        {
            idDirection = 2;
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

    public void slash()
    {
        GameObject slashPrefab = null;

        switch (idDirection)
        {
            case 0:
                slashPrefab = slashFront;
                break;
            case 1:
                slashPrefab = slashBack;
                break;
            case 2:
                slashPrefab = isLoockLeft ? slashSideL : slashSideR;
                break;
            default:
                break;
        }

        GameObject tmpSlash = Instantiate(slashPrefab, transform.position, transform.localRotation);
        Destroy(tmpSlash, 0.5f);
    }

    public void onAttackEnd()
    {
        isAttack = false;
    }

    public void teleport(Transform posPlayer, Transform posCam, int idRoom)
    {
        transform.position = posPlayer.position;
        mainCamera.position = new Vector3(posCam.position.x, posCam.position.y, -10);
        miniMapa.updateMiniMapa(idRoom);
        isDoor = false;
    }
}
