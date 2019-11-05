using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_Controller : MonoBehaviour {
    I2 pos = new I2(0, 0);
    List<I2> tail = new List<I2>();

    void Start() { preDir = dir; tail.Add(pos); Debug.Log("START"); StartCoroutine(Core()); }

    WaitForSeconds wait = new WaitForSeconds(0.1f);
    IEnumerator Core() {
        while (true)
        {
            HandleMotion();
            Env_Generator.Draw(ref pos, tail);
            preDir = dir;
            yield return wait;
        }
    }

    void FixedUpdate() { HandleInput(); }

    Dir dir, preDir;
    void HandleInput() {
        if ((Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)) && (tail.Count == 1 || preDir != Dir.down)) {
            dir = Dir.up;
        } else if ((Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)) && (tail.Count == 1 || preDir != Dir.up)) {
            dir = Dir.down;
        } else if ((Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)) && (tail.Count == 1 || preDir != Dir.left)) {
            dir = Dir.right;
        } else if ((Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)) && (tail.Count == 1 || preDir != Dir.right)) {
            dir = Dir.left;
        }
    }

    void HandleMotion() {
        switch (dir) {
            case Dir.up:
                pos.y++;
                break;
            case Dir.down:
                pos.y--;
                break;
            case Dir.right:
                pos.x++;
                break;
            case Dir.left:
                pos.x--;
                break;
        }
    }
}



public struct I2 {
    public int x, y;

    public I2(int x, int y) { this.x = x; this.y = y; }

    public static bool operator ==(I2 first, I2 second) {
        if (first.x == second.x && first.y == second.y) return true; return false;
    }
    public static bool operator !=(I2 first, I2 second) {
        if (first.x != second.x || first.y != second.y) return true; return false;
    }
}


public enum Dir {
    up, down, right, left
}