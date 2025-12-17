using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Bridge : MonoBehaviour
{
    [SerializeField] List<Transform> platforms = new List<Transform>();
    [SerializeField] List<Vector3> positions = new List<Vector3>();
    [SerializeField] float dropDistance = -7f;

    [SerializeField] float moveTime = .2f;
    [SerializeField] float desync = .1f;

    [Header("Battle Triggers")]
    [SerializeField] WaveManager[] waveManagers;



    private void Start()
    {
        foreach (Transform child in transform)
        {
            platforms.Add(child);
            positions.Add(child.localPosition);
        }

        //StartCoroutine(MovePlatforms());

        foreach (WaveManager waveManager in waveManagers)
        {
            waveManager.OnBattleStarted.AddListener(() => {
                MovePlatforms(true);
            });

            waveManager.OnBattleEnded.AddListener(() => {
                MovePlatforms(false);
            });
        }
    }

    public void MovePlatforms(bool isDropping)
    {
        StartCoroutine(MovePlatformsRoutine(isDropping));
    }

    public IEnumerator MovePlatformsRoutine(bool isDropping)
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            Vector3 startPos = positions[i];
            Vector3 targetPos = startPos;
            targetPos.y = dropDistance * -1f;
            

            Transform platform = platforms[i];

            if (isDropping)
            {
                StartCoroutine(ProjectileCurveRoutine(platform, startPos, targetPos));
            }
            else
            {
                StartCoroutine(ProjectileCurveRoutine(platform, targetPos, startPos));
            }
            yield return new WaitForSeconds(desync);
        }

        
    }

    IEnumerator ProjectileCurveRoutine(Transform platform, Vector3 startPos, Vector3 endPos)
    {
        float timePassed = 0f;

        while (timePassed < moveTime) 
        { 
            // Increase time passed by Time.deltaTime;
            timePassed += Time.deltaTime;
        // Make a float for the linear time using timePassed and travelTime;
            float linearT = timePassed / moveTime;
            // Lerp the transform from the start point to the end point using linear time
            platform.localPosition = Vector3.Lerp(startPos, endPos, linearT);
            Debug.Log(endPos);

            yield return null;
        }
        // Feel free to destroy the object after it reaches the end point
    }
}
