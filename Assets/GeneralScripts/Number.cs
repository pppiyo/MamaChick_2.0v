using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    // Start is called before the first frame update    private Rigidbody2D rb2d;

    private Rigidbody2D rb2d;

    private void Start()
    {
        // rb2d = GetComponent<Rigidbody2D>();
        // LockPosition();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.CompareTag("Bullet"))
        // {
        //     LockPosition();
        //     StartCoroutine(UnlockPositionAfterDelay(0.5f));
        // }

        // if ((other.gameObject.CompareTag("Platform_Solid") || other.gameObject.CompareTag("Platform_Mutate") || other.gameObject.CompareTag("Ground")))
        // {
        //     LockPosition();
        // }
    }


    private void OnCollisionExit2D(Collision2D other)
    {

        // if ((other.gameObject.CompareTag("Platform_Solid") || other.gameObject.CompareTag("Platform_Mutate") || other.gameObject.CompareTag("Ground")))
        // {
        //     UnlockPosition();
        // }
    }

    // IEnumerator UnlockPositionAfterDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     UnlockPosition();
    // }


    // // Lock or freeze the position
    // public void LockPosition()
    // {
    //     rb2d.bodyType = RigidbodyType2D.Static;
    // }

    // // Unlock or unfreeze the position
    // public void UnlockPosition()
    // {
    //     rb2d.bodyType = RigidbodyType2D.Dynamic; // or Kinematic, depending on your needs
    // }

}
