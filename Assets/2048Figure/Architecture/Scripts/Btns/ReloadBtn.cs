using _2048Figure.Architecture;
using _2048Figure.Architecture.ServiceLocator;
using GamePush;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadBtn : MonoBehaviour
{
    public void Reload()
    {
        ServiceLocator.current.UnregisterAll();
        Pools.figures.Clear();
        Pools.bomb = null;
        ResetData();
        SceneManager.LoadScene("BootstrapScene");
    }

    private void ResetData()
    {
        GP_Player.Set("localscore", null);
        GP_Player.Set("figurestransforms", null);
        GP_Player.Set("currentfigure", null);
        GP_Player.Set("activefigure", null);
        GP_Player.Set("currentorder", null);
        GP_Player.Set("maxsize", null);
        GP_Player.Sync();
        GP_Player.Sync(true);
    }
}
