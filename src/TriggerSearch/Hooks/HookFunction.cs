using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TriggerSearch.Core.Hooks
{
    public class HookFunction : IHookFunction
    {
        public event HookTrackingEventHandler TriggerSaveEvent;
        public Task<object> TriggerSave(HookTrackingResult entities)
        {
            Task<object> task = TriggerSaveEvent(entities);
            return task;
        }

    

    }
}
