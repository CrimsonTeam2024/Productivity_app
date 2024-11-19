using UnityEngine;

public class Task : MonoBehaviour {
    public TaskData taskData;

    public void Delete()
    {
        Destroy(gameObject);
    }
}