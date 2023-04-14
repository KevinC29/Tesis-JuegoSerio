using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;


    public class ControladorVideo : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private GameObject btn_play;
        [SerializeField] private GameObject btn_pause;
        [SerializeField] private GameObject btn_reset;
        public bool play;

        void Start()
        {
            videoPlayer.Play();
            videoPlayer.Pause();
        }
        public void PlayVideo()
        {
            videoPlayer.Play();
            play = true;
        }
        public void PauseVideo()
        {
            videoPlayer.Pause();
            play = false;
            
        }
        public void ResetVideo()
        {
            videoPlayer.frame = 0;
            PlayVideo();
        }
    }


