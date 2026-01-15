using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        float rotationAmount = input * rotationSpeed * Time.deltaTime;

        transform.Rotate(0, rotationAmount, 0);
    }
}
