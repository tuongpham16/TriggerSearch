using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriggerSearch.Core.Hooks;

namespace TriggerSearch.Search
{
    public interface  ITriggerService
    {
        Task<object> TriggerSave(HookTrackingResult entities);
    }
}
