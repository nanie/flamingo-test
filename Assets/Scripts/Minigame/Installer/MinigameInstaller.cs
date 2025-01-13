using Flamingo.GameLoop.Signals;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MinigameInstaller", menuName = "Installers/MinigameInstaller")]
public class MinigameInstaller : ScriptableObjectInstaller<MinigameInstaller>
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<MinigameRequestedSignal>();
        Container.DeclareSignal<MinigameCompletedSignal>();
        
    }
}