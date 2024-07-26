using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}

    private void Awake() {
        if (Instance != null)
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public AudioClip shopBell;
    public AudioClip UIButton;
    public AudioClip collectSound;
    public AudioClip coinSound;
    public AudioClip collectSoundAlt;
    public AudioClip fishDamage;
    public AudioClip strangle;


   public void PlayShopBell(){
    AudioSource.PlayClipAtPoint(shopBell, FindObjectOfType<Camera>().transform.position);
   }
   public void PlayUIButton(){
    AudioSource.PlayClipAtPoint(UIButton, FindObjectOfType<Camera>().transform.position);
   }
   public void PlayCollectSound(Vector3 pos){
    AudioSource.PlayClipAtPoint(collectSound, pos);
   }
   public void PlayCoinSound(){
    AudioSource.PlayClipAtPoint(coinSound, FindObjectOfType<Camera>().transform.position);
   }
   public void PlayCollectSoundAlt(Vector3 pos){
    AudioSource.PlayClipAtPoint(collectSoundAlt, pos);
   }
   public void PlayFishDamage(Vector3 pos){
    AudioSource.PlayClipAtPoint(fishDamage, pos);
   }
   public void PlayStrangle(Vector3 pos){
    AudioSource.PlayClipAtPoint(strangle, pos);
   }
   
}
