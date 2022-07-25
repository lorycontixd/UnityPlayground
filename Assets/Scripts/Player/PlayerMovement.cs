using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using Lore.Stats;
using System;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private NavMeshAgent agent;
    public Camera cam;
    public bool canMove = true;
    public bool canLook = true;
    public Quaternion defaultRotation;

    [Tooltip("Layers that do not block clicking raycast for moving.")]
    public LayerMask agentIgnoreLayer;

    [Header("Camera Components")]
    public GameObject virtualCameraObject;
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineFollowZoom virtualCameraZoom;

    [Range(0f, 1f)]
    public float cameraSensitivityHorizontal = 0.3f;
    [Range(0f, 1f)]
    public float cameraSensitivityVertical = 0.3f;
    [Range(0.3f, 2f)]
    public float cameraVerticalZoomDamping = 0.9f;

    private float defaultZoomWidth = 12.5f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GetComponent<Player>();

        if (virtualCamera == null)
        {
            virtualCamera = virtualCameraObject.GetComponent<CinemachineVirtualCamera>();
        }
        if (virtualCameraZoom == null)
        {
            virtualCameraZoom = virtualCameraObject.GetComponent<CinemachineFollowZoom>();
        }
        virtualCameraZoom.m_Damping = cameraVerticalZoomDamping;
        player.MoveSpeed.onValueChange += OnMoveSpeedChange;
    }

    private void OnMoveSpeedChange(Stat obj)
    {
        if (obj.GetType().Name == "MoveSpeed")
        {
            agent.speed = player.MoveSpeed.Value;
        }
    }

    public void Update()
    {
        agent.speed = player.MoveSpeed.Value;
        if (canMove)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, agentIgnoreLayer))
                {
                    Debug.Log("Moving");
                    agent.SetDestination(hit.point);
                }
            }
        }
        
        if (canLook)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                virtualCameraObject.transform.Rotate(new Vector3(0f, cameraSensitivityHorizontal, 0f), Space.World);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                virtualCameraObject.transform.Rotate(new Vector3(0f, -cameraSensitivityHorizontal, 0f), Space.World);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                virtualCameraZoom.m_Width = Mathf.Clamp(virtualCameraZoom.m_Width - cameraSensitivityVertical, 0f, defaultZoomWidth);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                virtualCameraZoom.m_Width += cameraSensitivityVertical;
            }
        }
    }
}
