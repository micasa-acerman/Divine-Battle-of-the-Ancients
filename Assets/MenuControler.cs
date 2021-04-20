using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControler : MonoBehaviour
{
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnPlay);
        DontDestroyOnLoad(GameObject.Find("BackgroundMusicControler"));
	}

	void OnPlay(){
        Debug.Log("Hello");
		SceneManager.LoadScene("SimpleArena");
	}
}
