using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
    [SerializeField] private float speed = 2000.0f;

    [Header("Particles")]
    public ParticleSystem trailParticle;
    public TrailRenderer trailRenderer;

    private new Rigidbody rigidbody;
    private ArrowCaster caster;

    private bool launched = false;

    private RaycastHit hit;

    protected override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody>();
        caster = GetComponent<ArrowCaster>();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (args.interactorObject is Notch notch)
        {
            if (notch.CanRelease)
                LaunchArrow(notch);
        }

        ArrowFly();
    }

    private void LaunchArrow(Notch notch)
    {
        launched = true;
        ApplyForce(notch.PullMeasurer);
        StartCoroutine(LaunchRoutine());
    }

    private void ApplyForce(PullMeasurer pullMeasurer)
    {
        rigidbody.AddForce(transform.forward * (pullMeasurer.PullAmount * speed));
    }

    private IEnumerator LaunchRoutine()
    {
        while (!caster.CheckForCollision(out hit))
        {
            SetDirection();
            yield return null;
        }
        DisablePhysics();
        ChildArrow(hit);
        CheckForHittable(hit);
    }

    private void SetDirection()
    {
        if (rigidbody.velocity.z > 0.5f)
            transform.forward = rigidbody.velocity;
    }

    private void DisablePhysics()
    {
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
        Destroy(this);
    }

    private void ArrowFly()
    {
        trailParticle.Play();
        trailRenderer.emitting = true;
        Debug.Log("flyyy");
    }

    private void ChildArrow(RaycastHit hit)
    {
        transform.SetParent(hit.transform);
    }

    private void CheckForHittable(RaycastHit hit)
    {
        /*if (hit.transform.TryGetComponent(out IArrowHittable hittable))
            hittable.Hit(this);
        if (hit.transform.TryGetComponent(out Agent agent))
            agent.TurnLover();*/
        // The collision function is commented out because the collider is being used.
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && !launched;
    }
}
