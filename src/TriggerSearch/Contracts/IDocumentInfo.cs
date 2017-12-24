using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TriggerSearch
{
    public interface IDocumentInfo
    {
        IBuildQuery Query { get; set; }
        string Index { get; }
        string Type { get; }
        MemberInfo KeyProperty { get;  }
        Type EntityTarget { get; }
        string[] References { get;}
        string[] Collections { get;}
        bool RefeshAfterIndex { get; }
        bool RefeshAfterUpdate { get; }
        bool RefeshAfterDeleted { get;}
        BehaviorChange LoadReferenceBehavior { get;}
        BehaviorChange LoadQueryBehavior { get; }
    }
}
