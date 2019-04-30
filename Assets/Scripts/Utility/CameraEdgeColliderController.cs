using UnityEngine;
using Sangaku;

public class CameraEdgeColliderController : MonoBehaviour {

    #region Inspector Variables
    [Header("References")]
    public Transform WallPrefab;
    public Transform Player;

    [Header("Wall Behaviours")]
    public BounceOn.BounceType LeftWallBehaviour;
    public BounceOn.BounceType RightWallBehaviour;
    public BounceOn.BounceType BotWallBehaviour;
    public BounceOn.BounceType TopWallBehaviour;
    #endregion

    #region Variables
    Camera cam;
    Vector3 leftCenter, rightCenter, topCenter, botCenter;
    float yScreenSize, xScreenSize;
    #endregion

    #region MonoBehaviour methods
    private void Awake()
    {
        cam = Camera.main;
        SetUp();
    }
    #endregion

    #region Script methods
    void SetUp()
    {
        float wallDistanceFromCamera = Vector3.Distance(transform.position, Player.position);

        //get the instantiation points
        leftCenter = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, wallDistanceFromCamera));
        rightCenter = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, wallDistanceFromCamera));
        topCenter = cam.ViewportToWorldPoint(new Vector3(0.5f, 1, wallDistanceFromCamera));
        botCenter = cam.ViewportToWorldPoint(new Vector3(0.5f, 0, wallDistanceFromCamera));

        //get the size of the screen in world space (not working)
        Vector3 BotLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, wallDistanceFromCamera));
        Vector3 TopLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, wallDistanceFromCamera));
        Vector3 TopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, wallDistanceFromCamera));
        yScreenSize = Vector3.Distance(BotLeft, TopLeft);
        xScreenSize = Vector3.Distance(TopLeft, TopRight);

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

        //Scale Walls
        InstantiatedLeftWall.localScale = new Vector3(InstantiatedLeftWall.localScale.x, InstantiatedLeftWall.localScale.y, yScreenSize);
        InstantiatedRightWall.localScale = new Vector3(InstantiatedRightWall.localScale.x, InstantiatedRightWall.localScale.y, yScreenSize);
        InstantiatedTopWall.localScale = new Vector3(xScreenSize, InstantiatedTopWall.localScale.y, InstantiatedTopWall.localScale.z);
        InstantiatedBotWall.localScale = new Vector3(xScreenSize, InstantiatedBotWall.localScale.y, InstantiatedBotWall.localScale.z);

        //get bounce behaviour components
        BounceOn leftBehaviour = InstantiatedLeftWall.GetComponent<BounceOn>();
        BounceOn rightBehaviour = InstantiatedRightWall.GetComponent<BounceOn>();
        BounceOn topBehaviour = InstantiatedTopWall.GetComponent<BounceOn>();
        BounceOn botBehaviour = InstantiatedBotWall.GetComponent<BounceOn>();

        //sets the behaviour
        leftBehaviour.BehaviourType = LeftWallBehaviour;
        rightBehaviour.BehaviourType = RightWallBehaviour;
        topBehaviour.BehaviourType = TopWallBehaviour;
        botBehaviour.BehaviourType = BotWallBehaviour;
    }
    #endregion

}
