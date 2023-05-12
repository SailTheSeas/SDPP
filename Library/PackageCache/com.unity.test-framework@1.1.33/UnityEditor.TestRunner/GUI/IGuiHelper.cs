<<<<<<< Updated upstream
using System;
using System.Reflection;

namespace UnityEditor.TestTools.TestRunner.GUI
{
    internal interface IGuiHelper
    {
        bool OpenScriptInExternalEditor(string stacktrace);
        void OpenScriptInExternalEditor(Type type, MethodInfo method);
        IFileOpenInfo GetFileOpenInfo(Type type, MethodInfo method);
        string FilePathToAssetsRelativeAndUnified(string filePath);
    }
}
=======
using System;
using System.Reflection;

namespace UnityEditor.TestTools.TestRunner.GUI
{
    internal interface IGuiHelper
    {
        bool OpenScriptInExternalEditor(string stacktrace);
        void OpenScriptInExternalEditor(Type type, MethodInfo method);
        IFileOpenInfo GetFileOpenInfo(Type type, MethodInfo method);
        string FilePathToAssetsRelativeAndUnified(string filePath);
    }
}
>>>>>>> Stashed changes
