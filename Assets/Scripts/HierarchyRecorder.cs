using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class HierarchyRecorder : MonoBehaviour
{
    // The clip the recording is going to be saved to.
    public AnimationClip clip;

    // Checkbox to start/stop the recording.
    public bool record = false;

    // The main feature: the actual recorder.
    private GameObjectRecorder recorder;

    void Start()
    {
        recorder = new GameObjectRecorder(gameObject);
        // Set it up to record the transforms recursively.
        recorder.BindComponentsOfType<Transform>(gameObject, true);

        if (record)
        {
            // As long as "record" is on: take a snapshot.
            recorder.TakeSnapshot(Time.deltaTime);
        }
    }

    // The recording needs to be done in LateUpdate in order
    // to be done once everything has been updated
    // (animations, physics, scripts, etc.).
    void LateUpdate()
    {
        if (clip == null)
            return;

        if (record)
        {
            // As long as "record" is on: take a snapshot.
            recorder.TakeSnapshot(Time.deltaTime);
        }
        else if (recorder.isRecording)
        {
            // "record" is off, but we were recording:
            // save to clip and clear recording.
            recorder.SaveToClip(clip);
            recorder.ResetRecording();
        }
    }
}