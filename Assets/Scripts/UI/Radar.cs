using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class Radar : MonoBehaviour
{
    [SerializeField] private Texture blip;
    [SerializeField] private Texture radarBG;
    [SerializeField] private Transform centerObject;
    [SerializeField] private float mapScale = 0.8f;
    //[SerializeField] private RectTransform mapCenter;
    private Vector2 mapCenter = new Vector2(50, 50);
    [SerializeField] private string tagFilter = "Reef";
    [SerializeField] private float maxDist = 200;

    private Image testImage;

    void OnGUI()
    {
        //	GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, Vector3(Screen.width / 600.0, Screen.height / 450.0, 1));

        // Draw player blip (centerObject)
        //		float bX=centerObject.transform.position.x * mapScale;
        //	    float bY=centerObject.transform.position.z * mapScale;


        GUI.DrawTexture(new Rect(mapCenter.x - 32, mapCenter.y - 32, 200, 200), radarBG);
        DrawBlipsFor(tagFilter);

    }

    private void DrawBlipsFor(string tagName)
    {

        // Find all game objects with tag 
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tagName);

        // Iterate through them
        foreach (GameObject go in gos)
        {
            drawBlip(go, blip);
        }
    }

    private void drawBlip(GameObject go, Texture aTexture)
    {
        Vector3 centerPos = centerObject.position;
        Vector3 extPos = go.transform.position;

        // first we need to get the distance of the enemy from the player
        float dist = Vector3.Distance(centerPos, extPos);

        float dx = centerPos.x - extPos.x; // how far to the side of the player is the enemy?
        float dy = centerPos.y - extPos.y; // how far in front or behind the player is the enemy?

        // what's the angle to turn to face the enemy - compensating for the player's turning?
        float deltay = Mathf.Atan2(dx, dy) * Mathf.Rad2Deg - 270 - centerObject.eulerAngles.z;

        // just basic trigonometry to find the point x,y (enemy's location) given the angle deltay
        float bX = dist * Mathf.Cos(deltay * Mathf.Deg2Rad);
        float bY = dist * Mathf.Sin(deltay * Mathf.Deg2Rad);

        bX = bX * mapScale; // scales down the x-coordinate by half so that the plot stays within our radar
        bY = bY * mapScale; // scales down the y-coordinate by half so that the plot stays within our radar

        if (dist <= maxDist)
        {
            // this is the diameter of our largest radar circle
            GUI.DrawTexture(new Rect(mapCenter.x + bX, mapCenter.y + bY, 20, 20), aTexture);
        }
    }
}