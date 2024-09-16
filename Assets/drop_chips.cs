using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop_chips : MonoBehaviour
{
    public float height = 10f;
    public Transform stackPosition;
    public float random_angle = 0.08f;
    public float vertical_acceleration = 2f;
    private Rigidbody rb;
    public float X_Z_rand_offset = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.centerOfMass = new Vector3(0.4f, 0f, 0.4f);

            rb.constraints = RigidbodyConstraints.None;

            GameObject[] chips = GameObject.FindGameObjectsWithTag("Chip");

            foreach (GameObject chip in chips)
            {
                // Находим скрипт chip_control на фишке и сбрасываем её состояние
                chip_control chipController = chip.GetComponent<chip_control>();
                if (chipController != null)
                {
                    chipController.ResetChipState();
                }
            }

            Vector3 newPosition = stackPosition.position;
            newPosition.y += height;                        //тепаем для сброса
            transform.position = newPosition;


            rb.AddForce(Random.Range(-X_Z_rand_offset, X_Z_rand_offset), -vertical_acceleration, Random.Range(-X_Z_rand_offset, X_Z_rand_offset), ForceMode.Impulse); //бросаем стопку

            Vector3 randomRotation = new Vector3(
                Random.Range(-random_angle, random_angle),
                Random.Range(-random_angle, random_angle),
                Random.Range(-random_angle, random_angle)
            );

            rb.AddTorque(randomRotation, ForceMode.Impulse);
        }
    }
}
