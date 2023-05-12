<<<<<<< Updated upstream
namespace Packages.Rider.Editor.ProjectGeneration {
  class GUIDProvider : IGUIDGenerator
  {
    public string ProjectGuid(string name)
    {
      return SolutionGuidGenerator.GuidForProject(name);
    }
  }
}
=======
namespace Packages.Rider.Editor.ProjectGeneration {
  class GUIDProvider : IGUIDGenerator
  {
    public string ProjectGuid(string name)
    {
      return SolutionGuidGenerator.GuidForProject(name);
    }
  }
}
>>>>>>> Stashed changes
