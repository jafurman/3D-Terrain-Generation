using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlightLight;
    public KeyCode toggleKey = KeyCode.R;
    public PlayerInventory playerInventory; 
    public float batteryDrainRate = 2f;
    public float detectionRange = 10f; 
    public int rayCount = 10; 

    void Start()
    {
        flashlightLight.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (!flashlightLight.enabled && playerInventory.GetBatteryPercent() > 0)
            {
                flashlightLight.enabled = true;
            }
            else
            {
                flashlightLight.enabled = false;
            }
        }

        if (flashlightLight.enabled)
        {
            DrainBattery();
            DetectInCone();
        }
    }

    void DrainBattery()
    {
        float currentBattery = playerInventory.GetBatteryPercent();
        currentBattery -= batteryDrainRate * Time.deltaTime;
        currentBattery = Mathf.Max(currentBattery, 0);
        playerInventory.SetBatteryPercent(currentBattery);

        if (currentBattery == 0)
        {
            flashlightLight.enabled = false;
        }
    }

    void DetectInCone()
    {
        float halfAngle = flashlightLight.spotAngle / 2f;
        Vector3 forward = flashlightLight.transform.forward;

        for (int i = 0; i < rayCount; i++)
        {

            float angle = Mathf.Lerp(-halfAngle, halfAngle, (float)i / (rayCount - 1));
            Vector3 direction = Quaternion.Euler(0, angle, 0) * forward;

            RaycastHit hit;
            if (Physics.Raycast(flashlightLight.transform.position, direction, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log("Detected Enemy at " + hit.point);
                }
            }

            Debug.DrawRay(flashlightLight.transform.position, direction * detectionRange, Color.blue);
        }
    }
}