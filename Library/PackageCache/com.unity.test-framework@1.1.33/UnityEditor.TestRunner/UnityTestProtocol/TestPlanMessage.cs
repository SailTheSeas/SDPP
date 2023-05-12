<<<<<<< Updated upstream
using System.Collections.Generic;

namespace UnityEditor.TestTools.TestRunner.UnityTestProtocol
{
    internal class TestPlanMessage : Message
    {
        public List<string> tests;

        public TestPlanMessage()
        {
            type = "TestPlan";
        }
    }
}
=======
using System.Collections.Generic;

namespace UnityEditor.TestTools.TestRunner.UnityTestProtocol
{
    internal class TestPlanMessage : Message
    {
        public List<string> tests;

        public TestPlanMessage()
        {
            type = "TestPlan";
        }
    }
}
>>>>>>> Stashed changes
