namespace Flexy.UI.Extra
{
	[RequireComponent(typeof(Button))]
	public class ButtonClick_OpenWindow : UIWidget
	{
		[Header("Click Options")]
		[SerializeField] Single		_enableClickDelay	= 0.2f;
		[SerializeField] Single		_reclickTimeout		= 0.5f;
		
		[Header("Actions")]
		[SerializeField] FlexyEvent			_clicked;
		[SerializeField] AssetRef<State>	_windowToOpen;
		
		private		Single	_lastClickTime;
		private		Single	_enableTime;
		private		Button	_button = null!;

		private		void	Do			( )	
		{
			if (Time.unscaledTime < _enableTime + _enableClickDelay)
				return;

			if (Time.unscaledTime < _lastClickTime + _reclickTimeout)
				return;
				
			_lastClickTime = Time.unscaledTime;
				
			OpenWindow();
			_clicked.Raise(this);
		}
		private		void	OpenWindow	( )	
		{
			State.Node.Graph.Open( _windowToOpen, State.Node );
		}

		private		void	OnEnable	( )	
		{
			_enableTime = Time.unscaledTime;
		}
		private		void	Awake		( )	
		{
			_button = GetComponent<Button>();
			if (_button != null)
				_button.onClick.AddListener(Do);
		}
		private		void	OnDestroy	( )	
		{
			if (_button)
				_button.onClick.RemoveListener(Do);
		}
	}
}