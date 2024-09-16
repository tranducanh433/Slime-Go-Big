using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePositionManager : MonoBehaviour
{
    [SerializeField] int minX = -18;
    [SerializeField] int maxX = 18;
    [SerializeField] float atY = -0.5f;

    static List<SpikePosition> positionList = new List<SpikePosition>();


    void Start()
    {
        int currentX = minX;

        while (currentX <= maxX)
        {
            positionList.Add(new SpikePosition(new Vector2 (currentX, atY)));
            currentX += 2;
        }
    }

    public static Vector2 GetPosition(GameObject spike)
    {
        List<SpikePosition> emptySpot = new List<SpikePosition>();
        for (int i = 0; i < positionList.Count; i++)
        {
            if (positionList[i].spike == null)
                emptySpot.Add(positionList[i]);
        }

        if (emptySpot.Count == 0)
            return new Vector2(-999, -999);

        SpikePosition selectPos = emptySpot[Random.Range(0, emptySpot.Count)];
        selectPos.spike = spike;
        return selectPos.pos;
    }


    class SpikePosition
    {
        public Vector2 pos;
        public GameObject spike;

        public SpikePosition(Vector2 pos)
        {
            this.pos = pos;
        }
    }
}
