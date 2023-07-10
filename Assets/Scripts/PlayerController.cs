using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float x = 0;
    [SerializeField] private float y = 0;
    [SerializeField] private float z = 0;
    [SerializeField] private float w = 0;
    [SerializeField] private float turnSpeed = 250f;
    [SerializeField] private float fwdSpeed = 5f;

    private NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>(
        new MyCustomData
        {
            _int    = 42,
            _bool   = true,
            message = "Ichiban",
        },
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
        );

    public struct MyCustomData : INetworkSerializable
    {
        public int    _int;
        public bool   _bool;
        public string message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _int);
            serializer.SerializeValue(ref _bool);
            serializer.SerializeValue(ref message);
        }
    }

    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (MyCustomData previousNum, MyCustomData newNum) =>
        {
            Debug.Log(OwnerClientId + ": " + newNum._int + ", " + newNum._bool + ", " + newNum.message);
        };
    }

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            randomNumber.Value = new MyCustomData
            {
                _int    = Random.Range(0,100),
                _bool   = false,
                message = "Wakka wakka do do ya!",
            };
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, 0, 1 * turnSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 0, -1 * turnSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up * fwdSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.up * -fwdSpeed * Time.deltaTime;
        }
    }
}
