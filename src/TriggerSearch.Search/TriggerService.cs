using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriggerSearch.Core.Hooks;

namespace TriggerSearch.Search
{
    public class TriggerService : ITriggerService
    {

        private readonly IIndexService _searchService;
        public TriggerService(IIndexService searchService)
        {
            _searchService = searchService;
        }

        public async Task<object> TriggerSave(HookTrackingResult entities)
        {
            if (entities.EntriesAdded.Count > 0)
            {
                foreach (var entry in entities.EntriesAdded)
                {
                    await _searchService.IndexAsync(entry.Entity);
                }
            }

            if (entities.EntriesModified.Count > 0)
            {
                foreach (var entry in entities.EntriesModified)
                {
                    await _searchService.UpdateAsync(entry.Entity);
                }
            }

            if (entities.EntriesDeleted.Count > 0)
            {
                foreach (var entry in entities.EntriesDeleted)
                {
                    await _searchService.DeleteAsync(entry.Entity);
                }
            }
            return string.Empty;
        }

    }
}
