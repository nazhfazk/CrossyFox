using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{
    [SerializeField] Car carPrefab;

    [SerializeField] float minCarSpawnInterval;

    [SerializeField] float maxCarSpawnInterval;

    float timer;

    Vector3 carspawnPosition;

    Quaternion carRotation;

    private void Start()
    {
        if (Random.value > 0.5f)
        {
            carspawnPosition = new Vector3(
                  horizontalSize / 2 + 3,
                  0,
                  this.transform.position.z);

            carRotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            carspawnPosition = new Vector3(
                -(horizontalSize / 2 + 3),
                0,
                this.transform.position.z);

            carRotation = Quaternion.Euler(0, 90, 0);

        }

    }

    private void Update()
    {
        if (timer <= 0 )
        {
            timer = Random.Range(
                minCarSpawnInterval,
                maxCarSpawnInterval);

            var car = Instantiate(
                carPrefab, 
                carspawnPosition, 
                carRotation);

            car.SetUpDistanceLimit(horizontalSize + 6);

            return;
        }
        timer -= Time.deltaTime;
    }

}
