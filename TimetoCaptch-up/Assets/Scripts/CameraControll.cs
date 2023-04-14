using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [Header("Look Parameters")]
    [SerializeField] private float lookSpeed = 400f;
    [SerializeField] private float maxRotX = 75;
    [SerializeField] private float minRotX = -75;
    
    public float rotationX = 0;
    public float rotationY = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * lookSpeed;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * lookSpeed;

        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, minRotX, maxRotX);
        if (rotationY > 359.99f) rotationY = 0;
        if (rotationY < 0) rotationY = 359.95f;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
