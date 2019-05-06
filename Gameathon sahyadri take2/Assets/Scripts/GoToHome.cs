using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToHome : MonoBehaviour
{
    public void LoadHome()
    {
        SceneManager.LoadScene("start");
    }
}
