using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeColliderController : MonoBehaviour {

    [Header("References")]
    public Transform WallPrefab;
    public Transform Player;

    [Header("Wall Behaviours")]
    public BounceBehaviour.Type LeftWallBehaviour;
    public BounceBehaviour.Type RightWallBehaviour;
    public BounceBehaviour.Type BotWallBehaviour;
    public BounceBehaviour.Type TopWallBehaviour;

    Camera cam;
    Vector3 leftCenter, rightCenter, topCenter, botCenter;
    float yScreenSize, xScreenSize;

    private void Awake()
    {
        cam = Camera.main;
        float wallDistanceFromCamera = Vector3.Distance(transform.position, Player.position);

        //get the instantiation points
        leftCenter = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, wallDistanceFromCamera));
        rightCenter = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, wallDistanceFromCamera));
        topCenter = cam.ViewportToWorldPoint(new Vector3(0.5f, 1, wallDistanceFromCamera));
        botCenter = cam.ViewportToWorldPoint(new Vector3(0.5f, 0, wallDistanceFromCamera));

        //get the size of the colliders (not working)
        yScreenSize = (cam.ViewportToWorldPoint(new Vector2(0, 1)) - cam.ViewportToWorldPoint(new Vector2(0, 0))).magnitude;
        xScreenSize = Vector3.Distance(cam.ViewportToWorldPoint(new Vector2(0, 1)), cam.ViewportToWorldPoint(new Vector2(1, 1)));

        //instantiate walls
        Transform InstantiatedLeftWall = Instantiate(WallPrefab, leftCenter, Quaternion.identity, transform);
        Transform InstantiatedRightWall = Instantiate(WallPrefab, rightCenter, Quaternion.identity, transform);
        Transform InstantiatedTopWall = Instantiate(WallPrefab, topCenter, Quaternion.identity, transform);
        Transform InstantiatedBotWall = Instantiate(WallPrefab, botCenter, Quaternion.identity, transform);

        //rotate walls
        InstantiatedLeftWall.rotation = Quaternion.Euler(InstantiatedLeftWall.rotation.x, InstantiatedLeftWall.rotation.y, 90f);
        InstantiatedRightWall.rotation = Quaternion.Euler(InstantiatedRightWall.rotation.x, InstantiatedRightWall.rotation.y, 90f);
        InstantiatedTopWall.rotation = Quaternion.Euler(InstantiatedTopWall.rotation.x + 90f, InstantiatedTopWall.rotation.y, InstantiatedTopWall.rotation.z);
        InstantiatedBotWall.rotation = Quaternion.Euler(InstantiatedBotWall.rotation.x + 90f, InstantiatedBotWall.rotation.y, InstantiatedBotWall.rotation.z);

        //get bounce behaviour components
        BounceBehaviour leftBehaviour = InstantiatedLeftWall.GetComponent<BounceBehaviour>();
        BounceBehaviour rightBehaviour = InstantiatedRightWall.GetComponent<BounceBehaviour>();
        BounceBehaviour topBehaviour = InstantiatedTopWall.GetComponent<BounceBehaviour>();
        BounceBehaviour botBehaviour = InstantiatedBotWall.GetComponent<BounceBehaviour>();

        //sets the behaviour
        leftBehaviour.BehaviourType = LeftWallBehaviour;
        rightBehaviour.BehaviourType = RightWallBehaviour;
        topBehaviour.BehaviourType = TopWallBehaviour;
        botBehaviour.BehaviourType = BotWallBehaviour;
    }

}
