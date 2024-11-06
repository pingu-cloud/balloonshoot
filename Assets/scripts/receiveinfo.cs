using UnityEngine;

public class DataReceiver : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;

    private int buttonValue;

    void Start()
    {
        // Retrieve the integer value set by the previous scene
        buttonValue = PlayerPrefs.GetInt("ButtonValue", 0);

        // Activate the corresponding GameObject based on buttonValue
        ActivateGameObject(buttonValue);

        // Remove the stored value from PlayerPrefs to prevent reuse
        PlayerPrefs.DeleteKey("ButtonValue");
    }

    void ActivateGameObject(int value)
    {
        // Deactivate all objects initially
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);
        object4.SetActive(false);

        // Activate the specific object based on the value received
        switch (value)
        {
            case 1:
                object1.SetActive(true);
                break;
            case 2:
                object2.SetActive(true);
                break;
            case 3:
                object3.SetActive(true);
                break;
            case 4:
                object4.SetActive(true);
                break;
            default:
                Debug.LogWarning("Invalid button value received!");
                break;
        }
    }
}
