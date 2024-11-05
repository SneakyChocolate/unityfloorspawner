using System.Data;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] floorObjects;
    public GameObject[] wallObjects;

    int getField(int[][] matrix, int row, int col) {
        if (row < 0 || row >= matrix.Length || col < 0 || col >= matrix[row].Length) {
            return 0;
        }
        return matrix[row][col];
    }

    void Start()
    {
        float size = 10;
        int[][] floormatrix = {
            new int[]{0,0,0,0,1,0,2,2,2},
            new int[]{0,0,0,0,1,0,2,2,2},
            new int[]{2,2,0,0,1,0,2,2,2},
            new int[]{2,2,1,1,1,1,2,2,2},
            new int[]{0,0,0,0,1,0,0,0,0},
            new int[]{0,0,0,0,1,0,0,0,0},
        };
        var centerx = size * floormatrix.Length / 2;
        var centery = size * floormatrix[0].Length / 2;

        for (int r = 0; r < floormatrix.Length; r ++) {
            for (int c = 0; c < floormatrix[r].Length; c ++) {
                // instantiate floor object
                var field = floormatrix[r][c];
                var pos = new Vector3(size * r - centerx, 0, size * c - centery);
                if (field == 0) {
                    var ceiling = Instantiate(floorObjects[0]);
                    ceiling.transform.position = pos + new Vector3(0, size, 0);
                    continue;
                }

                
                var floor = Instantiate(floorObjects[field-1]);
                floor.transform.position = pos;

                // instantiate wall object
                var wallOffsets = new int[][]{
                    new int[]{-1, 0},
                    new int[]{0, 1},
                    new int[]{1, 0},
                    new int[]{0, -1},
                };

                for (int w = 0; w < wallOffsets.Length; w ++) {
                    var walloffset = wallOffsets[w];
                    var wallfield = getField(floormatrix, r + walloffset[0], c + walloffset[1]);
                    if (wallfield == 0) {
                        var rotation = 90 * (w + 1);

                        var wallinstance = Instantiate(wallObjects[field-1]);
                        wallinstance.transform.position = pos + new Vector3(walloffset[0] * size / 2, size / 2, walloffset[1] * size / 2);
                        wallinstance.transform.eulerAngles = new Vector3(-90, rotation, 0);

                        var wallinstance2 = Instantiate(wallObjects[field-1]);
                        wallinstance2.transform.position = pos + new Vector3(walloffset[0] * size / 2, size / 2, walloffset[1] * size / 2);
                        wallinstance2.transform.eulerAngles = new Vector3(-90, rotation + 180, 0);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
