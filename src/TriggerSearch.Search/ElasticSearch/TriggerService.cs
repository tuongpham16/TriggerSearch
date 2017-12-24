using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TriggerSearch.Core.Hooks;

namespace TriggerSearch.Search
{
    public class TriggerService : ITriggerService
    {

        private readonly IIndexService _indexService;
        public TriggerService(IIndexService indexService)
        {
            _indexService = indexService;
        }

        public async Task<object> TriggerSave(HookTrackingResult result)
        {
           
            _indexService.SetDbContext(result.Context);
            if (result.EntriesAdded.Count > 0)
            {
                foreach (var entry in result.EntriesAdded)
                {
                    await _indexService.IndexAsync(entry.Entity);
                }
            }

            if (result.EntriesModified.Count > 0)
            {
                foreach (var entry in result.EntriesModified)
                {
                    await _indexService.UpdateAsync(entry.Entity);
                }
            }

            if (result.EntriesDeleted.Count > 0)
            {
                foreach (var entry in result.EntriesDeleted)
                {
                    await _indexService.DeleteAsync(entry.Entity);
                }
            }
            return string.Empty;
        }

    }
}
