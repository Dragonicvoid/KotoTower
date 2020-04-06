using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherMethod 
{
    // Shuffle a list by epoch times
    public static List<T> shuffle<T>(List<T> collection, int epoch)
    {
        List<T> finalList = new List<T>();
        for (int i = 0; i < epoch; i++)
        {
            finalList.Clear();

            int random = Random.Range(0, collection.Count);
            List<T> firstHalf = new List<T>(collection.GetRange(0, random));
            List<T> secondHalf = new List<T>(collection.GetRange(random, collection.Count - random));

            finalList.AddRange(secondHalf);
            finalList.AddRange(firstHalf);
        }

        return finalList;
    }

    // Find if spesific object is on the camera
    public static bool isVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
