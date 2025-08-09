namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Collection))]
	public class Binder_Collection : ABinder
	{
		[SerializeField] protected	Transform	_container;
		[SerializeField] protected	GameObject	_prefab;

		protected Func<Collection> _getter;
		protected Boolean _preventDestroyItemsOnDisable;
		
		public	Transform	Container => _container;

		protected override	void	Bind		( Boolean init )	
		{
			if(_container == null || _prefab == null || _getter == null)
				return;

			var collection		= _getter.Invoke();
			var dataIndex		= -1;
			var itemsContainer	= _container;

			foreach ( Transform tr in itemsContainer )
				tr.gameObject.Destroy( );

			itemsContainer.DetachChildren( );
			
			foreach ( var data in collection )
			{
				dataIndex++;
				
				var itemObj = Instantiate(_prefab);
				itemObj.transform.SetParent(itemsContainer, false);
				itemObj.transform.localScale = Vector3.one;

				// widget must be inactive before we setup it with data
				if( itemObj.activeSelf )
					itemObj.SetActive( false );
				
				try { collection.Setup?.Invoke( itemObj, data, true, dataIndex ); }    catch (Exception ex) { Debug.LogException( ex ); }
				
				// after we setup data into item we activate so binders can bind to actual data
				itemObj.SetActive( true );
			}
			
			collection.DoneCallback?.Invoke( this );
		}

		protected			void	Awake		( )		
		{
			Init(ref _getter);
				
			_prefab.SetActive( false );
			_prefab.transform.SetParent( _container.parent );
		}
		protected override	void	OnDisable	( )		
		{
			base.OnDisable( );
			
			if( _preventDestroyItemsOnDisable )
				return;
			
			foreach ( Transform item in _container )
			{
				item.gameObject.SetActive( false );
				Destroy( item.gameObject );
			}
		}
	}
}