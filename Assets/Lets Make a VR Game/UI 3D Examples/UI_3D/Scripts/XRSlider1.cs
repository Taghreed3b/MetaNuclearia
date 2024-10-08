using System;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;


namespace UnityEngine.XR.Content.Interaction
{
    /// <summary>
    /// An interactable that follows the position of the interactor on a single axis
    /// </summary>
    public class XRSlider1 : XRBaseInteractable
    {
        [Serializable]
        public class ValueChangeEvent : UnityEvent<float> { }

        [SerializeField]
        [Tooltip("The object that is visually grabbed and manipulated")]
        Transform m_Handle = null;

        [SerializeField]
        [Tooltip("The value of the slider")]
        [Range(0.0f, 1.0f)]
        float m_Value = 0.5f;

        [SerializeField]
        [Tooltip("The offset of the slider at value '1'")]
        float m_MaxPosition = 0.5f;

        [SerializeField]
        [Tooltip("The offset of the slider at value '0'")]
        float m_MinPosition = -0.5f;

        [SerializeField]
        [Tooltip("Events to trigger when the slider is moved")]
        ValueChangeEvent m_OnValueChange = new ValueChangeEvent();

        [SerializeField]
        [Tooltip("The object that will be used as the slider handle/marker")]
        private Transform m_SliderMarker = null; // مرجع للكائن المُستخدم كمؤشر


        [SerializeField]
        [Tooltip("The object that will be used as the second slider handle/marker")]
        private Transform m_SecondSliderMarker = null; // مرجع للمؤشر الثاني

        [SerializeField]
        [Tooltip("Speed of the second marker's follow movement")]
        private float m_FollowSpeed = 0.1f; // سرعة تتبع المؤشر الثاني

        // مرجع للكائن الذي سيتم تفعيله/إلغاء تفعيله

        //private Text scoreText; // مرجع لعنصر النص لعرض النقاط


       // private int score ; // متغير لتتبع النقاط

        IXRSelectInteractor m_Interactor;

        /// <summary>
        /// The value of the slider
        /// </summary>
        public float value
        {
            get => m_Value;
            set
            {
                SetValue(value);
                SetSliderPosition(value);
            }
        }

        /// <summary>
        /// Events to trigger when the slider is moved
        /// </summary>
        public ValueChangeEvent onValueChange => m_OnValueChange;

        void Start()
        {
            SetValue(m_Value);
            SetSliderPosition(m_Value);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            selectEntered.AddListener(StartGrab);
            selectExited.AddListener(EndGrab);
        }

        protected override void OnDisable()
        {
            selectEntered.RemoveListener(StartGrab);
            selectExited.RemoveListener(EndGrab);
            base.OnDisable();
        }

        void StartGrab(SelectEnterEventArgs args)
        {
            m_Interactor = args.interactorObject;
            UpdateSliderPosition();
        }

        void EndGrab(SelectExitEventArgs args)
        {
            m_Interactor = null;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (isSelected)
                {
                    UpdateSliderPosition();
                }
            }
        }

        void UpdateSliderPosition()
        {
            // Put anchor position into slider space
            var localPosition = transform.InverseTransformPoint(m_Interactor.GetAttachTransform(this).position);
            var sliderValue = Mathf.Clamp01((localPosition.z - m_MinPosition) / (m_MaxPosition - m_MinPosition));
            SetValue(sliderValue);
            SetSliderPosition(sliderValue);


        }

        void Update()
        {
            if (m_SecondSliderMarker == null)
                return;
            // حساب الدوران المعكوس المستهدف بناءً على قيمة السلايدر
            float inverseRotationAngle = m_Value * -180f; // الدوران المباشر
            inverseRotationAngle = 180f - inverseRotationAngle; // عكس الدوران

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, inverseRotationAngle);

            // تحديث دوران المؤشر الثاني بسرعة أقل
            m_SecondSliderMarker.localRotation = Quaternion.Lerp(m_SecondSliderMarker.localRotation, targetRotation, m_FollowSpeed * Time.deltaTime);

            if (m_SecondSliderMarker.localEulerAngles.z > 70f && m_SecondSliderMarker.localEulerAngles.z > 110f)
            {
               

            }


        }
        void SetSliderPosition(float value)
        {
            if (m_Handle == null)
                return;

            var handlePos = m_Handle.localPosition;
            handlePos.z = Mathf.Lerp(m_MinPosition, m_MaxPosition, value);
            m_Handle.localPosition = handlePos;

            if (m_SliderMarker == null)
                return;

            // حساب زاوية الدوران بناءً على قيمة السلايدر
            float rotationAngle = value * -180f; // تحويل قيمة السلايدر إلى زاوية دوران
            m_SliderMarker.localRotation = Quaternion.Euler(0f, 0f, rotationAngle); // تحديث الدوران
        }

        void SetValue(float value)
        {
            m_Value = value;
            m_OnValueChange.Invoke(m_Value);
           
        }

        void OnDrawGizmosSelected()
        {
            var sliderMinPoint = transform.TransformPoint(new Vector3(0.0f, 0.0f, m_MinPosition));
            var sliderMaxPoint = transform.TransformPoint(new Vector3(0.0f, 0.0f, m_MaxPosition));

            Gizmos.color = Color.green;
            Gizmos.DrawLine(sliderMinPoint, sliderMaxPoint);
        }

        void OnValidate()
        {
            SetSliderPosition(m_Value);
        }
    }
}
