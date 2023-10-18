using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private Rifle currentWeapon;
    private SpriteRenderer spriteRenderer;

    List<GameObject> ammoDisplayed = new List<GameObject>();
    List<GameObject> ammoEmptyDisplayed = new List<GameObject>();
    public List<GameObject> rechargesDisplayed = new List<GameObject>();
    private float topOffset = 50;
    private float leftOffset = 125;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        for(int i = 0; i < currentWeapon.GetMaxAmmoLevelMax(); i ++){
            GameObject go = Instantiate(currentWeapon.ammoSpriteForUI, transform.parent);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(leftOffset + i*(go.GetComponent<RectTransform>().sizeDelta.x + 5), - topOffset);
            ammoDisplayed.Add(go);
        }
        for(int i = 0; i < currentWeapon.GetMaxAmmoLevelMax(); i ++){
            GameObject go = Instantiate(currentWeapon.ammoEmptySpriteForUI, transform.parent);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(leftOffset + i*(go.GetComponent<RectTransform>().sizeDelta.x + 5), - topOffset);
            ammoEmptyDisplayed.Add(go);
            go.SetActive(false);
        }

        float currentDistanceY = topOffset + (currentWeapon.ammoSpriteForUI.GetComponent<RectTransform>().sizeDelta.y) + 15;

        for (int i = 0; i < currentWeapon.maxRecharges; i ++){
            GameObject go = Instantiate(currentWeapon.rechargeSpriteForUI, transform.parent);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(leftOffset + i*(go.GetComponent<RectTransform>().sizeDelta.x + 5), - currentDistanceY);
            rechargesDisplayed.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        for(i = 0; i < currentWeapon.GetMaxAmmo(); i ++){
            if(i < currentWeapon.currentAmmo){
                ammoDisplayed[i].SetActive(true);
                ammoEmptyDisplayed[i].SetActive(false);
            }
            else{
                ammoDisplayed[i].SetActive(false);
                ammoEmptyDisplayed[i].SetActive(true);
            }
        }
        for(; i < currentWeapon.GetMaxAmmoLevelMax(); i++)
        {
            ammoDisplayed[i].SetActive(false);
            ammoEmptyDisplayed[i].SetActive(false);
        }
        for(i = 0; i < currentWeapon.maxRecharges; i ++){
            if(i < currentWeapon.currentRecharges){
                rechargesDisplayed[i].SetActive(true);
            }
            else{
                rechargesDisplayed[i].SetActive(false);
            }
        }
    }
}
