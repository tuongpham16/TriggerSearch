using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TriggerSearch.Core.Hooks
{
    public class HookFunction : IHookFunction
    {
        public event EventHandler TriggerSaveEvent;
        public async Task<object> TriggerSave(HookTrackingResult entities)
        {
            TriggerSaveEvent(entities, new EventArgs());
            await Task.CompletedTask;
            return string.Empty;
        }
    }
}
