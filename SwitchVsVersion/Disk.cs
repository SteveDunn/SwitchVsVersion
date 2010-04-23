using System ;
using System.Collections.Generic ;
using System.IO ;
using System.Linq ;

namespace SwitchVsVersion
{
    class Disk
    {
        public static IEnumerable<string> GetFiles( string path, string extension )
        {
            var paths = new List<string>();
            paths.AddRange( Directory.GetFiles( path, extension ) ) ;

            string[ ] directories = Directory.GetDirectories( path ) ;
            foreach( string eachDirectoryPath in directories )
            {
                paths.AddRange( GetFiles( eachDirectoryPath, extension ) ) ;
            }

            return paths ;
        }

    	public static void ModifyFile( string pathToFile, IEnumerable< Mapping > mappings )
    	{
			string allText = File.ReadAllText(pathToFile);
    		if ( string.IsNullOrEmpty( allText ) )
    		{
    			return ;
    		}

			if(!anythingNeedsReplacing( allText, mappings ) )
			{
				Console.WriteLine(@"nothing to modify in " + pathToFile);
				return ;
			}

			Console.WriteLine(@"modifying " + pathToFile);

			allText = mappings.Aggregate(
				allText,
				(current, eachMapping) => current.Replace(eachMapping.OldText, eachMapping.NewText));

			File.WriteAllText(pathToFile, allText);
		}

    	static bool anythingNeedsReplacing( string allText, IEnumerable< Mapping > mappings )
    	{
    		return mappings.Any(m => allText.Contains(m.OldText)) ;
    	}
    }
}