using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WheelController : MonoBehaviour
{
    public int operatorID;
    public Button addDefault;

    // Start is called before the first frame update
    void Start()
    {
        operatorID = 0;
        EventSystem.current.SetSelectedGameObject(addDefault.gameObject);
    }

    void Update()
    {
        if(operatorID == 0)
        {
            EventSystem.current.SetSelectedGameObject(addDefault.gameObject);
        }
    }
}
