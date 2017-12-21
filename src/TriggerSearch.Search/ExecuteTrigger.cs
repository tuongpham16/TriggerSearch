using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Core.Hooks;

namespace TriggerSearch.Search
{
    public class ExecuteTrigger: IExecuteTrigger
    {
        private readonly IHookFunction _hookFunction;
        private readonly ITriggerService _indexService;

        public ExecuteTrigger(IHookFunction hookFunction, ITriggerService indexService)
        {
            _indexService = indexService;
            _hookFunction = hookFunction;
            _hookFunction.TriggerSaveEvent += IndexData;
        }

        public void IndexData(object obj, EventArgs e)
        {
            var result = (HookTrackingResult)obj;
            _indexService.TriggerSave(result);
        }
    }
}
