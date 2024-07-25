using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip shopBell;
    public AudioClip UIButton;
    public AudioClip collectSound;
    public AudioClip coinSound;
    public AudioClip collectSoundAlt;
    public AudioClip fishDamage;
    public AudioClip strangle;


   public void PlayShopBell(){
    AudioSource.PlayClipAtPoint(shopBell, Camera.main.transform.position);
   }
   public void PlayUIButton(){
    AudioSource.PlayClipAtPoint(UIButton, Camera.main.transform.position);
   }
   public void PlayCollectSound(){
    AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position);
   }
   public void PlayCoinSound(){
    AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position);
   }
   public void PlayCollectSoundAlt(){
    AudioSource.PlayClipAtPoint(collectSoundAlt, Camera.main.transform.position);
   }
   public void PlayFishDamage(){
    AudioSource.PlayClipAtPoint(fishDamage, Camera.main.transform.position);
   }
   public void PlayStrangle(){
    AudioSource.PlayClipAtPoint(strangle, Camera.main.transform.position);
   }
   
}
