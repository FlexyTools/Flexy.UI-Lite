namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Boolean))]
	public class Binder_ButtonEnabled : Binder
	{
		[SerializeField] 			Button			_button;
		[SerializeField] 			Boolean			_revert;
		
		private						Func<Boolean>	_getter;

		protected override			void			Bind			( Boolean init )	
		{
			var val = _revert ? !_getter( ) : _getter( );
			
			if( val != _button.interactable )
				_button.interactable = val;
		}
		private						void			Awake			( )					
		{
			if( _button == null )
				_button = GetComponent<Button> ( );

			Init( ref _getter );
		}
	}
}