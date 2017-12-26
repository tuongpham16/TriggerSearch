using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TriggerSearch.Core.Hooks
{
    public delegate Task<object> HookTrackingEventHandler(HookTrackingResult data);
}
