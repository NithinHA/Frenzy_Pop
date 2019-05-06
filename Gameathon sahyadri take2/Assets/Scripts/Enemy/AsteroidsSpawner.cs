using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour {

    public GameObject asteriodPrefab;
    public float delayBetweenSpawn = 0.8f;
    public float randomizeY = 1f;
    private bool canSpawn = true;

    // Update is called once per frame
    void Update() {
        if (canSpawn) {
            float y = Random.Range(-randomizeY, randomizeY);
            Instantiate(asteriodPrefab, new Vector2(transform.position.x, transform.position.y + y), Quaternion.identity);
            StartCoroutine(Wait(delayBetweenSpawn));
        }

    }

    IEnumerator Wait(float time) {
        canSpawn = false;
        yield return new WaitForSeconds(time);
        canSpawn = true;
    }
}
