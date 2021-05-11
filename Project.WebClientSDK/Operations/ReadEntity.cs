using Stylelabs.M.Base.Querying;
using Stylelabs.M.Framework.Essentials.LoadConfigurations;
using Stylelabs.M.Sdk.Contracts.Base;
using Stylelabs.M.Sdk.Contracts.Querying;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Stylelabs.M.Base.Querying.Linq;
using Sitecore.CH.Project.WebClientSDK.Examples.Client;

namespace Sitecore.CH.Project.WebClientSDK.Examples.Operations
{
    internal static class ReadEntity
    {
        public static async Task<IEntity> GetEntityById(long entityId)
        {
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.Id == entityId
                                                      select e);

            return await MConnector.Client().Querying.SingleAsync(query, EntityLoadConfiguration.Full);
        }

        public static async Task<IEntityQueryResult> GetEntitiesByDefinition(string definitionName)
        {
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == definitionName &&
                                                      e.Parent("ContentRepositoryToAsset") == 734
                                                      select e);
            query.Take = 150;

            return await MConnector.Client().Querying.QueryAsync(query, EntityLoadConfiguration.Default);
        }

        public static async Task<IEntityQueryResult> GetEntitiesByTitle(string title)
        {
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == "M.Asset"
                                                      && e.Property("Title") == title
                                                      select e);

            return await MConnector.Client().Querying.QueryAsync(query, EntityLoadConfiguration.Default);
        }

        public static async Task<IEntityQueryResult> GetAssetsByAssetType(long assetTypeId)
        {
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == "M.Asset" &&
                                                      e.Parent("AssetTypeToAsset") == assetTypeId
                                                      select e);

            return await MConnector.Client().Querying.QueryAsync(query, EntityLoadConfiguration.Default);
        }

        public static async Task<List<IEntity>> GetEntitiesByDefinitionByIterator(string definitionName)
        {
            List<IEntity> entities = new List<IEntity>();
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == definitionName &&
                                                      e.Parent("ContentRepositoryToAsset") == 734 //Standard (DAM)
                                                      select e);
            var iterator = MConnector.Client().Querying.CreateEntityIterator(query, EntityLoadConfiguration.Default);
            while (await iterator.MoveNextAsync().ConfigureAwait(false))
            {
                entities.AddRange(iterator.Current.Items);
            }
            return entities;
        }

        public static async Task<List<IEntity>> GetEntitiesByDefinitionByScroller(string definitionName)
        {
            List<IEntity> entities = new List<IEntity>();
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.DefinitionName == definitionName &&
                                                      e.Parent("ContentRepositoryToAsset") == 734
                                                      select e);

            var scroller = MConnector.Client().Querying.CreateEntityScroller(query, TimeSpan.FromSeconds(30),
                EntityLoadConfiguration.DefaultCultureFull);

            scroller.Reset();

            while (await scroller.MoveNextAsync().ConfigureAwait(false))
            {
                entities.AddRange(scroller.Current.Items);
            }
            return entities;
        }

        public static long? GetEntityIdByIdentifier(string identifier)
        {
            var query = Query.CreateQuery(entities => from e in entities
                                                      where e.Identifier == identifier
                                                      select e);

            return MConnector.Client().Querying.SingleIdAsync(query).Result;
        }
    }
}
