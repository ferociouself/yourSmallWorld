using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

	List<Vertex> path;

	int curIndex = 0;

	Vertex curTarget;

	bool disabled = false;

	public bool showTarget = false;

	public bool backAndForth = false;

	float disabledBuffer = 0.0f;
	float maxDisabledBuffer;

	float enabledBuffer = 0.0f;
	float maxEnabledBuffer = 0.25f;

	public float proxToTarget = 0.1f;

	Rigidbody rb;

	SphereTerrain sphere;

	public float speed;

	public Vertex start;
	public Vertex targetGoal;

	// Use this for initialization
	void Start () {
		maxDisabledBuffer = Random.Range(3.0f, 10.0f);
		sphere = GameObject.Find("Planet").GetComponent<SphereTerrain>() as SphereTerrain;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!disabled) {
			GetComponent<Animator>().SetBool("Walking", true);
			rb.constraints = RigidbodyConstraints.FreezeRotation;
			if (path != null) {
				if (path.Count > 0) {
					if ((transform.position - curTarget.getTransformedPoint()).magnitude < proxToTarget) {
						curIndex++;
						if (curIndex >= path.Count && backAndForth) {
							Vertex temp = targetGoal;
							targetGoal = start;
							start = temp;
							setPath();
						} else if (curIndex >= path.Count) {
							GetComponent<SmolMan>().findNewBuilding();
							setPath();
						}
						if (path == null) {
							disabled = true;
						} else {
							if (path.Count == 1) {
								curIndex = 0;
							}
							curTarget = path[curIndex];
							if (enabledBuffer > maxEnabledBuffer) {
								setPath();
								enabledBuffer = 0.0f;
							} else {
								enabledBuffer += Time.deltaTime;
							}
						}
					} else {
						Vector3 direction = curTarget.getTransformedPoint() - transform.position;
						if (rb.velocity.magnitude < speed / 50) {
							rb.velocity = direction.normalized * 2 * speed;
						} if (rb.velocity.magnitude < 0.001f) {
							transform.position = curTarget.getTransformedPoint();
						}
						rb.velocity = direction.normalized * speed;
					}
				}
			} else {
				setPath();
			}
		} else {
			GetComponent<SmolMan>().getCommunity().FreeOneBoi(GetComponent<SmolMan>());
			disabled = false;
		}
	}

	public void setPath() {
		path = AStar.FindPath(sphere.getVertex(sphere.findIndexOfNearest(transform.position)), targetGoal, this.gameObject);
		if (path != null && path.Count > 1) {
			curIndex = 0;
			curTarget = path[1];
			disabled = false;
		} else if (path != null && path.Count == 1) {
			curTarget = path[0];
			disabled = false;
		} else {
			disabled = true;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		if (path != null) {
			foreach (Vertex v in path) {
				Gizmos.DrawSphere(v.getTransformedPoint(), 0.1f);
			}
		}
		Gizmos.color = Color.black;
		if (disabled || showTarget) {
			if (targetGoal != null) {
				Gizmos.DrawSphere (targetGoal.getTransformedPoint (), 0.5f);
			}
		}
	}
}
