﻿using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    #region Singlton

    public static StatsUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of StatsUI found!");
            return;
        }

        instance = this;
    }

    #endregion

    [SerializeField] GameObject statsUI;
    [SerializeField] StatItem damageStat;
    [SerializeField] StatItem armorStat;
    [SerializeField] StatItem moveSpeedStat;

    [SerializeField] Text levelText;
    [SerializeField] Text statPointsText;

    private StatsManager manager;
    private int curDamage, curArmor, curMoveSpeed;

    int curLevel, curStatPoints;
    float curExp, nextLevelExp;

    void Start()
    {
        statsUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Stats"))
        {
            statsUI.SetActive(!statsUI.activeSelf);
        }

        if (manager != null)
        {
            CheckManagerChanges();
        }
    }

    public void SetManager(StatsManager statsManager)
    {
        manager = statsManager;
        CheckManagerChanges();
    }

    private void CheckManagerChanges()
    {
        if (curDamage != manager.damage)
        {
            curDamage = manager.damage;
            damageStat.ChangeStat(curDamage);
        }

        if (curArmor != manager.armor)
        {
            curArmor = manager.armor;
            armorStat.ChangeStat(curArmor);
        }

        if (curMoveSpeed != manager.moveSpeed)
        {
            curMoveSpeed = manager.moveSpeed;
            moveSpeedStat.ChangeStat(curMoveSpeed);
        }

        if (curLevel != manager.level)
        {
            curLevel = manager.level;
            levelText.text = curLevel.ToString();
        }

        if (curExp != manager.exp)
        {
            curExp = manager.exp;
        }

        if (nextLevelExp != manager.nextLevelExp)
        {
            nextLevelExp = manager.nextLevelExp;
        }

        if (curStatPoints != manager.statPoints)
        {
            curStatPoints = manager.statPoints;
            statPointsText.text = curStatPoints.ToString();
            if (curStatPoints > 0) SetUpgradableStats(true);
            else SetUpgradableStats(false);
        }
    }

    public void UpgradeStat(StatItem stat)
    {
        if (stat == damageStat)
            manager.CmdUpgradeStat((int) StatType.Damage);
        else if (stat == armorStat)
            manager.CmdUpgradeStat((int) StatType.Armor);
        else if (stat == moveSpeedStat)
            manager.CmdUpgradeStat((int) StatType.MoveSpeed);
    }

    private void SetUpgradableStats(bool active)
    {
        damageStat.SetUpgradable(active);
        armorStat.SetUpgradable(active);
        moveSpeedStat.SetUpgradable(active);
    }
}