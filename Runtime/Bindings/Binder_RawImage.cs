namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Texture2D))]
	public class Binder_RawImage : Binder
	{
		[Tooltip("Will get from this GO, if not set")]
		[FormerlySerializedAs("_texture")]
		[SerializeField]	RawImage	_rawImage = null!;
		[SerializeField]	Boolean		_disableOnNullValue		= true;
		[SerializeField]	Boolean		_ignoreNullValue		= true;

		private				Func<Texture2D> _getter = null!;

		protected override	void	Bind	( Boolean init )	
		{
			var texture = _getter();

			if (_disableOnNullValue)
				_rawImage.enabled = texture.IsAlive();

			if (texture.IsAlive())
				_rawImage.texture = texture;
				
			else if (!_ignoreNullValue) 
				_rawImage.texture = null;
		}
		private				void	Awake	( )					
		{
			if (!_rawImage)
				_rawImage = GetComponent<RawImage>( );
			
			Init( ref _getter );
		}
	}
}