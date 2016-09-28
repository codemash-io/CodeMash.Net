using System.Linq;

namespace CodeMash.Net.Tests
{
    public static class ProjectExtentions
    {
        public static void FillResourceCategories(this Project project)
        {
            project.Categories = Defaults.DefaultResourceCategories(project.SupportedLanguages).ToList();
        }
    }
}
