namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Texture2D))]
	public class Binder_RawImage : ABinder
	{
		[SerializeField]
		private						RawImage		_texture;
		[SerializeField] 
		private						Boolean         _disableOnNullValue						= true;
		[SerializeField] 
		private						Boolean			_ignoreNullValue						= true;

		private						Func<Texture2D> _getter;

		protected override			void					Bind				( Boolean init )				
		{
			var texture = _getter();

			if ( _disableOnNullValue )
				_texture.enabled = texture != null;

			if (texture.IsAlive(  ))
				_texture.texture = texture;
			else if( !_ignoreNullValue ) 
				_texture.texture = null;
		}

		private						void					Awake				( )				
		{
			if( !_texture )
				_texture = GetComponent<RawImage>( );
			
			Init( ref _getter );
		}
	}
}