using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Target))]
public class AudioCue : MonoBehaviour
{
    [SerializeField] int _id;
    public int ID => _id;
    [SerializeField] string _name;
    public string Name => _name;

    private bool _hasTargeting;
    private AudioSource _audioSource;
    private Target _target;

    private void Awake()
    {
        _name = name;
        _target = GetComponent<Target>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _audioSource.Play();
        if (_hasTargeting)
        {
            _target.enabled = true;
        }
    }

    public void StopSound()
    {
        _audioSource.Stop();
        if (_hasTargeting)
        {
            _target.enabled = false;
        }
    }

    public void Clicked()
    {
        // Notify the manager that this AudioCue was clicked
        AudioExperimentManager.Instance.AudioCueClicked(this);
    }
}
