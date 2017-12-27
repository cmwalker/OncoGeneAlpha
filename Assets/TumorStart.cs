using UnityEngine;
using System.Collections;

public class TumorStart : MonoBehaviour {
	public GameObject cellPrefab;
	// Use this for initialization
	void Start () {
		Instantiate (cellPrefab);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
