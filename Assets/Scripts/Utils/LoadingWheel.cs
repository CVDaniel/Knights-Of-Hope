using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingWheel : MonoBehaviour
{
    private static GameObject m_loadingView = null;

    private void Start()
    {
        //new GameObject().transform.parent = this.gameObject.transform;
        Debug.Log("START");
        m_loadingView = Instantiate(Resources.Load("Prefabs/Authentication/LoadingView") as GameObject);
        m_loadingView.transform.SetParent(this.transform, false);
        m_loadingView.SetActive(false);
    }

    public static void StarAnimation()
    {
        if (m_loadingView)
        {
            m_loadingView.SetActive(true);
        }
    }

    public static void StopAnimation()
    {
        if (m_loadingView)
        {
            m_loadingView.gameObject.SetActive(false);
        }
    }
}
