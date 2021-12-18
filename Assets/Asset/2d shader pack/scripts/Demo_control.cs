using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

namespace EasyGameStudio.Jeremy
{
    public class Demo_control : MonoBehaviour
    {
        public static Demo_control instance;

        public int index;
        public GameObject[] allens;

        [Header("title")]
        public Text text_title;
        public string[] titles;

        //public Volume m_volume;

        public AudioSource audio_source;
        public AudioClip ka;

        void Awake()
        {
            Demo_control.instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            this.change_to_index();
        }


        public void play_btn_sound()
        {
            this.audio_source.PlayOneShot(this.ka, 2f);
        }


        public void on_next_btn()
        {
            this.index++;
            if (this.index >= this.allens.Length)
                this.index = 0;

            this.change_to_index();
        }

        public void change_title(int index)
        {
            this.text_title.text = this.titles[this.index] + " " + index;
        }

        public void on_previous_btn()
        {
            this.index--;
            if (this.index < 0)
                this.index = this.allens.Length - 1;

            this.change_to_index();
        }

        private void change_to_index()
        {
            this.text_title.text = this.titles[this.index];

            for (int i = 0; i < this.allens.Length; i++)
            {
                if (this.index == i)
                {
                    this.allens[i].SetActive(true);
                }
                else
                {
                    this.allens[i].SetActive(false);
                }
            }

            this.audio_source.PlayOneShot(this.ka, 2f);
        }
    }
}