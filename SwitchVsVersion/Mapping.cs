using System ;

namespace SwitchVsVersion
{
	public class Mapping
	{
		readonly string _oldText ;
		readonly string _newText ;

		public Mapping( string oldText, string newText )
		{
			_oldText = oldText ;
			_newText = newText ;
		}

		public string OldText
		{
			get { return _oldText ; }
		}

		public string NewText
		{
			get { return _newText ; }
		}

		public Mapping InReverse( )
		{
			return new Mapping( _newText, _oldText ) ;
		}
	}
}