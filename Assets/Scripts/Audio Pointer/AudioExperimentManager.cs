using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class AudioExperimentManager : MonoBehaviour
{
    public static AudioExperimentManager Instance;
    private List<AudioCue> _audioCues = new List<AudioCue>();
    private float _timer;
    private bool _isExperimentRunning;
    private int _experimentCount;
    private List<TestResult> _testResults = new List<TestResult>();
    private AudioCue _correctAudioCue; // Variable to store the correct AudioCue for the current experiment
    [SerializeField] private bool _hasTargeting =false;

    public bool HasTargeting => _hasTargeting;

    
    [SerializeField] private float _maxTime =15;



    // Add a class to represent test results
    public class TestResult
    {
        public float Time;
        public int ID;
        public string Name;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        // Set the seed for randomization
        

        // Get all objects with the tag "Speakers"
        GameObject[] speakers = GameObject.FindGameObjectsWithTag("Speakers");

        // Add all AudioCue objects from the speakers to the list of audio cues
        foreach (GameObject speaker in speakers)
        {
            if (speaker.GetComponent<AudioSource>() != null)
            {
                _audioCues.Add(speaker.GetComponent<AudioSource>().GetComponent<AudioCue>());
            }
        }
    }

    public void StartExperiment()
    {
        if (!_isExperimentRunning)
        {
            _experimentCount = 0;
            _timer = 0;
            _isExperimentRunning = true;

            // Randomly choose an AudioCue as the correct one
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, _audioCues.Count);
            } while (_audioCues[randomIndex] == _correctAudioCue);
            _correctAudioCue = _audioCues[randomIndex];

            // Play the sound of the correct AudioCue
            _correctAudioCue.PlaySound();
        }
        else
        {
            _timer = 0;
            

            // Randomly choose an AudioCue as the correct one
            int randomIndex = Random.Range(0, _audioCues.Count);
            _correctAudioCue = _audioCues[randomIndex];

            // Play the sound of the correct AudioCue
            _correctAudioCue.PlaySound();
        }
    }

    public void Update()
    {
        if (_isExperimentRunning)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            return;
        }

        if(_timer >= _maxTime)
        {
            Debug.Log("Time Up");
            // Log the test result
            TestResult result = new TestResult
            {
                Time = -1f,
                ID = _correctAudioCue.ID,
                Name = _correctAudioCue.Name
            };
            _testResults.Add(result);

            _experimentCount++;

            // Stop the sound
            _correctAudioCue.StopSound();

            // Schedule the next experiment after a 3-second delay
            Invoke("StartNextExperiment", 2f);
            _timer = 0;
        }

    }

    // Call this method when an AudioCue is clicked
    public void AudioCueClicked(AudioCue clickedCue)
    {

        if (_isExperimentRunning)
        {


            // Check if the clicked AudioCue is the correct one
            if (clickedCue == _correctAudioCue)
            {
                Debug.Log("Clicked the correct one");
                // Log the test result
                TestResult result = new TestResult
                {
                    Time = _timer,
                    ID = clickedCue.ID,
                    Name = clickedCue.Name
                };
                _testResults.Add(result);

                _experimentCount++;

                // Stop the sound
                _correctAudioCue.StopSound();

                
                Invoke("StartNextExperiment", 2f);
            }
            else
            {
                Debug.Log("Clicked the Incorrect one");
            }






        }
    }

    private void StartNextExperiment()
    {

        if (_experimentCount >= 10)
        {
            Debug.Log("Finished");           // Experiment completed, log the results to CSV
            LogResultsToCSV();
            _isExperimentRunning = false;
        }
        else
        {
            Debug.Log("Starting next one    ");
            // Start the next experiment
            StartExperiment();
        }
    }

    private void LogResultsToCSV()
    {
        string filePath = "ExperimentResults.csv";
        if (HasTargeting)
        {
            filePath = "ExperimentResultsWithTargeting.csv";
        }
        else
        {
            filePath = "ExperimentResultsWithoutTargeting.csv";
        }





        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write header
            writer.WriteLine("Time; Spearker ID; World Direction; Type Of Hearing; Seed; ParticipantID");

            // Write results
            foreach (TestResult result in _testResults)
            {
                writer.WriteLine($"{result.Time}; {result.ID}; {result.Name}; {Manager.Instance.GetTypeOfHearing()}; " +
                    $"{Manager.Instance.RandomSeed}; {SystemInfo.deviceUniqueIdentifier}");
            }
        }




        
        Manager.Instance.EnableEndWindow();
    }
}
