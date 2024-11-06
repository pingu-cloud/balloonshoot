using UnityEngine;
using TMPro; // Required if using TextMeshPro, otherwise use UnityEngine.UI

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    
    private float deltaTime = 0.0f;
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // Calculate the time passed since the last frame
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        // Calculate FPS
        float fps = 1.0f / deltaTime;

        // Update the UI text
        fpsText.text = Mathf.Ceil(fps).ToString() + " FPS";
    }
    private void LateUpdate()
    {
        
    }
}
