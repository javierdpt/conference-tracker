using ConferenceTracker.Model;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConferenceTracker.Data.Services
{
    public abstract class BaseDataService<TEntity> where TEntity : BaseIdEntity
    {
        protected readonly IReliableStateManager StateManager;

        protected BaseDataService(IReliableStateManager stateManager)
        {
            StateManager = stateManager;
        }

        public async Task<List<TEntity>> GetAll(int skip, int take)
        {
            using var tx = StateManager.CreateTransaction();
            var collection = await GetCollection();

            var resp = new List<TEntity>();
            var enumerator = (await collection.CreateEnumerableAsync(tx)).GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync(CancellationToken.None))
            {
                if (skip-- > 0) continue;
                if (take-- == 0) break;
                resp.Add(enumerator.Current.Value);
            }

            return resp;
        }

        public async Task<TEntity> Get(Guid id)
        {
            using var tx = StateManager.CreateTransaction();
            var collection = await GetCollection();

            var enumerator = (await collection.CreateEnumerableAsync(tx)).GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync(CancellationToken.None))
            {
                if (enumerator.Current.Key == id)
                {
                    return enumerator.Current.Value;
                }
            }

            return null;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            using var tx = StateManager.CreateTransaction();
            var store = await GetCollection();
            entity.Id = new Guid();

            var res = await store.AddOrUpdateAsync(tx, entity.Id, entity, (k, o) => entity);
            await tx.CommitAsync();
            return res;
        }

        public async Task<TEntity> Update(Guid id, TEntity entity)
        {
            using var tx = StateManager.CreateTransaction();
            var store = await GetCollection();
            var resp = await store.AddOrUpdateAsync(tx, id, entity, ((k, o) => entity));
            await tx.CommitAsync();

            return resp;
        }

        public async Task<TEntity> Remove(Guid id)
        {
            using var tx = StateManager.CreateTransaction();
            var store = await GetCollection();
            if (await store.ContainsKeyAsync(tx, id))
            {
                var res = await store.TryRemoveAsync(tx, id);
                await tx.CommitAsync();
                return res.Value;
            }

            return null;
        }

        public async Task<long> Count()
        {
            var document = await GetCollection();
            using var tx = StateManager.CreateTransaction();
            return await document.GetCountAsync(tx);
        }

        public async Task Seed(int count)
        {
            if (await Count() > 0) return;

            for (var i = 0; i < count; i++)
            {
                await Add(await GetRandomEntity());
            }
        }

        protected abstract Task<TEntity> GetRandomEntity();

        protected abstract string GetCollectionName();

        protected Task<IReliableDictionary<Guid, TEntity>> GetCollection()
        {
            return StateManager.GetOrAddAsync<IReliableDictionary<Guid, TEntity>>(GetCollectionName());
        }
    }
}