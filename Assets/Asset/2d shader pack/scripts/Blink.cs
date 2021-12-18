using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyGameStudio.Jeremy
{
    public class Blink : MonoBehaviour
    {
        [Header("material")]
        public Material material;

        [Header("speed of blink")]
        public float speed_blink;

        private float tint_amount = 1;

        // Update is called once per frame
        void Update()
        {
            if (this.tint_amount <= 0.01)
            {
                this.tint_amount = 1;
            }
            else
            {
                this.tint_amount = Mathf.Lerp(this.tint_amount, 0, Time.deltaTime * this.speed_blink);
                this.material.SetFloat("_tint_amount", this.tint_amount);
            }
        }
    }
}
