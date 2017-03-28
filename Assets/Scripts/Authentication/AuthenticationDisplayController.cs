using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthenticationDisplayController : MonoBehaviour {

    public GameObject LandingView;
    public GameObject LoginView;
    public GameObject CreateAccountView;

    private Animator TransitionAnimator;

    public void OnLandingViewTransition()
    {
    }

    public void OnViewTransition(GameObject view)
    {
        HideOtherViewsExcept(view);
        view.SetActive(!view.activeSelf);
    }

    private void HideOtherViewsExcept(GameObject view)
    {
        if(view == LoginView)
        {
            CreateAccountView.SetActive(false);
        }
        else
        {
            LoginView.SetActive(false);
        }
    }
}