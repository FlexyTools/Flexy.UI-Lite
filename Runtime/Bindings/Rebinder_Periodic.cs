namespace Flexy.UI.Bindings
{
	[RequireComponent(typeof(Binder))]
	public class Rebinder_Periodic : MonoBehaviour
	{
		[SerializeField] TypeUpdate	_typeUpdate		= TypeUpdate.Update;
		[SerializeField] Single		_periodSeconds;
		[SerializeField] Boolean	_isUnscaledTime	= true;

		private		Single		_timer;
		private		Binder[]	_binders = null!;

		private		void	Awake		( )	
		{
			_binders = GetComponents<Binder>().Where( c => c.enabled ).ToArray();
		}
		private		void	OnEnable	( )	
		{
		  _timer = Time.time;
		}
		private		void	Update		( )	
		{
			if (_typeUpdate == TypeUpdate.Update)
			{
				Tick(  );
			}
		}
		private		void	LateUpdate	( )	
		{
			if (_typeUpdate == TypeUpdate.LateUpdate)
			{
				Tick(  );
			}
		}
		private		void	FixedUpdate	( )	
		{
			if (_typeUpdate == TypeUpdate.FixedUpdate)
			{
				Tick(  );
			}
		}
		private		void	Tick		( )	
		{
			var now = _isUnscaledTime ? Time.unscaledTime : Time.time;
			if( now - _timer < _periodSeconds )
				return;

			_timer += _periodSeconds;
			foreach (var binder in _binders)
			{
				//Profiler.BeginSample( binder.ToString(  ), binder );
				binder.Rebind();
				//Profiler.EndSample( );
			}
		}
		
		private enum TypeUpdate
		{
			LateUpdate,
			Update,
			FixedUpdate
		}
	}
}