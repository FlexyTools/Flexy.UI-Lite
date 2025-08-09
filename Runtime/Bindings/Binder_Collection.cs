namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Collection))]
	public class Binder_Collection : ABinder
	{
		[Header("Main")]
		[Tooltip("Container for items")]
		[SerializeField] protected Transform _container;
		[Tooltip("Prefab to instantiate for each item")]
		[SerializeField] protected GameObject _prefab;
		[Tooltip("Prefab to instantiate for each group")]
		[SerializeField] protected GameObject _prefabGroup;

		protected Func<Collection> _getter;
		protected Boolean _preventDestroyItemsOnDisable;
		public	Transform					Container => _container;

		protected override void	Bind				( Boolean init )			
		{
			if(_container == null || _prefab == null || _getter == null)
				return;

			var collection		= _getter.Invoke();
			var prevData		= default(System.Object);
			var dataIndex		= -1;
			var itemsContainer	= _container;

			foreach ( Transform tr in itemsContainer )
				tr.gameObject.Destroy( );

			itemsContainer.DetachChildren( );
			
			foreach ( var data in collection )
			{
				dataIndex++;
				
				//if we want to create group separators
				collection.GroupInjector?.Invoke( prevData, data, dataIndex, (prefabIndex,injectionItem) => 
				{
					var groupGo = Instantiate(_prefabGroup, _container, false);

					groupGo.transform.localScale = Vector3.one;
					
					if( !groupGo.activeSelf )
						groupGo.SetActive( true );

					var newContainer = collection.GroupSetup( groupGo, injectionItem );
					
					if( newContainer != null )
					{
						itemsContainer = newContainer;
					}
				} );

				prevData = data;

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

		protected void		Awake				( )							
		{
			Init(ref _getter);
				
			_prefab.SetActive( false );
			_prefab.transform.SetParent( _container.parent );

			if ( _prefabGroup.IsAlive( ) )
			{ 
				_prefabGroup.SetActive( false );
				_prefabGroup.transform.SetParent( _container.parent );
			}
		}
		protected override void OnDisable			( )							
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