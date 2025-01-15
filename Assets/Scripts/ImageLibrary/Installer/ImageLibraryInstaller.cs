using Flamingo.ImageProvider;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ImageLibraryInstaller", menuName = "Installers/ImageLibraryInstaller")]
public class ImageLibraryInstaller : ScriptableObjectInstaller<ImageLibraryInstaller>
{
    [SerializeField] private ImageLibrary imageLibrary;

    public override void InstallBindings()
    {
        Container.Bind<ImageLibraryProvider>().AsSingle().WithArguments(imageLibrary).NonLazy();
    }
}