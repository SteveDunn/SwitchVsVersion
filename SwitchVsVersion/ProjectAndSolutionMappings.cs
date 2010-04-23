using System.Collections ;
using System.Collections.Generic ;

namespace SwitchVsVersion
{
	public class ProjectAndSolutionMappings : IEnumerable<Mapping>
	{
		static readonly List<Mapping> _mappings =
			new List<Mapping>
				{
					new Mapping(
						@"Microsoft Visual Studio Solution File, Format Version 10.00", 
						@"Microsoft Visual Studio Solution File, Format Version 11.00" ),
					new Mapping(
						@"# Visual Studio 2008", 
						@"# Visual Studio 2010" ),
					new Mapping(@"ToolsVersion=""3.5""", @"ToolsVersion=""4.0""" ),
					new Mapping(
						@"<Import Project=""$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v9.0\WebApplications\Microsoft.WebApplication.targets"" />", 
						@"<Import Project=""$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v10.0\WebApplications\Microsoft.WebApplication.targets""/>" ),
				};

		public IEnumerator< Mapping > GetEnumerator( )
		{
			return _mappings.GetEnumerator( ) ;
		}

		IEnumerator IEnumerable.GetEnumerator( )
		{
			return GetEnumerator( ) ;
		}
	}
}