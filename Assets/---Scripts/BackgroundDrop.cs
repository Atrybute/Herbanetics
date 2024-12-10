using UnityEngine;

public class BackgroundDrop : MonoBehaviour
{
    [SerializeField] private float totalTimeToReachEndInMinutes = 1f;
    [SerializeField] private float endYPosition = -10f;

    private float totalTimeToReachEndInSeconds;
    private float scrollSpeed;

    private void Start()
    {
        totalTimeToReachEndInSeconds = totalTimeToReachEndInMinutes * 60f;

        float distance = transform.position.y - endYPosition;
        scrollSpeed = distance / totalTimeToReachEndInSeconds;
    }

    private void Update()
    {
        Vector3 newPosition = transform.position - Vector3.up * scrollSpeed * Time.deltaTime;
        transform.position = newPosition;

        if (newPosition.y <= endYPosition)
        {
            // Level is over, do something here
        }
    }
}

