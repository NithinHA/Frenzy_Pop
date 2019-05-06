using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour {

	[Header("Platform Details")]
	public int platform_length = 1;
	public GameObject platform_prefab;
	public Sprite platform_sprite;

	[HideInInspector]
	private GameObject[] _platform;
	public GameObject[] platform
	{
		get { return _platform; }
		//set { _platform = value; }
	}
	
	protected virtual void Start() {
		_platform = new GameObject[platform_length];

		for (int i = 0; i < platform_length; i++)
		{
			GameObject floor = Instantiate(platform_prefab, transform.position + new Vector3(i, 0, 0), Quaternion.identity);
			floor.GetComponent<SpriteRenderer>().sprite = platform_sprite;
			floor.transform.SetParent(transform);
			_platform[i] = floor;
		}
	}

}
