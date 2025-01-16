using Flamingo.SceneLoader;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SceneLoaderInstaller", menuName = "Installers/SceneLoaderInstaller")]
public class SceneLoaderInstaller : ScriptableObjectInstaller<SceneLoaderInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ILoadSceneService>().To<SimpleLoadSceneService>().AsSingle().NonLazy();
    }
}