using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    public int ID;
    public string itemName;
    public TextMeshProUGUI itemText;
    public GameObject WheelManager;
    private bool selected; 
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        selected = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            itemText.text = itemName;
        }
    }

    public void Selected()
    {
        selected = true;
        WheelManager.GetComponent<WheelController>().operatorID = ID;
    }

    public void Deselected()
    {
        selected = false;
        WheelManager.GetComponent<WheelController>().operatorID = 0;
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
        itemText.text = itemName;
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        itemText.text = "";
    }
}
