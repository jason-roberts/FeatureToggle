#if NETCORE

using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace FeatureToggle.NetCore.Tests
{
    // Original code from: https://github.com/aspnet/Mvc
    public static class SolutionPathUtility
    {
        private const string SolutionName = "FeatureToggle.sln";

        /// <summary>
        /// Gets the full path to the project.
        /// </summary>
        /// <param name="solutionRelativePath">
        /// The parent directory of the project.
        /// e.g. samples, test, or test/Websites
        /// </param>
        /// <param name="assembly">The project's assembly.</param>
        /// <returns>The full path to the project.</returns>
        public static string GetProjectPath(string solutionRelativePath, Assembly assembly)
        {
            var projectName = assembly.GetName().Name;
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;

            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, SolutionName));
                if (solutionFileInfo.Exists)
                {
                    return Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName));
                }

                directoryInfo = directoryInfo.Parent;
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
        }
    }
}
#endif