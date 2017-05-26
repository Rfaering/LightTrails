using ProgressBar.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ProgressBar
{
    /// <summary>
    /// This Script is directed at linearly progressing designs.
    /// </summary>
    public class ProgressBarBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Rect from the panel that will act as Filler.
        /// </summary>
        [SerializeField]
        private RectTransform m_FillRect;
        /// <summary>
        /// Class used for storing the Min and Max width values that the Filler will vary between.
        /// </summary>
        private FillerProperty m_FillerInfo;

        public FillerProperty FillerInfo
        {
            get
            {
                if (m_FillerInfo == null)
                    m_FillerInfo = new FillerProperty(0, m_FillRect.rect.width);
                return m_FillerInfo;
            }
        }

        /// <summary>
        /// The Progress Value Class stores the current Filler state as a fraction of its maximal value (normalized).
        /// This is raw Data that you don't need to be using. See the Value property.
        /// </summary>
        private ProgressValue m_Value;

        private float _value;
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                var floatValue = value;
                if (floatValue < 0)
                {
                    floatValue = 0;
                }
                if (floatValue > 100)
                {
                    floatValue = 100;
                }
                _value = floatValue;

                SetFillerSizeAsPercentage(floatValue);
            }
        }
        /// <summary>
        /// This is the core of the Filler animation. If there is a difference between m_Value and TransitoryValue,
        /// the latter will play catch up in the Update Method.
        /// </summary>
        /// <remarks>
        /// Keep in mind that this means that you get the actual Filler value from the property Value only,
        /// If the animation is playing, TransitoryValue will be different until it catches up.
        /// </remarks>
        public float TransitoryValue { get; set; }

        /// <summary>
        /// If a Text component is set here it will be updated with the ProgressBar value (percentage).
        /// Otherwise no Error will be raised.
        /// </summary>
        [SerializeField]
        private Text m_AttachedText;

        /// <summary>
        /// By default the Filler is centered horizontally in its container panel.
        /// This value is needed for the SetInsetAndSizeFromParentEdge method.
        /// </summary>
        private float m_XOffset = float.NaN;

        public float XOffset
        {
            get
            {
                if (float.IsNaN(m_XOffset))
                    m_XOffset = (transform.GetComponent<RectTransform>().rect.width - m_FillRect.rect.width) / 2;

                return m_XOffset;
            }
        }

        void OnEnable()
        {
            //We set the Filler size to zero at the start.
            SetFillerSize(0);
            //We initialize m_Value
            m_Value = new ProgressValue(0, FillerInfo.MaxWidth);
        }


        /// <summary>
        /// This method is used to set the Filler's width
        /// </summary>
        /// <param name="Width">the new Filler's width</param>
        public void SetFillerSize(float Width)
        {
            if (m_AttachedText)
                m_AttachedText.text = Mathf.Round(Width / FillerInfo.MaxWidth * 100).ToString() + " %";

            m_FillRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, XOffset, Width);
        }

        /// <summary>
        /// This method is used to set the Filler's width with a percentage.
        /// </summary>
        /// <param name="Percent">this method needs a percentage as parameter</param>
        public void SetFillerSizeAsPercentage(float Percent)
        {
            m_Value.Set(FillerInfo.MaxWidth * Percent / 100);
            SetFillerSize(m_Value.AsFloat);
        }
    }
}