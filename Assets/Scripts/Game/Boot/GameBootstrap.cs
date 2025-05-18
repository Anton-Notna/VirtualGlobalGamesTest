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

            Container.Bind<IStorage>().To<PlayerPrefsJsonStorage>().AsSingle();

            Container.BindInstance(_levels);
            Container.BindInstance(_enemies);
            Container.BindInstance(_items);
            Container.Bind<ILevelBuilder>().To<LevelBuilder>().AsSingle();

            _ammoInventory = Container.Instantiate<AmmoInventory>();
            Container.BindInterfacesAndSelfTo<AmmoInventory>().FromInstance(_ammoInventory);

            _itemsInventory = Container.Instantiate<ItemsInventory>();
            Container.BindInterfacesAndSelfTo<ItemsInventory>().FromInstance(_itemsInventory);

            _save = Container.Instantiate<DataSaveLoad>();
            Container.BindInstance(_save);

            Container.Bind<IEnumerable<IObjectPickup>>().FromInstance(new List<IObjectPickup>()
            {
                Container.Instantiate<AmmoPickup>(),
                Container.Instantiate<ItemPickup>(),
            });

            Container.BindInstance(_playerSetup);
            Container.BindInstance(_hud);

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
            Container.BindFactory<Level, Level, PlaceholderFactory<Level, Level>>().FromMethod(Instantiate);
            Container.BindFactory<EquippedItem, EquippedItem, PlaceholderFactory<EquippedItem, EquippedItem>>().FromMethod(Instantiate);
            Container.BindFactory<PickableItem, PlaceholderFactory<PickableItem>>().FromComponentInNewPrefab(_pickablePrefab);
        }
    }
}