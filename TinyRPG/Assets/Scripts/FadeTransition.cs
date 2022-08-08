using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    public Animator animator;
    public bool isDoor;

    private DoorScript door;

    public void startFade(DoorScript doorScript)
    {
        door = doorScript;
        animator.SetTrigger("FadeOut");
    }

    public void onFadeComplete()
    {
        PlayerController.instance.teleport(door.exit, door.PosCam, door.idNextRoom);
        animator.SetTrigger("FadeIn");
    }
}
