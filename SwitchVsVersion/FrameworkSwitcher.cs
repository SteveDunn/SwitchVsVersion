using System ;
using System.Collections.Generic ;
using System.IO ;

namespace SwitchVsVersion
{
    internal static class FrameworkSwitcher
    {
        public static void ModifyAllProjectsUnderThisFolderTo( string path, string frameworkVersion )
        {
            IEnumerable<string> projectFilePaths = Disk.GetFiles(path, @"*.csproj");
            foreach (string eachProjectFilePath in projectFilePaths)
            {
                modifyProjectFile(eachProjectFilePath, frameworkVersion);
            }
        }

        static void modifyProjectFile(string projectFilePath, string frameworkVersion)
        {
            Console.Write(
                string.Format(
                    @"Converting to {0} - {1}... ", frameworkVersion, projectFilePath ) ) ;

            string allText = File.ReadAllText( projectFilePath ) ;
            string xmlFragement = string.Format(
                @"<TargetFrameworkVersion>v{0}</TargetFrameworkVersion>", frameworkVersion ) ;

            if( allText.Contains( xmlFragement ) )
            {
                Console.WriteLine( @"Already converted - did nothing." ) ;
                return ;
            }

            allText = allText.Replace(
                @"</PropertyGroup>",
                xmlFragement + @"</PropertyGroup>" ) ;
            
            File.WriteAllText( projectFilePath, allText ) ;

            Console.WriteLine( @"Done" ) ;
        }
    }
}