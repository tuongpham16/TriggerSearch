using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TriggerSearch.Core.Hooks
{
    public interface IHookFunction
    {
        event HookTrackingEventHandler TriggerSaveEvent;
        Task<object> TriggerSave(HookTrackingResult entities);
    }
}
