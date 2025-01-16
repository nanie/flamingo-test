using Flamingo.Localization;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LocalizationInstaller", menuName = "Installers/LocalizationInstaller")]
public class LocalizationInstaller : ScriptableObjectInstaller<LocalizationInstaller>
{
    [SerializeField] private LocalizationData[] _localizedTexts;
    public override void InstallBindings()
    {
        Container.Bind<ILocalizationService>().To<LocalizationService>().AsSingle().WithArguments(_localizedTexts).NonLazy();
    }
}