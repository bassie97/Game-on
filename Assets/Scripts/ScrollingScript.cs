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

    private SpriteRenderer firstChild;

    private SpriteRenderer middleChild;

    private SpriteRenderer lastChild;

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
            // Get the middle object.
            // The list is ordered from left (x position) to right.
            //middleChild = backgroundPart.FirstOrDefault();
            middleChild = backgroundPart.ElementAt<SpriteRenderer>(1);

            firstChild = backgroundPart.FirstOrDefault();

            if (middleChild != null)
            {
                //players going right
                // Check if the child is already (partly) before the camera.
                // We test the position first because the IsVisibleFrom
                // method is a bit heavier to execute.
                if (middleChild.transform.position.x < Camera.main.transform.position.x)
                {
                    // If the child is already on the left of the camera,
                    // we test if it's completely outside and needs to be
                    // recycled.
                    if (middleChild.IsVisibleFrom(Camera.main) == false)
                    {
                        // Get the last child position.
                        lastChild = backgroundPart.LastOrDefault();

                        Vector3 lastPosition = lastChild.transform.position;
                        //lastPosition += middleChild.transform.position;
                        Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

                        // Set the position of the recyled one to be AFTER
                        // the last child.
                        // Note: Only work for horizontal scrolling currently.
                        firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x, middleChild.transform.position.y, middleChild.transform.position.z);

                        // Set the recycled child to the last position
                        // of the backgroundPart list.
                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }

                lastChild = firstChild = backgroundPart.LastOrDefault();
                //players going left
                if (middleChild.transform.position.x > Camera.main.transform.position.x)
                {
                    // If the child is already on the left of the camera,
                    // we test if it's completely outside and needs to be
                    // recycled.
                    if (middleChild.IsVisibleFrom(Camera.main) == false)
                    {
                        // Get the first child position.
                        firstChild = backgroundPart.FirstOrDefault();

                        Vector3 lastPosition = firstChild.transform.position;
                        Vector3 lastSize = (firstChild.bounds.max - firstChild.bounds.min);

                        // Set the position of the recyled one to be AFTER
                        // the last child.
                        // Note: Only work for horizontal scrolling currently.
                        lastChild.transform.position = new Vector3(lastPosition.x - lastSize.x, lastChild.transform.position.y, lastChild.transform.position.z);

                        // Set the recycled child to the last position
                        // of the backgroundPart list.
                        backgroundPart.Remove(lastChild);
                        //backgroundPart.Add(lastChild);
                        backgroundPart.Insert(0, lastChild);
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
