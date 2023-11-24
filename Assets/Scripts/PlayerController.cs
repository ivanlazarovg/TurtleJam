using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float horizontalAxis;
    private float verticalAxis;
    [SerializeField]
    private float moveSpeed;

    private void Update() {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        transform.position += new Vector3(horizontalAxis, verticalAxis, 0).normalized * moveSpeed;
    }
}
