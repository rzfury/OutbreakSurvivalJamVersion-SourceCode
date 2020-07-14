using System.Collections.Generic;
using UnityEngine;

public class Populator : MonoBehaviour
{

    public GameObject shelter;
    public GameObject puddle;
    public GameObject crate;

    public List<Vector2> tpPos = new List<Vector2>() {
        new Vector2(200, 0),
        new Vector2(260, 0),
        new Vector2(200, 60),
        new Vector2(260, 60),
        new Vector2(200, 120)
    };

    public int shelterAmount = 5;
    public int puddleAmount = 10;
    public int crateAmount = 30;

    public int prohibitionRadius = 1;
    public int circleRadius = 1;
    public Vector2 boxSize;

    void Start()
    {
        PopulatePuddle();
        PopulateShelter();
        PopulateCrate();
    }
    
    void PopulatePuddle() {
        int i = 0;
        while (i < puddleAmount) {
            bool cantSpawnHere = false;
            Vector2 spawnPos = new Vector2(
                Random.Range(-boxSize.x / 2, boxSize.x / 2),
                Random.Range(-boxSize.y / 2, boxSize.y / 2)
            );

            if(Vector2.Distance(spawnPos, Vector2.zero) < prohibitionRadius) {
                continue;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, 20);
            
            foreach(Collider2D collider in colliders) {
                if(Vector2.Distance(spawnPos, collider.transform.position) < 10) {
                    cantSpawnHere = true;
                    break;
                }
            }

            if(!cantSpawnHere) {
                Instantiate(puddle, spawnPos, Quaternion.identity);
                i++;
            }
        }
    }
    
    void PopulateShelter() {
        int i = 0;
        while (i < shelterAmount) {
            bool cantSpawnHere = false;
            Vector2 spawnPos = new Vector2(
                Random.Range(-boxSize.x / 2, boxSize.x / 2),
                Random.Range(-boxSize.y / 2, boxSize.y / 2)
            );
            
            if(Vector2.Distance(spawnPos, Vector2.zero) < prohibitionRadius) {
                continue;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, 20);
            
            foreach(Collider2D collider in colliders) {
                if(Vector2.Distance(spawnPos, collider.transform.position) < 20) {
                    cantSpawnHere = true;
                    break;
                }
            }

            if(!cantSpawnHere) {
                GameObject shelterObj = Instantiate(shelter, spawnPos, Quaternion.identity);
                Shelter shelterScript = shelterObj.GetComponent<Shelter>();
                shelterScript.tpPos = tpPos[i];
                i++;
            }
        }
    }
    
    public void PopulateCrate() {
        int i = 0;
        while (i < crateAmount) {
            bool cantSpawnHere = false;
            Vector2 spawnPos = new Vector2(
                Random.Range(-boxSize.x / 2, boxSize.x / 2),
                Random.Range(-boxSize.y / 2, boxSize.y / 2)
            );
            
            if(Vector2.Distance(spawnPos, Vector2.zero) < prohibitionRadius) {
                continue;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, 20);
            
            foreach(Collider2D collider in colliders) {
                if(Vector2.Distance(spawnPos, collider.transform.position) < 5) {
                    cantSpawnHere = true;
                    break;
                }
            }

            if(!cantSpawnHere) {
                Instantiate(crate, spawnPos, Quaternion.identity);
                i++;
            }
        }
    }
}
