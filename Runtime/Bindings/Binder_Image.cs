namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Sprite))]
	public class Binder_Image : Binder
	{
		[SerializeField] 	Image		_image;
		[SerializeField] 	Boolean		_disableOnNullValue		= true;
		[SerializeField] 	Boolean		_ignoreNullValue		= true;

		private				Func<Sprite> _getter;

		protected override			void					Bind				( Boolean init )				
		{
			var sprite = _getter();

			if (_disableOnNullValue)
				_image.enabled = sprite != null;

			if (sprite.IsAlive())		_image.sprite = sprite;
			else if (!_ignoreNullValue)	_image.sprite = null;
		}

		private						void					Awake				( )				
		{
			if (!_image)
				_image = GetComponent<Image>();
			
			Init( ref _getter );
		}
	}
}