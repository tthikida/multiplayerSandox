using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float speed = 1.0f;

    private NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>(
        new MyCustomData
        {
            _int = 42,
            _bool = true,
        },
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
        );

    public struct MyCustomData
    {
        public int _int;
        public bool _bool;
    }

    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (MyCustomData previousNum, MyCustomData newNum) =>
        {
            Debug.Log(OwnerClientId + ": " + newNum._int + ", " + newNum._bool);
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            randomNumber.Value = new MyCustomData
            {
                _int = Random.Range(0,100),
                _bool = false,
            };
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
