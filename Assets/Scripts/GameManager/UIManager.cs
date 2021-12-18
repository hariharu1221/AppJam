using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Player P1;
    [SerializeField] Player P2;

    [SerializeField] InGameUIClass P1UI;
    [SerializeField] InGameUIClass P2UI;

    [SerializeField] GameObject P1UIOb;
    [SerializeField] GameObject P2UIOb;

    private void Awake()
    {
        if (P1 == null) P1 = GameObject.Find("P1").GetComponent<Player>();
        if (P2 == null) P2 = GameObject.Find("P2").GetComponent<Player>();

        if (P1UIOb == null) P1UIOb = GameObject.Find("P1UI");
        if (P2UIOb == null) P2UIOb = GameObject.Find("P2UI");

        PlayerUISet(P1, P1UIOb, 1);
        PlayerUISet(P2, P2UIOb, 2);
    }

    private void Update()
    {
        PlayerUI(P1, P1UI);
        PlayerUI(P2, P2UI);
    }

    void PlayerUISet(Player P, GameObject Ob, int p)
    {
        InGameUIClass UI = new InGameUIClass();
        UI.HpBar = Ob.transform.Find("Frame").GetChild(0).GetComponent<Image>();
        UI.SkillOne = Ob.transform.Find("SkillOne").GetChild(0).GetComponent<Image>();
        UI.SkillTwo = Ob.transform.Find("SkillTwo").GetChild(0).GetComponent<Image>();
        UI.SkillThree = Ob.transform.Find("SkillThree").GetChild(0).GetComponent<Image>();
        if (p == 1) P1UI = UI;
        if (p == 2) P2UI = UI;
    }

    void PlayerUI(Player P, InGameUIClass UI)
    {
        UI.HpBar.fillAmount = P.GetStatus.Hp / P.GetStatus.Maxhp;
        UI.SkillOne.fillAmount = P.SkillOneKey.NowCool / P.SkillOneKey.getcool;
        UI.SkillTwo.fillAmount = P.SkillTwoKey.NowCool / P.SkillTwoKey.getcool;
        UI.SkillThree.fillAmount = P.SkillThrKey.NowCool / P.SkillThrKey.getcool;
    }
}

[System.Serializable]
public class InGameUIClass
{
    public Image HpBar;
    public Image SkillOne;
    public Image SkillTwo;
    public Image SkillThree;
}
