using UnityEngine;

public class StreakManager : MonoBehaviour
{
    [SerializeField] BreathUI breathUI;
    
    public static StreakManager instance; //SINGLETON instance

    private void Awake()
    {
        //SINGLETON pattern initialization
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    
    public void NotifyEndOfSequence(bool WasSuccess)
    {
        breathUI.UpdateStreak(WasSuccess);
    }
}
