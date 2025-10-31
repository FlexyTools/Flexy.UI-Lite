#if UNITY_TMPRO || UNITY_6_OR_NEWER
using TMPro;

namespace Flexy.UI.Bindings
{
	[BindTo(typeof(String))]
	public class Binder_Text : Binder
	{
		[Tooltip("Will get from this GO if null")]
		[SerializeField]	TMP_Text	_label = null!;

		private		Func<String>	_getter = null!;
		private		String?			_value;
		
		protected override	void	Bind	( Boolean init )
		{
			var text = _getter();

			//if nothing changed and this is not first time then return
			if( !init && _value == text )
				return;
			
			_value = text;
			
			_label.text					= text;
		}
		private				void	Awake	( )				
		{
			if( _label == null )
				_label = GetComponent<TMP_Text> ( );

			Init( ref _getter );
		}
	}
}
#endif