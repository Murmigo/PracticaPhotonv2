using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    public void SetRed()
    {
        NetWorkManager.Instance.PlayerColor = Color.red;

    }

    public void SetGreen()
    {
        NetWorkManager.Instance.PlayerColor = Color.green;

    }

    public void SetBlue()
    {
        NetWorkManager.Instance.PlayerColor = Color.blue;

    }
}
