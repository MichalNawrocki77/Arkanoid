using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource ballHitsBlockSFX;
    [SerializeField] AudioSource ballHitsPlatformSFX;
    
    //public void W��czMuzykeWTle()
    //{
    //    muzykaWTle.Play();
    //}
    //public void Wy��czMuzykeWTle()
    //{
    //    muzykaWTle.Stop();
    //}
    public void PlayBallHitsBlockSFX()
    {
        //ballHitsBlockSFX.Play();
    }
    public void PlayBallHitsPlatformSFX()
    {
        //ballHitsPlatformSFX.Play();
    }
}
