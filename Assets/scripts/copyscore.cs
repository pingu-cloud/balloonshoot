using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class copyscore : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public TextMeshProUGUI menuuiText;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        menuuiText.text = uiText.text;
    }
}
