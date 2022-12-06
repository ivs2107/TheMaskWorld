using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDisplay : MonoBehaviour
{
    public Hero hero;
    public TextMesh textHp;
    public TextMesh textMana;
    public string textHeroName;

    private string textHpMax;
    private string textManaMax;

    public SpriteRenderer spriteRenderer;

    public GameObject spriteFillHp;
    public GameObject spriteFillMana;

    private float numberToTranslate = 4.4f;

    public GameObject DamageTakenUI;

    public GameObject ParticleDamageTaken;

    public GameObject ParticleDeath;

    public bool IsInEvolutionState = false;

    //initialise display on client
    public void InitDisplay()
    {
        textHpMax = hero.health;
        textManaMax = hero.mana;
        spriteRenderer.sprite = hero.artwork;
    }

    

    //display stats
    public void displayHero()
    {
        //special level robotthemask
       if(this.textHeroName == "RobotTheMask")
        {
            this.textHeroName = "RobotTheMaskUP";
            textHp.text = "1/1";
            textMana.text = "1/1";
            return;
        }

       if(GameManager.instance.mapManager.isRobotTheMaskEvolve == true)
        {
            textHp.text = "?/?";
            textMana.text = "?/?";
            spriteFillHp.transform.localPosition = new Vector3(spriteFillHp.transform.localPosition.x, numberToTranslate, spriteFillHp.transform.localPosition.z);
            spriteFillMana.transform.localPosition = new Vector3(spriteFillMana.transform.localPosition.x, -numberToTranslate, spriteFillMana.transform.localPosition.z);
            return;
        }
        float y = -numberToTranslate + numberToTranslate * Mathf.Clamp(float.Parse(hero.health) / float.Parse(textHpMax), 0f, 1f); 
        spriteFillHp.transform.localPosition = new Vector3(spriteFillHp.transform.localPosition.x, y, spriteFillHp.transform.localPosition.z);

        y = numberToTranslate -numberToTranslate * Mathf.Clamp(float.Parse(hero.mana) / float.Parse(textManaMax), 0f, 1f);
        spriteFillMana.transform.localPosition = new Vector3(spriteFillMana.transform.localPosition.x, y, spriteFillMana.transform.localPosition.z);

        textHp.text = hero.health + "/" + textHpMax;
        textMana.text = hero.mana + "/" + textManaMax;
    }
    //display animation of damage taken
    public void AnimationDamageTaken(int damageDealed)
    {
        DamageTakenUI.GetComponentInChildren<TextMesh>().text = damageDealed.ToString();
        DamageTakenUI.SetActive(true);
        StartCoroutine(CloseDamageTaken());
    }
    //sstop displaying damage taken
    public IEnumerator CloseDamageTaken()
    {
        yield return new WaitForSeconds(1f);
        DamageTakenUI.SetActive(false);

    }
    //display particle effect
    public void EffectDamageTaken()
    {
        ParticleDamageTaken.GetComponent<ParticleSystem>().Play();
    }

    //display particle death
    public void EffectDeath()
    {
        ParticleDeath.GetComponent<ParticleSystem>().Play();
    }
    //stop evolution
    public void EndOfEvolution()
    {
        IsInEvolutionState = false;
    }
}