using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Core.Hooks;

namespace TriggerSearch.Search
{
    public class ExecuteTrigger
    {
        private readonly IHookFunction _hookFunction;

        public ExecuteTrigger(IHookFunction hookFunction)
        {
            _hookFunction = hookFunction;
            _hookFunction.TriggerSaveEvent += IndexData;
        }
        
        public void IndexData(object obj, EventArgs e)
        {
            var result = (HookTrackingResult) obj;
            var indexService = SearchServiceLocator.GetService<ITriggerService>();
            indexService.TriggerSave(result);
        }
    }
}
