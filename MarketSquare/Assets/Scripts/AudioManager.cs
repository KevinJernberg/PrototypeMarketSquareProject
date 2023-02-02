using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FMOD.Studio;
using FMODUnity;


public enum AmbianceLocation
{
    Outdoor,
    InsideSmall,
    InsideLarge,
    Boss,
}



public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class AmbianceAudio
    {
        public StudioEventEmitter ambianceEmitter;

        public void ChangeAmbiance(AmbianceLocation location)
        {
            switch (location)
            {
                case AmbianceLocation.Outdoor:
                    ambianceEmitter.SetParameter("Location", 0);
                    Debug.Log("I am outside!");
                    break;
                case AmbianceLocation.InsideSmall:
                    ambianceEmitter.SetParameter("Location", 1);
                    Debug.Log("I am inside a small area");
                    break;
                case AmbianceLocation.InsideLarge:
                    ambianceEmitter.SetParameter("Location", 2);
                    Debug.Log("I am inside a large area");
                    break;
                case AmbianceLocation.Boss:
                    ambianceEmitter.SetParameter("Location", 3);
                    Debug.Log("This is it...the boss");
                    break;

            }
        }
    }

    [Serializable]
    public class InteractablesAudio
    {
        [EventRef]
        public string doorOpenEvent;

        [EventRef]
        public string pickupHealthEvent;

        [EventRef]
        public string pressurePadEvent;

        [EventRef]
        public string destructableBoxEvent;

        [EventRef]
        public string switchesEvent;

        [EventRef]
        public string acidSplashEvent;


        public void AcidSplashAudio (GameObject acidSplashObject)
        {
            RuntimeManager.PlayOneShotAttached(acidSplashEvent, acidSplashObject);
        }

        public void DoorOpenAudio(GameObject doorObject)
        {
            RuntimeManager.PlayOneShotAttached(doorOpenEvent, doorObject);
        }

        public void PickUpHealthAudio(GameObject pickUpHealthObject)
        {
            RuntimeManager.PlayOneShotAttached(pickupHealthEvent, pickUpHealthObject);
        }

        public void PressurePadAudio(GameObject pressurePadObject)
        {
            RuntimeManager.PlayOneShotAttached(pressurePadEvent, pressurePadObject);
        }

        public void DestructableBoxAudio(GameObject destructableBoxObject)
        {
            RuntimeManager.PlayOneShotAttached(destructableBoxEvent, destructableBoxObject);
        }

        public void SwitchesAudio(GameObject switchesObject)
        {
            RuntimeManager.PlayOneShotAttached(switchesEvent, switchesObject);
        }

    }

    [Serializable]
    public class PlayerAudio
    {
        [EventRef]
        public string playerFootstepEvent;
        private EventInstance playerFootstepInstance;

        [EventRef]
        public string playerLandingEvent;
        private EventInstance playerLandingInstance;

        [EventRef]
        public string playerMeleeEvent;
        private EventInstance playerMeleeInstance;

        [EventRef]
        public string playerDamageEvent;
        private EventInstance playerDamageInstance;

        [EventRef]
        public string playerJumpEvent;
        private EventInstance playerJumpInstance;

        [EventRef]
        public string playerRespawnEvent;
        private EventInstance playerRespawnInstance;


        public void PlayerRespawnAudio (GameObject respawnObject)
        {
            playerRespawnInstance = RuntimeManager.CreateInstance(playerRespawnEvent);

            RuntimeManager.AttachInstanceToGameObject(playerRespawnInstance, respawnObject.transform, respawnObject.GetComponent<Rigidbody>());

            playerRespawnInstance.start();

            playerRespawnInstance.release();
        }


        public void PlayerJumpAudio (GameObject jumpObject)
        {
            playerJumpInstance = RuntimeManager.CreateInstance(playerJumpEvent);

            RuntimeManager.AttachInstanceToGameObject(playerJumpInstance, jumpObject.transform, jumpObject.GetComponent<Rigidbody>());

            playerJumpInstance.start();

            playerJumpInstance.release();
        }


        public void PlayerDamageAudio (GameObject damageObject)
        {
            playerDamageInstance = RuntimeManager.CreateInstance(playerDamageEvent);

            RuntimeManager.AttachInstanceToGameObject(playerDamageInstance, damageObject.transform, damageObject.GetComponent<Rigidbody>());

            playerDamageInstance.start();

            playerDamageInstance.release();
        }

        public void PlayerMeleeAudio (GameObject meleeObject)
        {
            playerMeleeInstance = RuntimeManager.CreateInstance(playerMeleeEvent);

            RuntimeManager.AttachInstanceToGameObject(playerMeleeInstance, meleeObject.transform, meleeObject.GetComponent<Rigidbody>());

            playerMeleeInstance.start();

            playerMeleeInstance.release();
        }

        public void PlayerLandingAudio(GameObject landingObject)
        {
            playerLandingInstance = RuntimeManager.CreateInstance(playerLandingEvent);

            RuntimeManager.AttachInstanceToGameObject(playerLandingInstance, landingObject.transform, landingObject.GetComponent<Rigidbody>());

            playerLandingInstance.start();

            playerLandingInstance.release();
        }


        public void PlayerFootstepAudio(GameObject footstepObject, string surface)
        {
            //RuntimeManager.PlayOneShotAttached(playerFootstepEvent, footstepObject);

            //Skapar vi en FMOD event instans med referens till EventRef-Variabel
            playerFootstepInstance = RuntimeManager.CreateInstance(playerFootstepEvent);
            //Fäster vår FMOD-Event instans till ett spelobjekt med referens till dess Transofrm- och Rididbody-komponent
            RuntimeManager.AttachInstanceToGameObject(playerFootstepInstance, footstepObject.transform, footstepObject.GetComponent<Rigidbody>());
            //Startar/Spelar upp vårt event
            switch (surface)
            {
                case "Concrete":
                    playerFootstepInstance.setParameterByName("Surface", 0f);
                    break;
                case "Gravel":
                    playerFootstepInstance.setParameterByName("Surface", 1f);
                    break;
                case "Water":
                    playerFootstepInstance.setParameterByName("Surface", 2f);
                    break; 
            }
              
            playerFootstepInstance.start();
            //"Slänger" eventinstansen
            playerFootstepInstance.release();
        }

    }

    [Serializable]
    public class EnemyAudio
    {
        [EventRef]
        public string spitterAttackEvent;
        private EventInstance spitterAttackInstance;

        [EventRef]
        public string spitterShootEvent;
        private EventInstance spitterShootInstance;

        [EventRef]
        public string chomperAttackEvent;
        private EventInstance chomperAttackInstance;

        [EventRef]
        public string enemyDeathEvent;
        private EventInstance enemyDeathInstance;

        [EventRef]
        public string enemyFootstepEvent;
        private EventInstance enemyFootstepInstance;

        public void SpitterAttackAudio(GameObject attackObject)
        {
            spitterAttackInstance = RuntimeManager.CreateInstance(spitterAttackEvent);

            RuntimeManager.AttachInstanceToGameObject(spitterAttackInstance, attackObject.transform, attackObject.GetComponent<Rigidbody>());

            spitterAttackInstance.start();

            spitterAttackInstance.release();
        }

        public void SpitterShootAudio(GameObject shootObject)
        {
            spitterShootInstance = RuntimeManager.CreateInstance(spitterShootEvent);

            RuntimeManager.AttachInstanceToGameObject(spitterShootInstance, shootObject.transform, shootObject.GetComponent<Rigidbody>());

            spitterShootInstance.start();

            spitterShootInstance.release();
        }

        public void ChomperAttackAudio(GameObject attackObject)
        {
            chomperAttackInstance = RuntimeManager.CreateInstance(chomperAttackEvent);

            RuntimeManager.AttachInstanceToGameObject(chomperAttackInstance, attackObject.transform, attackObject.GetComponent<Rigidbody>());

            chomperAttackInstance.start();

            chomperAttackInstance.release();
        }

        public void EnemyDeathAudio(GameObject deathObject)
        {
            enemyDeathInstance = RuntimeManager.CreateInstance(enemyDeathEvent);

            RuntimeManager.AttachInstanceToGameObject(enemyDeathInstance, deathObject.transform, deathObject.GetComponent<Rigidbody>());

            enemyDeathInstance.start();

            enemyDeathInstance.release();
        }
        
        public void EnemyFootstepAudio(GameObject footstepObject)
        {
            enemyFootstepInstance = RuntimeManager.CreateInstance(enemyFootstepEvent);

            RuntimeManager.AttachInstanceToGameObject(enemyFootstepInstance, footstepObject.transform, footstepObject.GetComponent<Rigidbody>());

            enemyFootstepInstance.start();

            enemyFootstepInstance.release();
        }
            
    }

    [Serializable]
    public class BossAudio
    {
        [EventRef]
        public string bossFootstepEvent;
        private EventInstance bossFootstepInstance;

        [EventRef]
        public string bossLaserEvent;
        private EventInstance bossLaserInstance;

        [EventRef]
        public string bossNadeEvent;
        private EventInstance bossNadeInstance;

        [EventRef]
        public string bossPunchEvent;
        private EventInstance bossPunchInstance;

        [EventRef]
        public string bossDamageEvent;
        private EventInstance bossDamageInstance;

        [EventRef]
        public string bossDeathEvent;
        private EventInstance bossDeathInstance;

        public void BossFootstepAudio(GameObject footstepObject)
        {
            bossFootstepInstance = RuntimeManager.CreateInstance(bossFootstepEvent);

            RuntimeManager.AttachInstanceToGameObject(bossFootstepInstance, footstepObject.transform, footstepObject.GetComponent<Rigidbody>());

            bossFootstepInstance.start();

            bossFootstepInstance.release();
        }

        public void BossLaserAudio(GameObject laserObject)
        {
            bossLaserInstance = RuntimeManager.CreateInstance(bossLaserEvent);

            RuntimeManager.AttachInstanceToGameObject(bossLaserInstance, laserObject.transform, laserObject.GetComponent<Rigidbody>());

            bossLaserInstance.start();

            bossLaserInstance.release();
        }
        
        public void BossNadeAudio(GameObject nadeObject)
        {
            bossNadeInstance = RuntimeManager.CreateInstance(bossNadeEvent);

            RuntimeManager.AttachInstanceToGameObject(bossNadeInstance, nadeObject.transform, nadeObject.GetComponent<Rigidbody>());

            bossNadeInstance.start();

            bossNadeInstance.release();
        }

        public void BossPunchAudio(GameObject punchObject)
        {
            bossPunchInstance = RuntimeManager.CreateInstance(bossPunchEvent);

            RuntimeManager.AttachInstanceToGameObject(bossPunchInstance, punchObject.transform, punchObject.GetComponent<Rigidbody>());

            bossPunchInstance.start();

            bossPunchInstance.release();
        }

        public void BossDamageAudio(GameObject damageObject)
        {
            bossDamageInstance = RuntimeManager.CreateInstance(bossDamageEvent);

            RuntimeManager.AttachInstanceToGameObject(bossDamageInstance, damageObject.transform, damageObject.GetComponent<Rigidbody>());

            bossDamageInstance.start();

            bossDamageInstance.release();
        }

        public void BossDeathAudio(GameObject deathObject)
        {
            bossDeathInstance = RuntimeManager.CreateInstance(bossDeathEvent);

            RuntimeManager.AttachInstanceToGameObject(bossDeathInstance, deathObject.transform, deathObject.GetComponent<Rigidbody>());

            bossDeathInstance.start();

            bossDeathInstance.release();
        }
    }



public EnemyAudio enemyAudio;
public AmbianceAudio ambianceAudio;
public InteractablesAudio interactablesAudio;
public PlayerAudio playerAudio;
public BossAudio bossAudio;

}