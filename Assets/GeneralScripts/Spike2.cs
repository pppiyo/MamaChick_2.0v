using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike2 : MonoBehaviour
{
    public GameObject player; // Reference to the PlayerMovement script.
    private int playerNumber;
    private PlayerControl playerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerControl>();
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            HurtPlayer();
        }
    }

    private void HurtPlayer()
    {
        playerScript.UpdateScore(-1);
    }
}
