<<<<<<< Updated upstream
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TestTools.TestRunner.GUI;

namespace TestRunner.Callbacks
{
    internal class WindowResultUpdaterDataHolder : ScriptableSingleton<WindowResultUpdaterDataHolder>
    {
        public List<TestRunnerResult> CachedResults = new List<TestRunnerResult>();
    }
=======
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TestTools.TestRunner.GUI;

namespace TestRunner.Callbacks
{
    internal class WindowResultUpdaterDataHolder : ScriptableSingleton<WindowResultUpdaterDataHolder>
    {
        public List<TestRunnerResult> CachedResults = new List<TestRunnerResult>();
    }
>>>>>>> Stashed changes
}