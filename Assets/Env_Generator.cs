using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env_Generator : MonoBehaviour
{
    static Color foodColor = new Color(0.5f, 0.5f, 0.8f);
    public GameObject Block, Wall; float rangeX = 4, rangeY = 4; static float x, y; static int ix, iy; static bool b;
    public static List<List<SpriteRenderer>> sprites = new List<List<SpriteRenderer>>();
    // Awake----------------------------------------------------------------------------
    void Awake() {
        Vector2 vec = new Vector2(); i = 0;
        for (vec.x = -rangeX; vec.x <= rangeX; vec.x+=0.5f) {
            sprites.Add(new List<SpriteRenderer>()); j = 0;
            for (vec.y = -rangeY; vec.y <= rangeY; vec.y+=0.5f) {
                var insted = Instantiate(Block, vec, Quaternion.identity);
                sprites[i].Add(insted.GetComponent<SpriteRenderer>());
                j++;
            }
            i++;
        }
        DrawWalls();
        DisAll();
        foodPos = new I2(sprites.Count / 2, sprites.Count / 2); setFood();
    }

    static int i, j; static I2 i2Temp;
    public static void Draw(ref I2 pos, List<I2> tail) { // DRAW
        // Collision
        foreach (I2 block in tail) {
            if (pos == block) { Score.GameOver(); return; }
        }
        // Draw
        if (pos.x >= sprites.Count) { pos.x = 0; } else if (pos.x < 0) { pos.x = sprites.Count - 1; }
        if (pos.y >= sprites[0].Count) { pos.y = 0; } else if (pos.y < 0) { pos.y = sprites.Count - 1; }
        sprites[pos.x][pos.y].enabled = true;
        // FOOD
        if (checkOnFood(pos, tail)) {
            tail.Insert(0, pos);
        }
        else {
            i2Temp = tail[tail.Count - 1]; sprites[i2Temp.x][i2Temp.y].enabled = false;
            for (i = tail.Count-2; i >= 0; i--) {
                tail[i + 1] = tail[i];
            }
            tail[0] = pos;
        }
    }


    static I2 foodPos;
    static void setFood() {
        sprites[foodPos.x][foodPos.y].enabled = true;
        sprites[foodPos.x][foodPos.y].color = foodColor;
    }

    static bool checkOnFood(I2 pos, List<I2> tail) {
        if (foodPos == pos) {
            sprites[foodPos.x][foodPos.y].color = Color.white;
            foodPos.x = Random.Range(0, sprites.Count);
            foodPos.y = Random.Range(0, sprites.Count);
            for (int i = 0; i < tail.Count; i++) {
                if (foodPos == tail[i]) {
                    for (ix = 0; ix < sprites.Count; ix++) {
                        for (iy = 0; iy < sprites[0].Count; iy++)
                        {
                            foodPos.x = ix; foodPos.y = iy; b = false;
                            foreach (I2 block in tail) {
                                if (tail[i] == foodPos) { b = true; break; }
                            }
                            if (pos == foodPos) b = true;
                            if (!b) { goto Final; }
                        }
                    }
                }
            }
        Final:
            Score.ADD(); setFood();
            return true;
        }
        return false;
    }

    public void DrawWalls() {
        for (x = -rangeX; x <= rangeX; x+=0.5f) {
            Instantiate(Wall, new Vector3(x, rangeY, 1), Quaternion.identity);
            Instantiate(Wall, new Vector3(x,-rangeY, 1), Quaternion.identity);
        }
    }


    public static void DisAll() {
        for (i = 0; i < sprites.Count; i++) {
            for (j = 0; j < sprites[i].Count; j++) { sprites[i][j].enabled = false; }
        }
    }
}
