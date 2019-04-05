using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DamageModel{

    //baseline: assume enemy has 10 health, kill in 3 seconds by normal attacks

    public static float DamageInflicted(string element, GameObject attacker, GameObject reciever) {
        float damage = 0;
        switch (element) {
            case "FireAttack":
                damage = 0.04f;
                KnockBack(attacker, reciever, 20f);
                break;
            case "WaterAttack":
                damage = 0.059f;
                KnockBack(attacker, reciever, 20f);
                break;
            case "WindAttack":
                damage = 0.125f;
                KnockBack(attacker, reciever, 20f);
                break;
            case "MudAttack":
                damage = 1;
                KnockBack(attacker, reciever, 20f);
                break;
            case "LavaAttack":
                damage = 0.5f;
                break;
            case "LightningAttack":
                damage = 0.3f;
                if (reciever.GetComponent<SmallEnemy>().IsWet()) {
                    reciever.GetComponent<SmallEnemy>().StopGetWet();
                    damage = 1;
                }
                break;
            case "SandStormAttack":
                reciever.GetComponent<SmallEnemy>().Stun();
                break;
            case "FogAttack":
                if (!reciever.GetComponent<SmallEnemy>().IsWet())
                {
                    reciever.GetComponent<SmallEnemy>().GetWet();
                }
                break;
            case "SteamAttack":
                damage = 1f;
                break;
            default:
                damage = 0;
                break;
        }
        return damage;
    }

    public static void KnockBack(GameObject attacker, GameObject reciever, float knockBack)
    {
        Vector3 knockBackDir = new Vector3((reciever.transform.position.x - attacker.transform.position.x), 0f, (reciever.transform.position.z - attacker.transform.position.z));
        reciever.GetComponent<Rigidbody>().AddForce(knockBackDir * knockBack);
    }

}
