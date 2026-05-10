namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Sprite))]
	public class Binder_Image : Binder
	{
		[Tooltip("Will get from this GO, if not set")]
		[SerializeField] 	Image		_image = null!;
		[SerializeField] 	Boolean		_disableOnNullValue		= true;
		[SerializeField] 	Boolean		_ignoreNullValue		= true;
		[SerializeField]	Boolean		_override;

		private		Func<Sprite>	_getter = null!;
		private		Sprite?			_lastValue = null!;

		protected override	void		Bind	( Boolean init )	
		{
			var value = _getter();

			if (!init && _lastValue == value)
				return;
			
			_lastValue = value;
			
			if (_disableOnNullValue)
				_image.enabled = value != null;

			var finalVal = default(Sprite);

			if (value.IsAlive())	
				finalVal = value;
			
			if(_ignoreNullValue && finalVal == null)
				return;

			if (_override)	_image.overrideSprite	= finalVal;
			else			_image.sprite			= finalVal;
		}
		private				void		Awake	( )					
		{
			if (!_image)
				_image = GetComponent<Image>();
			
			Init( ref _getter );
		}
	}
}