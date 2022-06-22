using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPatterns : MonoBehaviour
{
    public List<int[,]> patterns;

    private void Awake()
    {
        patterns = new List<int[,]>();
        //pattern 1
        patterns.Add(new int[5, 5] {
            { 1,1,1,1,1},
            { 1,1,1,1,1},
            { 1,1,1,1,1},
            { 1,1,1,1,1},
            { 1,1,1,1,1}
        });

        patterns.Add(new int[5, 5] {
            { 2,1,2,1,2},
            { 1,2,3,2,1},
            { 2,3,3,3,2},
            { 1,2,3,2,1},
            { 2,1,2,1,2}
        });

        patterns.Add(new int[5, 5] {
            { 1,1,3,1,1},
            { 3,3,2,3,3},
            { 1,1,1,1,1},
            { 1,3,2,3,1},
            { 3,1,2,1,3}
        });

        patterns.Add(new int[5, 5] {
            { 1,1,3,2,1},
            { 2,3,1,2,1},
            { 2,3,2,3,2},
            { 1,2,1,3,2},
            { 1,2,3,1,1}
        });

        patterns.Add(new int[5, 5] {
            { 1,1,3,2,1},
            { 2,3,1,2,1},
            { 2,3,2,3,2},
            { 1,2,1,3,2},
            { 1,2,3,1,1}
        });

        patterns.Add(new int[5, 5]{
            { 3,2,3,2,3},
            { 2,1,3,1,2},
            { 1,1,3,1,1},
            { 3,2,1,2,3},
            { 2,3,1,3,2}
        });

        patterns.Add(new int[5, 5]{
            { 2,2,3,1,1},
            { 2,3,1,3,2},
            { 2,1,3,2,1},
            { 1,2,2,1,3},
            { 2,2,3,1,1}
        });


        patterns.Add(new int[5, 5]{
            { 3,3,1,3,3},
            { 3,2,1,2,3},
            { 2,2,1,2,2},
            { 3,1,2,1,3},
            { 1,2,3,2,1}
        });


        patterns.Add(new int[5, 5]{
            { 3,1,2,1,3},
            { 1,3,3,3,1},
            { 2,3,1,3,2},
            { 2,3,3,3,2},
            { 1,2,1,2,1}
        });
    }
}
