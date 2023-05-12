<<<<<<< Updated upstream
using UnityEngine.TestTools;

namespace UnityEditor.TestTools.TestRunner
{
    internal class PostbuildCleanupAttributeFinder : AttributeFinderBase<IPostBuildCleanup, PostBuildCleanupAttribute>
    {
        public PostbuildCleanupAttributeFinder() : base(attribute => attribute.TargetClass) {}
    }
}
=======
using UnityEngine.TestTools;

namespace UnityEditor.TestTools.TestRunner
{
    internal class PostbuildCleanupAttributeFinder : AttributeFinderBase<IPostBuildCleanup, PostBuildCleanupAttribute>
    {
        public PostbuildCleanupAttributeFinder() : base(attribute => attribute.TargetClass) {}
    }
}
>>>>>>> Stashed changes
