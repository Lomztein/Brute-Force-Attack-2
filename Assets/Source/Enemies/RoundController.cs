using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lomztein.BFA2.Enemies
{
    public class RoundController : MonoBehaviour, IRoundController
    {
        public enum RoundState { Ready, Preparing, InProgress }

        public SimpleWave[] Waves;
        public int CurrentWave;
        public RoundState State;

        public Vector2 SpawnAreaSize;
        public Vector2 SpawnAreaCenter;

        private IWaveCollection _waveCollection = new WaveCollection();
        private IResourceContainer _resourceContainer;

        private void Awake()
        {
            _waveCollection.Init(Waves);
            _resourceContainer = GetComponent<IResourceContainer>();
        }

        public void InvokeDelayed(Action callback, float time)
        {
            StartCoroutine(InvokeDelayedInternal(callback, time));
        }

        private IEnumerator InvokeDelayedInternal(Action callback, float time)
        {
            yield return new WaitForSeconds(time);
            callback();
        }

        private void Update()
        {
            if (State == RoundState.Ready && Input.GetButtonDown("StartWave"))
            {
                StartCoroutine (RunNextWave());
            }
        }

        private IEnumerator RunNextWave()
        {
            CurrentWave++;
            yield return PrepareWave();
            StartWave(CurrentWave);
        }

        private IEnumerator PrepareWave()
        {
            State = RoundState.InProgress;
            yield return null;
        }

        private void StartWave(int wave)
        {
            State = RoundState.InProgress;
            IWave next = _waveCollection.GetWave(wave);
            next.OnFinished += OnWaveFinished;
            next.OnSpawn += OnSpawn;
            next.Start(this);
        }

        private void OnSpawn(IEnemy obj)
        {
            obj.SetPosition(GetRandomPosition());
        }

        private Vector2 GetRandomPosition()
        {
            return new Vector2(
                SpawnAreaCenter.x + Random.Range(-SpawnAreaSize.x, SpawnAreaSize.x),
                SpawnAreaCenter.y + Random.Range(-SpawnAreaSize.y, SpawnAreaSize.y)
                );
        }

        private void OnWaveFinished()
        {
            EndWave(CurrentWave);
        }

        private void EndWave(int wave)
        {
            IWave ended = _waveCollection.GetWave(wave);
            ended.OnFinished -= OnWaveFinished;
            ended.OnSpawn -= OnSpawn;
            State = RoundState.Ready;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(SpawnAreaCenter, SpawnAreaSize * 2f);
        }

        public void OnEnemyDeath(IEnemy enemy)
        {
            _resourceContainer.ChangeResource(Resource.Credits, enemy.Value);
        }
    }
}
