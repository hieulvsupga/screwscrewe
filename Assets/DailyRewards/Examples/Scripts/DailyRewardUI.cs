
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

namespace NiobiumStudios
{
    public class DailyRewardUI : MonoBehaviour
    {
        [SerializeField]
        private RectTransform imageRewardrectTransform;

        public bool showRewardName;     
        [Header("UI Elements")]
        public Text textDay;                // Text containing the Day text eg. Day 12
        public Text textReward;             // The Text containing the Reward amount
        public Image imageRewardBackground; // The Reward Image Background
        public Image imageReward;           // The Reward Image
        public Color colorClaim;            // The Color of the background when claimed
        private Color colorUnclaimed;       // The Color of the background when not claimed

        public SpriteAtlas spriteAtlas;

        [Header("Internal")]
        public int day;

        [HideInInspector]
        public Reward reward;

       
        public DailyRewardState state;

        // The States a reward can have
        public enum DailyRewardState
        {
            UNCLAIMED_AVAILABLE,
            UNCLAIMED_UNAVAILABLE,
            CLAIMED
        }

        void Awake()
        {
           
            colorUnclaimed = imageReward.color;
        }



        public void Initialize()
        {
            textDay.text = $"Day {day}";
            //{day.ToString()}
            if (reward.reward > 0)
            {
                if (showRewardName)
                {
                    textReward.text = reward.reward + " " + reward.unit;
                }
                else
                {
                    textReward.text = reward.reward.ToString();
                }
            }
            else
            {
                textReward.text = reward.unit.ToString();
            }
            imageReward.sprite = reward.sprite;
        }

        // Refreshes the UI
        public void Refresh()
        {
            switch (state)
            {
                case DailyRewardState.UNCLAIMED_AVAILABLE:
                    if(day==7){
                        imageRewardBackground.color = colorClaim;
                        imageReward.enabled = false;
                        textReward.enabled = false;
                    }
                    else{
                        imageRewardBackground.sprite = spriteAtlas.GetSprite("cell_2"); ;
                        imageReward.enabled = true;
                        textReward.enabled = true;
                    }
                    break;
                case DailyRewardState.UNCLAIMED_UNAVAILABLE:
                    if(day==7){
                        imageRewardBackground.sprite = spriteAtlas.GetSprite("image 1"); ;                 
                        imageReward.enabled = false;
                        textReward.enabled = false;
                    }
                    else{
                        imageRewardBackground.sprite = spriteAtlas.GetSprite("cell_3"); ;
                        imageReward.enabled = true;
                        textReward.enabled = true;
                    }
                    break;
                case DailyRewardState.CLAIMED:
                    //if(day==7){
                        
                    //    imageRewardBackground.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    //    imageReward.enabled = false;
                    //    textReward.enabled = false;
                    //}
                    //else{
                    imageRewardBackground.sprite = spriteAtlas.GetSprite("cell_1");
                    imageReward.enabled = true;
                    imageReward.sprite = spriteAtlas.GetSprite("v_tick");
                    imageRewardrectTransform.anchoredPosition = new Vector2(0, 0);
                    textReward.enabled = false;
                    //}
                    break;
            }
        }
    }
}