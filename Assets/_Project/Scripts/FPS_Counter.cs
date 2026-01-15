using UnityEngine;
using UnityEngine.UI;
public class FPS_Counter : MonoBehaviour
{
    int frameCount = 0;
    float dt = 0;
    float fps = 0;
    float updateRate = 4;  // 4 updates per sec.
    Text fps_text;
    private void Awake()
    {
        fps_text = GetComponent<Text>();
    }
    void Update()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0 / updateRate)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1 / updateRate;
        }
        fps_text.text = (int)fps + " FPS";
    }
}
