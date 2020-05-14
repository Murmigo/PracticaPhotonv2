using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Player : MonoBehaviourPun
{
    // Start is called before the first frame update

    Rigidbody rb;
    Vector3 direction;

    public Image img;

    public int Health = 10;

    private string nombre = "Name";
    public Text CharacterName;

    public float Speed = 5f;
    public bool isGrounded;

    public Transform ShootingPoint;

    public LayerMask GroundMask;

    public Renderer rend;
    public PhotonView pv;
    private TextMesh cn;

    public Dictionary<Color, int> ColorDict = new Dictionary<Color, int>();
    public Dictionary<int, Color> IntColorDict = new Dictionary<int, Color>();
    
    public GameObject PlayerCamera;

    public void Awake()
    {
        
        ColorDict.Add(Color.blue, 0);
        ColorDict.Add(Color.red, 1);
        ColorDict.Add(Color.green, 2);
        IntColorDict.Add(0, Color.blue);
        IntColorDict.Add(1, Color.red);
        IntColorDict.Add(2, Color.green);

        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        pv = GetComponent<PhotonView>();
        cn = GetComponentInChildren<TextMesh>();
    }

    void Start()
    {
        if (pv.IsMine)
        {

            PlayerCamera.SetActive(true);
            rend.material.color = NetWorkManager.Instance.PlayerColor;
            Color temp = NetWorkManager.Instance.PlayerColor;

            this.pv.RPC("SetColor", RpcTarget.AllBuffered, ColorDict[temp]);
            this.pv.RPC("SetName", RpcTarget.AllBuffered, NetWorkManager.Instance.PlayerName.text);

        }
        else
        {

            PlayerCamera.SetActive(false);
        }
    }

    [PunRPC]
    void SetName(string name)
    {
        cn.text = name;
    }

    [PunRPC]
    void SetColor(int col)
    {
        if (rend == null)
            rend = GetComponent<Renderer>();

        Debug.Log("Color=== " + col);
        Color temp;
        if (IntColorDict.ContainsKey(col))
        {
            temp = IntColorDict[col];
        }
        else temp = Color.white;
        rend.material.color = temp;

        Debug.Log("SETTING COLOR FROM REMOTE PLAYER");
    }

    [PunRPC]
    void SetColorRed()
    {

        rend.material.color = Color.red;
    }

    [PunRPC]
    void SetColorGreen()
    {

        rend.material.color = Color.green;
    }

    [PunRPC]
    void SetColorBlue()
    {

        rend.material.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {

        if (Health <= 0)
            pv.RPC("DestroyPlayer", RpcTarget.AllBuffered);


        if (!pv.IsMine)
            return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        direction = new Vector3(x, rb.velocity.y, z);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * 100f, ForceMode.Impulse);

        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PhotonNetwork.Instantiate("Bullet", ShootingPoint.position, transform.rotation);
        }

        
        if (x != 0 && z != 0)
        {
            float angle = Mathf.Atan2(x, z) * Mathf.Rad2Deg + PlayerCamera.transform.eulerAngles.y;
            Quaternion rot = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 2 * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if (!pv.IsMine)
            return;
        isGrounded = Physics.CheckSphere(transform.position + new Vector3(0, -0.5f, 0), 0.1f, GroundMask);
        //direction = direction.normalized * Speed * Time.deltaTime;
        //rb.MovePosition(transform.position + direction);
        rb.velocity = ((transform.right * direction.x)+(transform.forward * direction.z) )* Speed;
    }


    [PunRPC]
    void DestroyPlayer()
    {
        Destroy(gameObject);
    }


    [PunRPC]
    void DamagePlayer(int damage)
    {
        Health -= damage;
        img.fillAmount = Health / 10;
        Debug.Log("Taking Damage == " + damage);
    }
}
