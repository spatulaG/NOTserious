using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class status : MonoBehaviour {
    static int[] slots = {0,0,0};
    static //white:0 R:1   Y:2   B:3                                                 
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int n = 0; n < 3; n++)
        {
            switch (slots[n]) {
                case 1:
                        break;
                case 2:
                    
                        break;
                case 3:
                        break;
                case 0:
                        break;

            }

        }
    }

    public static void absorbColor(int color) {
        for (int n = 0; n < 3; n++) {
            if (slots[n] == 0) {
                slots[n] = color;
            }
        }
    }
}
