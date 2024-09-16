using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UPGRADE_TYPE
{
    SLIME_UPGRADE,
    POWER_UP,
    FRIEND,
    SKILL,
}


[CreateAssetMenu(fileName = "Upgrade Name", menuName = "My Game/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    [SerializeField] string m_upgradeName;
    [SerializeField] Sprite m_sprite;
    [SerializeField] int m_upgradeLV = 1;
    [SerializeField] UPGRADE_TYPE m_upgradeType;
    [SerializeField, TextArea] string m_description;
    [SerializeField, TextArea] string m_baseDescription;
    [SerializeField] UpgradeData m_nextUpgrade;
    [SerializeField] GameObject m_spawnObj;

    public string upgradeName { get { return m_upgradeName; } }
    public Sprite sprite { get { return m_sprite; } }
    public int upgradeLevel { get { return m_upgradeLV; } }
    public UPGRADE_TYPE upgradeType { get { return m_upgradeType; } }
    public string description { get { return m_description;} }
    public string baseDescription { get { return m_baseDescription; } }
    public UpgradeData nextUpgrade { get { return m_nextUpgrade; } }
    public GameObject spawnObj { get { return m_spawnObj; } }
}
