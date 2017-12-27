using UnityEngine;
using System.Collections;

public class TumorCell : MonoBehaviour {
	private float growthRate = 0.1f; // Rate of cell groth (1/second)
	private float radius; // cell radius (microns)
	private int cellCycle; // Integer for phase of cell cycle
	private int telomere; // lenght of Telomere (number of remaing devisions
	public GameObject cellPrefab;

	// Use this for initialization
	void Start () {
		Debug.Log("Welcome to the world daughter");
		telomere = 10;
		cellCycle = 1;
		radius = 25f;
		transform.localScale = new Vector3 (2f*radius, 2f*radius, 2f*radius);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch (cellCycle) {
		case 0:
			break;
		case 1:
			Grow ();
			if (Random.Range (0f, 1f) <= 0.5f * Time.fixedDeltaTime) {
				cellCycle = 2;
			}
			break;
		case 2:
			if (Random.Range (0f, 1f) <= 0.5f * Time.fixedDeltaTime) {
				cellCycle = 3;
			}
			break;
		case 3:
			Grow ();
			if (Random.Range (0f, 1f) <= 0.5f * Time.fixedDeltaTime) {
				cellCycle = 4;
			}
			break;
		case 4:
			if (Random.Range (0f, 1f) <= 0.5f * Time.fixedDeltaTime) {
				Divide ();
			}
			break;
		}
	}

	//OnCollision
	void OnCollisionEnter(Collision c){
	}

	// Grow: increases the size of the cell
	void Grow () {
		radius += radius * growthRate*Random.Range(0f,1f) * Time.fixedDeltaTime;
		transform.localScale = new Vector3 (2f*radius, 2f*radius, 2f*radius);
	}

	// Divide Splits the cell into two daugter cell
	void Divide(){
		// Determin Inheritance
		HeritableTraits inheritance = PassTraits();
		float newRadius = inheritance.Radius;
		Debug.Log (newRadius);
		// Calculate new centers
		float tmpTheta = Random.Range(-Mathf.PI,Mathf.PI);
		float tmpPhi = Random.Range(-Mathf.PI/2f,Mathf.PI/2f);
		Vector3 newPosition1 = transform.position+new Vector3 (newRadius * Mathf.Sin (tmpPhi) * Mathf.Cos (tmpTheta), newRadius * Mathf.Sin (tmpPhi) * Mathf.Sin (tmpTheta), newRadius * Mathf.Cos (tmpPhi));
		Vector3 newPosition2 = transform.position+new Vector3 (-newRadius * Mathf.Sin (tmpPhi) * Mathf.Cos (tmpTheta), -newRadius * Mathf.Sin (tmpPhi) * Mathf.Sin (tmpTheta), -newRadius * Mathf.Cos (tmpPhi));
		Debug.Log (newPosition1.ToString("F4"));
		Debug.Log (newPosition2.ToString("F4"));
		// Create Daugter 1
		cellPrefab = Instantiate(cellPrefab) as GameObject;
		cellPrefab.transform.position = newPosition1;
		TumorCell daughter1 = cellPrefab.GetComponent<TumorCell>();
		daughter1.AcceptTraits (inheritance);

		// Create Daugter 2
		cellPrefab = Instantiate(cellPrefab) as GameObject;
		cellPrefab.transform.position = newPosition2;
		TumorCell daughter2 = cellPrefab.GetComponent<TumorCell>();
		daughter2.AcceptTraits (inheritance);

		// Destroy parent
		Destroy (gameObject);
	}
	private HeritableTraits PassTraits(){
		int newTelomere = telomere - (int)Mathf.Round(Random.Range (0f, 2f));
		int newCellCycle = 1;
		float newRadius = Mathf.Pow(2,-1f/3f)*radius; // Calculate new radius to conserve volume
		HeritableTraits inheritance = new HeritableTraits (growthRate, newRadius, newCellCycle, newTelomere);
		return inheritance;
	}
	void AcceptTraits(HeritableTraits inheritance){
		growthRate = inheritance.GrowthRate;
		radius = inheritance.Radius;
		cellCycle = inheritance.CellCycle;
		telomere = inheritance.Telomere;
	}
	public class Signaling{
	}
	public class Energetics{
	}
	public class HeritableTraits{
		private float growthRate; // Rate of cell groth (1/second)
		private float radius; // cell radius (microns)
		private int cellCycle; // Integer for phase of cell cycle
		private int telomere; // lenght of Telomere (number of remaing devisions

		// Properties
		public float GrowthRate {get{return growthRate;}}
		public float Radius {get{return radius;}}
		public int CellCycle {get{return cellCycle;}}
		public int Telomere {get{return telomere;}}

		public HeritableTraits(float growthRateH, float radiusH, int cellCycleH,int telomereH){
			growthRate = growthRateH;
			radius = radiusH;
			cellCycle = cellCycleH;
			telomere = telomereH;
		}
	}
}
