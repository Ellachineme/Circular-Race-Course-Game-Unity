using UnityEngine;

/// <summary>
/// Follow an object's coordinates without adapting it's rotation
/// </summary>
public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    private Vector3 _offset;
    
    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - objectToFollow.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.position + _offset;
    }
}
