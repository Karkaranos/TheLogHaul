using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject player;

    [Header("Camera Clamps")]
    [SerializeField] private float topClamp;
    [SerializeField] private float bottomClamp;
    [SerializeField] private float leftClamp;
    [SerializeField] private float rightClamp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 pPos = player.transform.position;
        transform.position = new Vector3(Mathf.Clamp(pPos.x, leftClamp, rightClamp),
            Mathf.Clamp(pPos.y, bottomClamp, topClamp), -10);
    }
}
