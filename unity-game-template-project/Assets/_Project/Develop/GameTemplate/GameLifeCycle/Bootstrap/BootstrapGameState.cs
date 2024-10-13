using Cysharp.Threading.Tasks;
using GameTemplate.GameLifeCycle.Loading.States;
using GameTemplate.Services.Localization;
using GameTemplate.Services.Analytics;
using GameTemplate.Services.GameLevelLoader;
using GameTemplate.Infrastructure.StateMachineComponents.States;
using GameTemplate.UI.LoadingCurtain;
using GameTemplate.Infrastructure.StateMachineComponents;
using GameTemplate.Systems;
using GameTemplate.Systems.Performance;
using GameTemplate.Infrastructure.Signals;
using Modules.AssetsManagement.StaticData;
using Modules.AudioManagement.Mixer;
using Modules.Logging;
using Modules.SaveManagement.Interfaces;

namespace GameTemplate.GameLifeCycle.Bootstrap
{
    public class BootstrapGameState : GameState
    {
        private readonly IStaticDataService _staticDataService;
        private readonly LoadingCurtainProxy _loadingCurtainProxy;
        private readonly ILevelLoaderService _gameLevelLoaderService;
        private readonly IAudioMixerSystem _audioMixerSystem;
        private readonly ILocalizationService _localizationService;
        private readonly IAnalyticsService _analyticsService;
        private readonly SystemPerformanceSetter _performanceSetter;
        private readonly ISaveLoadSystem _saveLoadSystem;
        private readonly IDevicePerformaceConfigurator _devicePerformaceConfigurator;

        public BootstrapGameState(GameStateMachine stateMachine, IEventBus eventBus, ILogSystem logSystem, 
            IAnalyticsService analyticsService, IStaticDataService staticDataService, 
            LoadingCurtainProxy loadingCurtainProxy, ILevelLoaderService gameLevelLoaderService, 
            IAudioMixerSystem audioMixerSystem, IDevicePerformaceConfigurator devicePerformaceConfigurator,
            ILocalizationService localizationService, ISaveLoadSystem saveLoadSystem, 
            SystemPerformanceSetter performanceSetter)
            
            : base(stateMachine, eventBus, logSystem)
        {
            _staticDataService = staticDataService;
            _loadingCurtainProxy = loadingCurtainProxy;
            _gameLevelLoaderService = gameLevelLoaderService;
            _audioMixerSystem = audioMixerSystem;
            _localizationService = localizationService;
            _analyticsService = analyticsService;
            _performanceSetter = performanceSetter;
            _saveLoadSystem = saveLoadSystem;
            _devicePerformaceConfigurator = devicePerformaceConfigurator;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            await InitializeServices();
            await StateMachine.SwitchState<GameLoadingState>();
        }

        private async UniTask InitializeServices()
        {
            await _staticDataService.InitializeAsync();
            _devicePerformaceConfigurator.Initialize();
            _performanceSetter.Initialize();
            await _loadingCurtainProxy.InitializeAsync();
            await _saveLoadSystem.InitializeAsync();
            _gameLevelLoaderService.Initialize();
            _audioMixerSystem.Initialize();
            _localizationService.Initialize();
            _analyticsService.Initialize();
        }
    }
}