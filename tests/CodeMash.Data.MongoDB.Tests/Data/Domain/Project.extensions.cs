using System.Linq;

namespace CodeMash.Data.MongoDB.Tests.Data
{
    public static class ProjectExtensions
    {
        public static void FillResourceCategories(this Project project)
        {
            project.Categories = Defaults.DefaultResourceCategories(project.SupportedLanguages).ToList();
        }
    }
}
