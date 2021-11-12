using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCorrectPlace : MonoBehaviour
{

    public float duration = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public IEnumerator MoveChar(Transform target)
    {
        float timeElapsed = 0;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, target.position, timeElapsed / duration);
            transform.rotation = Quaternion.Lerp(startRotation, target.rotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = target.rotation;
        transform.position = target.position;
    }


}
