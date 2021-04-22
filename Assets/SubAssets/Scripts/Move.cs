using UnityEngine;

public class Move : MonoBehaviour {

    public Vector3 goal = new Vector3(5, 0, 4);

    [Range(0.0f, 10f)]
    public float speed;

    void Start() {
        //goal = goal * speed;
    }

    private void Update() {
        this.transform.Translate(goal.normalized*speed*Time.deltaTime);

    }
}
