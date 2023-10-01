using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnnemyFollow : MonoBehaviour
{
    LevelCreator levelCreator;
    UtilitiesNonStatic uns;
    Vector3 destination;
    public  float width = 2f;

    void Awake()
    {
        uns = UtilitiesStatic.GetUNS();
        levelCreator = uns.levelCreator;
    }

    private void Start()
    {
        FindeNextdestination();
    }

    private void FixedUpdate()
    {
        Vector3 directionToTarget = destination - transform.position;

        float angleDeg = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angleDeg), 0.1f);

        transform.position += directionToTarget.normalized * 3 * Time.fixedDeltaTime;

        FindeNextdestination();
        
    }

    private void FindeNextdestination()
    {
        Vector3 raycastDir = uns.player.transform.position - transform.position;
        Vector3 rightSide = transform.position + transform.up * width / 2;
        Vector3 leftSide = transform.position - transform.up * width / 2;
        if( !Physics2D.Raycast(rightSide, raycastDir, raycastDir.magnitude, uns.levelLayerMask) &&
            !Physics2D.Raycast(leftSide, raycastDir, raycastDir.magnitude, uns.levelLayerMask))
        {
            Debug.DrawLine(rightSide, rightSide + raycastDir);
            Debug.DrawLine(leftSide, leftSide + raycastDir);
            destination = uns.player.transform.position;
            return;
        }

        Vector2Int pos = new Vector2Int((int)((transform.position.x + 1f) / 2), (int)((transform.position.y + 1f) / 2));
        LevelTile tileOn = levelCreator.map[pos.x][pos.y];


        raycastDir = destination - transform.position;
        rightSide = transform.position + transform.up * width / 2;
        leftSide = transform.position - transform.up * width / 2;

        Vector3 nextdestination = destination;

        int insurancy = 20;
        while( !Physics2D.Raycast(rightSide, raycastDir, raycastDir.magnitude) &&
            !Physics2D.Raycast(leftSide, raycastDir, raycastDir.magnitude) && insurancy > 0){
            insurancy--;



            if(tileOn == null)
            {
                return;
            }
            destination = nextdestination;
            nextdestination = new Vector2(tileOn.position.x, tileOn.position.y) * 2;

            tileOn = tileOn.parent;


            raycastDir = nextdestination - transform.position;
            rightSide = transform.position + transform.up * width / 2;
            leftSide = transform.position - transform.up * width / 2;


        }
        Debug.DrawLine(rightSide, rightSide + raycastDir);
        Debug.DrawLine(leftSide, leftSide + raycastDir);
        Debug.Log(insurancy);

    }
}