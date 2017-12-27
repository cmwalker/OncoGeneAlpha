using UnityEngine;            

public class Tumor : MonoBehaviour {
	public GameObject cellPrefab;
	// Use this for initialization
	void Start () {
		cellPrefab = Instantiate(cellPrefab) as GameObject;
		cellPrefab.transform.position = new Vector3 (0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
