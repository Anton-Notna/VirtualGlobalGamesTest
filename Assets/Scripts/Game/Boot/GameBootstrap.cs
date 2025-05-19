using UnityEngine;
using Shared.Storage;
using Zenject;
using System.Collections.Generic;
using Game.Core;
using Game.Levels;
using Game.Player;
using Game.UI;
using Game.Ammo;
using Game.Interaction;
using Game.Enemies;
using Game.Items;

namespace Game.Boot
{
    public class GameBootstrap : MonoInstaller
    {
        [Header("Prefabs:")]
        [SerializeField]
        private PickableItem _pickablePrefab;
        [Header("Catalogs:")]
        [SerializeField]
        private LevelsCatalog _levels;
        [SerializeField]
        private EnemiesCatalog _enemies;
        [SerializeField]
        private ItemsCatalog _items;
        [Header("Windows:")]
        [SerializeField]
        private GameHUD _hud;
        [SerializeField]
        private WinWindow _win;
        [SerializeField]
        private LoseWindow _lose;
        [Header("SceneObjects:")]
        [SerializeField]
        private PlayerSetup _playerSetup;

        private AmmoInventory _ammoInventory;
        private ItemsInventory _itemsInventory;
        private DataSaveLoad _save;
        private GameScenario _scenario;

        public override void InstallBindings()
        {
            InstallFactories();

            Container.BindInstances(_levels, _enemies, _items);
            Container.Bind<IStorage>().To<PlayerPrefsJsonStorage>().AsSingle();
            Container.Bind<ILevelBuilder>().To<LevelBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<CursorBehaviour>().AsSingle();

            _ammoInventory = Container.Instantiate<AmmoInventory>();
            Container.BindInterfacesAndSelfTo<AmmoInventory>().FromInstance(_ammoInventory);

            _itemsInventory = Container.Instantiate<ItemsInventory>();
            Container.BindInterfacesAndSelfTo<ItemsInventory>().FromInstance(_itemsInventory);

            _save = Container.Instantiate<DataSaveLoad>();
            Container.BindInstance(_save);

            var pickups = new List<IObjectPickup>()
            {
                Container.Instantiate<AmmoPickup>(),
                Container.Instantiate<ItemPickup>(),
            };
            Container.Bind<IEnumerable<IObjectPickup>>().FromInstance(pickups);
            Container.BindInstances(_playerSetup, _hud, _win, _lose);
            Container.Bind<ILevelResultShowcase>().To<Windows>().AsSingle();

            _scenario = Container.Instantiate<GameScenario>();
            Container.BindInstance(_scenario);
        }

        public override async void Start()
        {
            _save.Register(Constants.StorageKeys.Ammo, _ammoInventory);
            _save.Register(Constants.StorageKeys.Inventory, _itemsInventory);

            _playerSetup.Init();
            await _scenario.ProcessGame(destroyCancellationToken);
        }

        private static T Instantiate<T>(DiContainer container, T prefab) where T : Component
        {
            var instance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            container.InjectGameObject(instance.gameObject);
            return instance;
        }

        private void OnDestroy()
        {
            _save?.UnregisterAll();
        }

        private void InstallFactories()
        {
            Container.BindFactoryCustomInterface<Level, Level, PlaceholderFactory<Level, Level>, IFactory<Level, Level>>().FromMethod(Instantiate);
            Container.BindFactoryCustomInterface<EnemySetup, EnemySetup, PlaceholderFactory<EnemySetup, EnemySetup>, IFactory<EnemySetup, EnemySetup>>().FromMethod(Instantiate);
            Container.BindFactoryCustomInterface<EquippedItem, EquippedItem, PlaceholderFactory<EquippedItem, EquippedItem>, IFactory<EquippedItem, EquippedItem>>().FromMethod(Instantiate);
            Container.BindFactoryCustomInterface<PickableItem, PlaceholderFactory<PickableItem>, IFactory<PickableItem>>().FromComponentInNewPrefab(_pickablePrefab);
        }
    }
}