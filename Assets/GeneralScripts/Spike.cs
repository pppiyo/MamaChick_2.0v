using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject player; // Reference to the PlayerMovement script.
    private int playerNumber;
    private PlayerControl playerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerControl>();
        // playerNumber = GetplayerNumber();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            Debug.Log("Spike");
            // HurtPlayer();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

    private void HurtPlayer()
    {

        // int playerNumber = GetplayerNumber(player);
        playerScript.UpdateScore(-1);
    }


    // private int GetplayerNumber()
    // {

    //     GameObject playerTextGameObject = player.transform.Find("Player_Number").gameObject;
    //     TMP_Text playerText = playerTextGameObject.GetComponent<TMP_Text>();

    //     return int.Parse(playerText.text);
    // }
}
