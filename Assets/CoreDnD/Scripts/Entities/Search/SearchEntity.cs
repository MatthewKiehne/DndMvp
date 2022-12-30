using System.Collections.Generic;
using System.Linq;
using Entities.Features;
using UnityEngine;

namespace Entities.Search
{
    public class SearchEntity
    {

        public const string FeatureSearchTerm =  "Features";


        public EntitySearchResult Search(Entity entity, EntitySearchQuery query)
        {

            Debug.Log(entity.Features.Count);
            EntitySearchResult result = new EntitySearchResult();

            List<string> allFeatureTerms = new List<string>();

            foreach (string searchTarget in query.SearchTargets)
            {
                switch (searchTarget)
                {
                    case SearchEntity.FeatureSearchTerm:
                        allFeatureTerms.AddRange(query.ConditionNames);
                        break;
                    default:
                        // does not add
                        break;
                }
            }

            HashSet<string> featureSearchTerms = new HashSet<string>(allFeatureTerms.Distinct());
            EntitySearchResult featureResult = SearchFeatureList(entity.Features, featureSearchTerms);

            return CombineSearchDirectories(new List<EntitySearchResult>() {
                featureResult
            });
        }

        private EntitySearchResult SearchFeatureList(List<Feature> features, HashSet<string> searchTerms)
        {
            EntitySearchResult searchResult = new EntitySearchResult();

            Debug.Log("SearchFeatureList " + features.Count);

            foreach (Feature feature in features)
            {
                // Debug.Log(feature.Name);
                foreach (Condition condition in feature.Conditions)
                {
                    if (searchTerms.Contains(condition.Name))
                    {
                        // Debug.Log("~ " + condition.Name);
                        if (!searchResult.ContainsKey(condition.Name))
                        {
                            searchResult.Add(condition.Name, new List<Condition>());
                        }

                        searchResult[condition.Name].Add(condition);
                    }
                }
            }

            return searchResult;
        }

        private EntitySearchResult CombineSearchDirectories(List<EntitySearchResult> dictionaries)
        {
            EntitySearchResult result = new EntitySearchResult();

            foreach (EntitySearchResult item in dictionaries)
            {
                foreach (KeyValuePair<string, List<Condition>> pair in item)
                {
                    if (!result.ContainsKey(pair.Key))
                    {
                        result.Add(pair.Key, new List<Condition>());
                    }
                    result[pair.Key].AddRange(pair.Value);
                }
            }

            return result;
        }
    }
}