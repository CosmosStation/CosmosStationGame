using System.Collections;
using UnityEngine;
using RootMotion.FinalIK;

public class LegControl : MonoBehaviour
{
    public FABRIK[] fabrikComponents;
    public Transform[] ikTargets;
    public Transform[] ikControls;
    public float[] stepDistanceForLeg;
    public bool[] isLegMoving;
    public float stepDistance = 1f;
    public float stepRange = 0.25f;
    public float stepHeight = 0.5f;
    public float stepDuration = 0.5f;
    public Vector3 walkingDirection = Vector3.forward;
    public Animator animator;
    public float animationSpeedMultiplier = 1f;
    
    private int currentLeg = 0;
    
    void Update()
    {
        float distance = Random.Range(stepDistanceForLeg[currentLeg] - stepRange, stepDistanceForLeg[currentLeg] + stepRange);
        if ((Vector3.Distance(ikControls[currentLeg].position, ikTargets[currentLeg].position) > distance) && !isLegMoving[currentLeg])
        {
            isLegMoving[currentLeg] = true;
            StartCoroutine(TakeStep(currentLeg));
            currentLeg = (currentLeg + 1) % ikControls.Length;
        }
    } 
    
    private IEnumerator TakeStep(int legIndex)
    {
        Vector3 startPosition = ikControls[legIndex].position;
        Vector3 targetPosition = ikTargets[currentLeg].position;
        float startTime = Time.time;
    
        while (Time.time < startTime + stepDuration)
        {
            float progress = (Time.time - startTime) / stepDuration;
            float yOffset = Mathf.Sin(progress * Mathf.PI) * stepHeight;
            ikControls[legIndex].position = Vector3.Lerp(startPosition, targetPosition, progress) + Vector3.up * yOffset;
            yield return null;
        }
        
        isLegMoving[currentLeg] = false;
        ikControls[legIndex].position = targetPosition;
    }
    
}
