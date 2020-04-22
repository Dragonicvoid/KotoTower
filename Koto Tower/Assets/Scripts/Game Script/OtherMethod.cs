using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class OtherMethod 
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

    // Serialize save file
    public static string serialize<T>(this T toSerialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringWriter writer = new StringWriter();
        xml.Serialize(writer, toSerialize);
        return writer.ToString();
    }

    public static T deserialize<T>(this string toDeserialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(toDeserialize);
        return (T)xml.Deserialize(reader);
    }

    public static bool onUiPressed(Vector3 position)
    {
        if (position.x < (float)Screen.width * ((float)22 / 100) && position.y > (float)Screen.height * ((float)57 / 100))
            return true;

        if (position.x > (float)Screen.width * ((float)79 / 100) && position.y > (float)Screen.height * ((float)90 / 100))
            return true;

        return false;
    }
}
