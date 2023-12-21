using System.Collections;
using UnityEngine;

public class Delay : MonoBehaviour
{
    

    public void StartDelay()
    {
        // Start the experiment after a delay of 3 seconds

        
        StartCoroutine(DelayedStartExperiment());
    }

    private IEnumerator DelayedStartExperiment()
    {
        yield return new WaitForSeconds(3f);
        AudioExperimentManager.Instance.StartExperiment();
    }
}