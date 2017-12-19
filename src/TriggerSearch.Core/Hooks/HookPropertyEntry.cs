using System;
using System.Collections.Generic;
using System.Text;

namespace TriggerSearch.Core.Hooks
{
    public class HookPropertyEntry
    {
        public string Name { get; set; }
        public object OriginalValue { get; set; }
        public object CurrentValue { get; set; }
    }
}
