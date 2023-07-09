using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float speed = 1.0f;

    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(OwnerClientId + ": " + randomNumber.Value);
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            randomNumber.Value = Random.Range(0, 100);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1f * Time.deltaTime * speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1f * Time.deltaTime * speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 1f * Time.deltaTime * speed, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0,-1f * Time.deltaTime * speed, 0);
        }
    }
}
