using System;
using System.Collections.Generic;
using System.Text;

namespace TriggerSearch.Core.Hooks
{
    public class HookTrackingResult
    {
        public List<HookedEntityEntry> EntriesAdded { get; set; } = new List<HookedEntityEntry>();
        public List<HookedEntityEntry> EntriesModified { get; set; } = new List<HookedEntityEntry>();
        public List<HookedEntityEntry> EntriesDeleted { get; set; } = new List<HookedEntityEntry>();

    }
}
