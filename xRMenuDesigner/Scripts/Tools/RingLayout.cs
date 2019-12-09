using UnityEngine;
using UnityEngine.UI;

public class RingLayout : LayoutGroup
{
    private GameObject target = null;

    public float Distance;
    [Range(0f, 360f)]
    public float StartAngle = 0.0f;

    private float MinAngle, MaxAngle;

    protected override void OnEnable()
    {
        base.OnEnable();
        CalculateRadial();
    }
    public override void SetLayoutHorizontal()
    {
    }
    public override void SetLayoutVertical()
    {
    }
    public override void CalculateLayoutInputVertical()
    {
        CalculateRadial();
    }
    public override void CalculateLayoutInputHorizontal()
    {
        CalculateRadial();
    }
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        CalculateRadial();
    }
#endif
    public void CalculateRadial()
    {
        m_Tracker.Clear();
        if (transform.childCount == 0)
            return;
        MaxAngle = 360.0f;
        MinAngle = 0.0f;
        float fOffsetAngle = ((MaxAngle - MinAngle)) / (transform.childCount);

        target = transform.parent.gameObject;

        float fAngle = StartAngle;
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform child = (RectTransform)transform.GetChild(i);
            if (child != null && child.tag == "RingButton")
            {
                //child.LookAt(target.transform);
                //child.Rotate(0, 180, 0);
                m_Tracker.Add(this, child, DrivenTransformProperties.Anchors | DrivenTransformProperties.AnchoredPosition | DrivenTransformProperties.Pivot);
                Vector3 vPos = new Vector3(Mathf.Cos(fAngle * Mathf.Deg2Rad), Mathf.Sin(fAngle * Mathf.Deg2Rad), 0);
                child.localPosition = vPos * Distance;
                child.anchorMin = child.anchorMax = child.pivot = new Vector2(0.5f, 0.5f);
                fAngle += fOffsetAngle;
            }
        }
    }

    void Update()
    {
        // make buttons always look at ring center
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform child = (RectTransform)transform.GetChild(i);
            if (child != null && child.tag == "RingButton")
            {
                child.LookAt(target.transform);
                child.Rotate(0, 180, 0);
            }
        }
    }

}
