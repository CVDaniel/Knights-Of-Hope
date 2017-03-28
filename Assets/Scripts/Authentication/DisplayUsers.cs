using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUsers : MonoBehaviour {

    public GameObject m_displayView;
    public float usersMargin = 40;

    private GameObject m_scrollViewContent;

    private string m_response;

    void Start()
    {
        m_scrollViewContent = m_displayView.transform.Find("Scroll View/Viewport/Content").gameObject;
        m_response = null;
    }

	public void OnDisplayUsers()
    {
        GetUsers();
    }


    public void GetUsers()
    {
        WWW request = new WWW("http://127.0.0.1:8000/users");

        StartCoroutine(WaitForUsersRetrieval(request));
    }

    public void RemovePreviousChilds()
    {
        List<Transform> listChilds = new List<Transform>();
        foreach (Transform child in m_scrollViewContent.transform)
        {
            listChilds.Add(child);
        }

        m_scrollViewContent.transform.DetachChildren();

        foreach (Transform child in listChilds)
        {
            Destroy(child.gameObject);
        }
    }

    public void DisplayUsersAfterRetrieval()
    {
        RemovePreviousChilds();

        if (m_response != null)
        {
            Users users = new Users();
            JsonUtility.FromJsonOverwrite(m_response, users);

            RectTransform dimensions = m_scrollViewContent.transform as RectTransform;
            dimensions.sizeDelta += new Vector2(0, users.users.Count * 70);

            if(users != null)
            {
                for(int i = 1; i <= users.users.Count; i++)
                {
                    GameObject user = (GameObject)Instantiate(Resources.Load("Prefabs/DisplayUserButton"));
                    user.transform.parent = m_scrollViewContent.transform;
                    user.transform.localPosition = user.transform.localPosition - new Vector3(0, i * usersMargin, 0);
                    user.GetComponentInChildren<Text>().text = i.ToString() + ". " + "EMAIL: " + users.users[i-1].email + "SALT: " + users.users[i-1].salt;
                }
            }
            else
            {
                Debug.Log("DATA NULL");
            }
        }
        else
        {
            Debug.Log("RESPONSE NULL");
        }

        m_displayView.SetActive(true);
    }

    public IEnumerator WaitForUsersRetrieval(WWW response)
    {
        yield return response;

        if(response.error != null)
        {
            m_response = response.error;
        }
        else
        {
            m_response = response.text;
            Debug.Log("RESPONSE: " + m_response);
            DisplayUsersAfterRetrieval();
        }
    }

    public void HideDisplay()
    {
        m_displayView.SetActive(false);
    }
}

[System.Serializable]
class User
{
    public string email;
    public string salt;
}

[System.Serializable]
class Users
{
    public List<User> users;
}
