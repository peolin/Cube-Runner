using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    public GameObject[] tracks;
    public Transform currentTrack;

    public void AddNewTracks(Transform player)
    {
        if (player.position.z >= currentTrack.position.z + 10f)
        {
            GameObject newTrack = Instantiate(tracks[UnityEngine.Random.Range(0, 4)], new Vector3(0f, 0f, currentTrack.position.z + 30f), Quaternion.identity);

            currentTrack = newTrack.transform;
        }
    }
}
