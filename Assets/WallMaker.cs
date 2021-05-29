
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaker : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform lastBlock;
    Vector3 lastBlockPos;
    float offsetVal = 0.70711f;
    Camera cam;
    Transform player { get { return FindObjectOfType<PlayerController>().transform; } }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        lastBlockPos = lastBlock.position;
        InvokeRepeating("CreateWall", 0.5f, 0.25f);
    }


    private void CreateWall()
    {
        float distance = Vector3.Distance(player.position, lastBlockPos);
        if (distance > cam.orthographicSize * 3f) return;

        int chance = Random.Range(0, 11);
        var newBlock = Instantiate(blockPrefab, transform);
        if (chance >= 5)
        {
            newBlock.transform.position = new Vector3(lastBlockPos.x + offsetVal, lastBlockPos.y, lastBlockPos.z + offsetVal);
        }
        else
        {
            newBlock.transform.position = new Vector3(lastBlockPos.x - offsetVal, lastBlockPos.y, lastBlockPos.z + offsetVal);
        }
        bool crystalState = chance % 3 == 1;
        newBlock.transform.GetChild(0).gameObject.SetActive(crystalState);

        lastBlockPos = newBlock.transform.position;
    }
}
