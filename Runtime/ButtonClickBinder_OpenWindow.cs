namespace Flexy.UI
{
	public class ButtonClickBinder_OpenWindow : MonoBehaviour
	{
		[Header("Click Options")]
		[SerializeField] Single 	EnableClickDelay		= 0.2f;
		[SerializeField] Single 	ReclickTimeout			= 0.5f;
		[SerializeField] Boolean	DoubleClickMode			= false;
		
		[Header("Events")]
		[SerializeField] FlexyEvent	Clicked;
		[SerializeField] FlexyEvent	Misclicked;
		
		[Header("Window Open Options")]
		[SerializeField] AssetRef<State>	_windowToOpen;
		[SerializeField] EOpenType			_openType;

		private		Single	_lastClickTime;
		private		Single	_enableTime;
		private		Action	_action;
		private		Button	_button;

		private		void	Do			( )	
		{
			if( Time.unscaledTime < _enableTime + EnableClickDelay )
			{
				Misclicked.Raise( this );
						 
				return;
			}

			if ( DoubleClickMode )
			{
				if ( Time.unscaledTime > _lastClickTime + ReclickTimeout )
				{
					_lastClickTime = Time.unscaledTime;
						
					Misclicked.Raise( this );
						
					return;
				}
			}
			else
			{
				if( Time.unscaledTime < _lastClickTime + ReclickTimeout )
				{
					Misclicked.Raise( this );
							 
					return;
				}
					
				_lastClickTime = Time.unscaledTime;
			}
				
			OpenWindow();

			_action?.Invoke( );
				
			Clicked.Raise( this );
			//else												//Not portable, use Button prefab for default click sound 
			//	this.GetService<UIDefaultSfx>( ).DefaultClickSfx.PlayOneShot( this );
		}

		private void OpenWindow( )
		{
			var panel = gameObject.GetComponentInParent<FlowItem>( true );
			
			panel.GameStage.Open( _windowToOpen, _openType );
		}

		private		void	OnEnable	( )	
		{
			_enableTime = Time.unscaledTime;
		}
		private		void	Awake		( )	
		{
			_button = GetComponent<Button>( );
			if( _button != null )
				_button.onClick.AddListener( Do );
		}
		private		void	OnDestroy	( )	
		{
			if( _button )
				_button.onClick.RemoveListener( Do );
		}

		private		String	GetButtonNiceName	( Transform btn )	
		{
			if ( btn.name.Equals( "Button", StringComparison.InvariantCultureIgnoreCase ) )
				return GetButtonNiceName( btn.transform.parent );

			return btn.name.Replace( "Button", "" );
		}
	}
}