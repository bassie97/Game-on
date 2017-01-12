using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour {

    private Transform cam;

    public bool isLooping;

    private Vector3 newPos;

    private int xOffset = 20;

    private Vector3 initPos;

    private List<SpriteRenderer> backgroundPart;

    [SerializeField]
    private float layerOffset = 1;

    // Use this for initialization
    void Start () {
        initPos = this.transform.position;

        if (isLooping)
        {
            backgroundPart = new List<SpriteRenderer>();

            for(int i = 0;  i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                SpriteRenderer r = child.GetComponent<SpriteRenderer>();

                if(r != null)
                {
                    backgroundPart.Add(r);
                }
            }

            // Sort by position.
            backgroundPart = backgroundPart.OrderBy(
              t => t.transform.position.x
            ).ToList();
        }
	}
	
	// Update is called once per frame
	void Update () {

        newPos = new Vector3(Camera.main.gameObject.transform.position.x * layerOffset, initPos.y, 0);
        this.transform.position = newPos;

        if (isLooping)
        {
            // Get the first object.
            // The list is ordered from left (x position) to right.
            SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

            if (firstChild != null)
            {
                // Check if the child is already (partly) before the camera.
                // We test the position first because the IsVisibleFrom
                // method is a bit heavier to execute.
                if (firstChild.transform.position.x < Camera.main.transform.position.x)
                {
                    // If the child is already on the left of the camera,
                    // we test if it's completely outside and needs to be
                    // recycled.
                    if (firstChild.IsVisibleFrom(Camera.main) == false)
                    {
                        // Get the last child position.
                        SpriteRenderer lastChild = backgroundPart.LastOrDefault();

                        Vector3 lastPosition = lastChild.transform.position;
                        Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

                        // Set the position of the recyled one to be AFTER
                        // the last child.
                        // Note: Only work for horizontal scrolling currently.
                        firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x, firstChild.transform.position.y, firstChild.transform.position.z);

                        // Set the recycled child to the last position
                        // of the backgroundPart list.
                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }
        }
        
        /*cam = Camera.main.gameObject.transform;
        Sprite bg = GetComponent<SpriteRenderer>().sprite;

        //if(((this.transform.position).x - (bg.bounds.min).x) < (Camera.main.transform.position).x + xOffset + //rightborder)


        newPos = new Vector3(cam.position.x * layerOffset, initPos.y, 0);
        this.transform.position = newPos;*/
        
    }
}
