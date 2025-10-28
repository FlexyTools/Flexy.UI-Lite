using Flexy.Core.Extensions;

namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Texture2D))]
	public class Binder_RawImage : Binder
	{
		[SerializeField]	RawImage	_texture = null!;
		[SerializeField]	Boolean		_disableOnNullValue		= true;
		[SerializeField]	Boolean		_ignoreNullValue		= true;

		private				Func<Texture2D> _getter = null!;

		protected override	void	Bind	( Boolean init )	
		{
			var texture = _getter();

			if (_disableOnNullValue)
				_texture.enabled = texture.IsAlive();

			if (texture.IsAlive())
				_texture.texture = texture;
				
			else if (!_ignoreNullValue) 
				_texture.texture = null;
		}
		private				void	Awake	( )					
		{
			if( !_texture )
				_texture = GetComponent<RawImage>( );
			
			Init( ref _getter );
		}
	}
}