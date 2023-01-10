using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PullMeasurer : XRBaseInteractable
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [Header("Polish")]
    public LineRenderer stringLine;
    [ColorUsage(true, true)]
    public Color stringNormalCol, stringPulledCol;
    public ParticleSystem lineParticle;
    private InputDevice targetDevice;
    private InputDevice leftController;
    private InputDevice rightController;
    public float PullAmount { get; private set; } = 0.0f;

    public Vector3 PullPosition => Vector3.Lerp(start.position, end.position, PullAmount);

    private XRBaseInteractor pullingInteractor = null;

    private void Update()
    {
        if (!leftController.isValid && !rightController.isValid)
        {
            getController();
        }
    }

    private void getController()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        foreach (var item in devices)
        {
            if (item.name == "Oculus Touch Controller - Left")
            {
                leftController = item;
            }
            else if (item.name == "Oculus Touch Controller - Right")
            {
                rightController = item;
            }
        }
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        PullAmount = 0;

        stringLine.material.SetColor("_EmissionColor", stringNormalCol);
        lineParticle.Play();
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        pullingInteractor = interactor;
        Debug.Log(interactor.name);

        HapticManager.Impulse(1f, 5f, rightController);

    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (isSelected)
        {
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
                UpdatePull();

            stringLine.material.SetColor("_EmissionColor",
                    Color.Lerp(stringNormalCol, stringPulledCol, PullAmount));
            HapticManager.Impulse(0.5f, 0.05f, rightController);

        }
    }

    private void UpdatePull()
    {
        Vector3 interactorPosition = firstInteractorSelecting.transform.position;

        PullAmount = CalculatePull(interactorPosition);
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;

        float maxLength = targetDirection.magnitude;
        targetDirection.Normalize();

        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        return Mathf.Clamp(pullValue, 0.0f, 1.0f);
    }

    private void OnDrawGizmos()
    {
        if (start && end)
            Gizmos.DrawLine(start.position, end.position);
    }
}
