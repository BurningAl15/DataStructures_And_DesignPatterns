using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
   public LineRenderer line;

   public void DrawRectangle(Vector2 position, Vector2 size)
   {
      line.positionCount = 5;
      List<Vector3> positions= new List<Vector3>();
      positions.Add(new Vector2(position.x-size.x/2,position.y-size.y/2));
      positions.Add(new Vector2(position.x+size.x/2,position.y-size.y/2));
      positions.Add(new Vector2(position.x+size.x/2,position.y+size.y/2));
      positions.Add(new Vector2(position.x-size.x/2,position.y+size.y/2));
      positions.Add(new Vector2(position.x-size.x/2,position.y-size.y/2));
      line.SetPositions(positions.ToArray());
   }
}
