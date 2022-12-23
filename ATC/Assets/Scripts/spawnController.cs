using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnController : MonoBehaviour
{
    public CircleCollider2D backgroundCollider;
    public GameObject airPlanePrefab;
    public float spawnDelay = 5.0f;
    public int levelNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(levelSpawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator levelSpawner(int levNum)
    {
        for(int i = 0; i < levNum + 2; i++)
        {
            Vector2 spawnPoint = new Vector2();
            spawnPoint = Random.insideUnitCircle.normalized * backgroundCollider.radius * 2.80f;
            float angle = Mathf.Atan2(spawnPoint.y, spawnPoint.x) * Mathf.Rad2Deg + 90;
            angle += Random.Range(-60.0f, 60.0f); //Adds variety to angle so they aren't all heading towards the center
            Instantiate(airPlanePrefab, spawnPoint, Quaternion.Euler(0, 0, angle));
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public IEnumerator levelSpawner()
    {
        while(true)
        {
            Vector2 spawnPoint = new Vector2();
            spawnPoint = Random.insideUnitCircle.normalized * backgroundCollider.radius * 2.80f;
            float angle = Mathf.Atan2(spawnPoint.y, spawnPoint.x) * Mathf.Rad2Deg + 90;
            angle += Random.Range(-60.0f, 60.0f); //Adds variety to angle so they aren't all heading towards the center
            Instantiate(airPlanePrefab, spawnPoint, Quaternion.Euler(0, 0, angle));
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
