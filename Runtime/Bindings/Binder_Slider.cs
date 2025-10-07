namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Single))]
	public class Binder_Slider : Binder
	{
		[SerializeField, Tooltip("Will get from this GO if null")]
		private		Slider	_slider;

		[Header("Tween")]
		[SerializeField]		Boolean	_useTween;
		[SerializeField]		Boolean	_startFromZero;
		[SerializeField]		Single	_tweenTime;
		
		private		Func<Single>		_getter;
		private		Action<Single>		_setter;
		private		Boolean				_inset;
		
		protected override		void	Bind			( Boolean init )	
		{
			if( _inset || !Application.isPlaying )
				return;

			var val = _getter( );
			
			if( !init && _slider.value == val )
				return;
			
			var setter	= _setter;
			_setter		= null;
			
			DoBind( val );

			_setter		= setter;
		}
		private					void	DoBind			( Single newValue )	
		{
			_slider.value = newValue;
		}
		private					void	Awake			( )					
		{
			if( _slider == null )
				_slider = GetComponent<Slider>();

			Init( ref _getter );
			Init( ref _setter, false );

			_slider.onValueChanged.AddListener( OnValueChanged );
			
		}

		private					void	OnValueChanged	( Single value )	
		{
			if( _setter == null )
				return;

			_inset	= true;
			_setter	( value );
			_inset	= false;
		}
	}
}