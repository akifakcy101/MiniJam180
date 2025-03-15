using UnityEngine;

public class SpectatorCamera : MonoBehaviour
{
    public float moveSpeed = 10f; // Normal hareket h�z�
    public float sprintMultiplier = 2f; // Shift'e bas�nca h�z �arpan�
    public float lookSpeed = 3f;  // Kameran�n d�nme h�z�

    private float yaw = 0f; // Yatay d�n�� a��s�
    private float pitch = 0f; // Dikey d�n�� a��s�

    void Update()
    {
        // Fare giri�ine g�re kameray� d�nd�rme
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f); // Kameran�n dikey eksende a��r� d�nmesini engelle

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);

        // Klavye giri�ine g�re kameray� hareket ettirme
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // A/D veya Sol/Sa� ok tu�lar�
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;   // W/S veya Yukar�/A�a�� ok tu�lar�
        float moveY = 0f;

        if (Input.GetKey(KeyCode.Q)) // Yukar� hareket (Q tu�u)
        {
            moveY = moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E)) // A�a�� hareket (E tu�u)
        {
            moveY = -moveSpeed * Time.deltaTime;
        }

        // Shift tu�u ile h�zlanma
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * sprintMultiplier : moveSpeed;

        Vector3 movement = new Vector3(moveX, moveY, moveZ);
        transform.Translate(movement * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f));
    }
}