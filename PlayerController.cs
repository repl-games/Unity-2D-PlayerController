using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  private float playerSpeed = 5f;
  private Animator m_animator;

  // Start is called before the first frame update
  void Start() {
    m_animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update() {
    MovePlayer();
  }

  private void MovePlayer() {
    Vector2 moveVector = GetMovementInput();

    UpdatePlayerAnimator(moveVector.x, moveVector.y);
    MovePlayerFree(moveVector);
  }

  private Vector2 GetMovementInput() {
    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical   = Input.GetAxisRaw("Vertical");

    // Lock directions to avoid diagonal movements
    if (horizontal != 0f) {
      vertical = 0f;
    }

    return new Vector2(horizontal, vertical);
  }

  // Free movement of player
  private void MovePlayerFree(Vector2 moveVector) {
    Vector3 position = transform.position;

    if (moveVector.y > 0) {
      position += Vector3.up;
    }
    if (moveVector.y < 0) {
      position += Vector3.down;
    }
    if (moveVector.x > 0) {
      position += Vector3.right;
    }
    if (moveVector.x < 0) {
      position += Vector3.left;
    }

    transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime*playerSpeed);
  }

  private void UpdatePlayerAnimator(float horizontal, float vertical) {
    if (horizontal != 0f || vertical != 0f) {
      m_animator.SetBool("Walking", true);
      if (horizontal > 0f) {
        m_animator.SetFloat("LastMoveHorizontal", 1f);
      } else if (horizontal < 0f) {
        m_animator.SetFloat("LastMoveHorizontal", -1f);
      } else {
        m_animator.SetFloat("LastMoveHorizontal", 0f);
      }

      if (vertical > 0f) {
        m_animator.SetFloat("LastMoveVertical", 1f);
      } else if (vertical < 0f) {
        m_animator.SetFloat("LastMoveVertical", -1f);
      } else {
        m_animator.SetFloat("LastMoveVertical", 0f);
      }
    } else {
      m_animator.SetBool("Walking", false);
    }

    m_animator.SetFloat("Horizontal", horizontal);
    m_animator.SetFloat("Vertical", vertical);
  }
}
