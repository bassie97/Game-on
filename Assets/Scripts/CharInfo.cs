using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class CharInfo : MonoBehaviour {

        private Platformer2DUserControl UserControl1;
        //private Platformer2DUserControl UserControl2;
        private PlatformerControl2 UserControl2;

        public Sprite[] HeartSprites;
        public Image HeartUI1 = null;
        public Image HeartUI2 = null;

        [SerializeField]
        private Text NickName;

        [SerializeField]
        private Text ammoCount1 = null;
        
        [SerializeField]
        private Text ammoCount2 = null;

        // Use this for initialization
        void Start() {
            UserControl1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Platformer2DUserControl>();
            //UserControl2 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Platformer2DUserControl>();

            UserControl2 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlatformerControl2>();

            if (GameController.Instance.NickName != null)
            {
                NickName.text = GameController.Instance.NickName;
            }
            else
            {
                NickName.text = "No name found";
            }


        }

        // Update is called once per frame
        void Update() {
            CheckAmmoCount();
            CheckCharHealth();
        }

        private void CheckCharHealth()
        {
            if(GameController.Instance.AmountOfPlayers == 1)
            {
                if (UserControl1.playerOne != null)
                {
                    HeartUI1.sprite = HeartSprites[UserControl1.playerOne.curHealth - 1];
                }
            }
            else
            {
                if(UserControl1.playerOne != null)
                {
                    HeartUI1.sprite = HeartSprites[UserControl1.playerOne.curHealth - 1];
                }

                if(UserControl2.playerTwo != null)
                {
                    HeartUI2.sprite = HeartSprites[UserControl2.playerTwo.curHealth - 1];
                }
            }
        }

        private void CheckAmmoCount()
        {
            if (GameController.Instance.AmountOfPlayers == 1)
            {
                if (UserControl1.playerOne != null)
                {
                    ammoCount1.text = UserControl1.playerOne.ammoCount.ToString();
                }
            }
            else
            {
                if (UserControl1.playerOne != null)
                {
                    ammoCount1.text = UserControl1.playerOne.ammoCount.ToString();
                }
                if (UserControl2.playerTwo != null)
                {
                    ammoCount2.text = UserControl2.playerTwo.ammoCount.ToString();
                }
            }
        }
    }
}
