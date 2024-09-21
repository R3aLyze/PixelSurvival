using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildController : MonoBehaviour
{
    public RuleTile grassTile;
    public Tilemap groundTileMap;

    public float castDistance = 1.0f;
    public Transform raycastPoint;
    public LayerMask layer;

    float blockDestroyTime = 0.2f;

    Vector3 direction;
    RaycastHit2D hit;

    bool destroyingBlock = false;
    bool placingBlock = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            RaycastDirection();
        }
    }

    void RaycastDirection()
    {
        if (Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") != 0)
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");
        }

        hit = Physics2D.Raycast(raycastPoint.position, direction, castDistance, layer.value);

        Vector2 endpos = raycastPoint.position + direction;

        if (Input.GetMouseButton(0))
        {
            if (hit.collider && !destroyingBlock)
            {
                destroyingBlock = true;
                StartCoroutine(DestroyBlock(hit.collider.gameObject.GetComponent<Tilemap>(), endpos));
            }
        }
        if (Input.GetMouseButton(1))
        {
            if(!hit.collider && !placingBlock)
            {
                placingBlock = true;
                StartCoroutine(PlaceBlock(groundTileMap, endpos));
            }
        }
    }

    IEnumerator DestroyBlock(Tilemap map, Vector2 pos)
    {
        yield return new WaitForSeconds(blockDestroyTime);

        pos.y = Mathf.Floor(pos.y);
        pos.x = Mathf.Floor(pos.x);

        map.SetTile(new Vector3Int((int)pos.x, (int)pos.y, 0), null);

        destroyingBlock = false;
    }
    IEnumerator PlaceBlock(Tilemap map, Vector2 pos)
    {
        yield return new WaitForSeconds(blockDestroyTime);

        pos.y = Mathf.Floor(pos.y);
        pos.x = Mathf.Floor(pos.x);

        map.SetTile(new Vector3Int((int)pos.x, (int)pos.y, 0), grassTile);

        placingBlock = false;
    }
}
