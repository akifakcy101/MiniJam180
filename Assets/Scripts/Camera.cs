using UnityEngine;

public class SpectatorCamera : MonoBehaviour
{
    public float moveSpeed = 10f; // Normal hareket hýzý
    public float sprintMultiplier = 2f; // Shift'e basýnca hýz çarpaný
    public float lookSpeed = 3f;  // Kameranýn dönme hýzý

    private float yaw = 0f; // Yatay dönüþ açýsý
    private float pitch = 0f; // Dikey dönüþ açýsý

    void Update()
    {
        // Fare giriþine göre kamerayý döndürme
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f); // Kameranýn dikey eksende aþýrý dönmesini engelle

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);

        // Klavye giriþine göre kamerayý hareket ettirme
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // A/D veya Sol/Sað ok tuþlarý
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;   // W/S veya Yukarý/Aþaðý ok tuþlarý
        float moveY = 0f;

        if (Input.GetKey(KeyCode.Q)) // Yukarý hareket (Q tuþu)
        {
            moveY = moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E)) // Aþaðý hareket (E tuþu)
        {
            moveY = -moveSpeed * Time.deltaTime;
        }

        // Shift tuþu ile hýzlanma
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * sprintMultiplier : moveSpeed;

        Vector3 movement = new Vector3(moveX, moveY, moveZ);
        transform.Translate(movement * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f));
    }
}