namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Color))]
	public class Binder_GraphicColor : ABinder
	{
		[SerializeField] private Graphic _widget;
		[SerializeField] Boolean _useCustomAlpha;
		[Range(0f,1f)]
		[SerializeField] Single _customAlpha;
	
		private Color _value;
    
		private Func<Color> _getter;

		protected override void Bind(Boolean init)
		{
			var color = _getter();
    
			if( !init && _value == color )
				return;
	
			_value = color;
		
			if (_useCustomAlpha)
				color.a = _customAlpha;
      
			_widget.color = color;
		}

		private void Awake()
		{
			if (_widget == null)
				_widget = GetComponent<Graphic>();

			Init(ref _getter);
		}
	}
}