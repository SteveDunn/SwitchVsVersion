using System ;
using System.Collections.Generic ;
using System.IO ;
using System.Text.RegularExpressions ;
using System.Xml ;
using System.Xml.Linq ;

namespace SwitchVsVersion
{
	internal static class TargetPlatformSwitcher
	{
		public static void ModifyAllProjectsUnderThisFolderTo( string path, string targetPlatform )
		{
			IEnumerable<string> projectFilePaths = getProjectFiles( path );
			
			foreach (string eachProjectFilePath in projectFilePaths)
			{
				try
				{
					modifyProjectFile( eachProjectFilePath, targetPlatform ) ;
				}
				catch(XmlException ex)
				{
					Console.WriteLine( @"SKIPPED '{0}' because '{1}'.", eachProjectFilePath, ex.Message ) ;
				}
			}
		}

		static IEnumerable<string> getProjectFiles( string path )
		{
			if( StringComparer.Ordinal.Compare( Path.GetExtension( path ), @".csproj" ) == 0 )
			{
				return new[] {path } ;
			}
			
			return Disk.GetFiles(path, @"*.csproj") ;
		}

		static void modifyProjectFile(string projectFilePath, string targetPlatform)
		{
			Console.Write(
				string.Format(
					@"Converting to {0} - {1}... ", targetPlatform, projectFilePath ) ) ;

			XElement e = XElement.Load( projectFilePath ) ;

			XNamespace ns = @"http://schemas.microsoft.com/developer/msbuild/2003" ;
			
			IEnumerable<XElement> propertyGroupElements = e.Elements( ns+@"PropertyGroup" ) ;

			bool modified = false ;

			foreach( XElement eachElement in propertyGroupElements )
			{
				XAttribute conditionAttribute = eachElement.Attribute( @"Condition" ) ;
				if( conditionAttribute == null )
				{
					continue ;
				}

				if(!conditionAttribute.Value.Contains( @"'$(Configuration)|$(Platform)'" ))
				{
					continue ;
				}

				XElement platformTargetElement = eachElement.Element(ns+ @"PlatformTarget" ) ;
				if( platformTargetElement == null )
				{
					modified = true ;
					platformTargetElement=new XElement(ns+ @"PlatformTarget",targetPlatform );
					eachElement.Add( platformTargetElement ) ;
				}
				else
				{
					if (platformTargetElement.Value != targetPlatform)
					{
						platformTargetElement.Value = targetPlatform ;
						modified = true ;
					}
				}
			}

			if( modified )
			{
				File.WriteAllText( projectFilePath, e.ToString() ) ;
				Console.WriteLine( @"DONE" ) ;
			}	
			else
			{
				Console.WriteLine( @"NOTHING TO CHANGE, all configuration already set to '{0}'", targetPlatform ) ;
			}
		}
	}
}