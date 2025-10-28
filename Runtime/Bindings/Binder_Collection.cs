using Flexy.Core.Extensions;
using GameObject = UnityEngine.GameObject;

namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Collection))]
	[DefaultExecutionOrder(-90)]
	public class Binder_Collection : Binder
	{
		[SerializeField] protected	Transform	_container = null!;
		[SerializeField] protected	GameObject	_prefab = null!;

		protected Func<Collection> _getter = null!;
		protected Boolean _preventDestroyItemsOnDisable;
		
		public	Transform	Container => _container;

		protected override	void	Bind		( Boolean init )	
		{
			if (_container == null || _prefab == null || _getter == null)
				return;

			var collection		= _getter.Invoke();
			var dataIndex		= -1;
			var itemsContainer	= _container;

			foreach (Transform tr in itemsContainer)
				tr.gameObject.Destroy();

			itemsContainer.DetachChildren();
			
			foreach (var data in collection)
			{
				dataIndex++;
				
				_prefab.SetActive(false);
				var itemWidget = Instantiate(_prefab);
				_prefab.ClearEditorDirty();
				
				itemWidget.transform.SetParent(itemsContainer, false);
				itemWidget.transform.localScale = Vector3.one;
				
				if (itemWidget.TryGetComponent<IAutoSetup>(out var autoSetup))
					autoSetup.Setup(data);
					
				try { collection.Setup?.Invoke( itemWidget, data, true, dataIndex ); }   
				catch (Exception ex) { Debug.LogException( ex ); }
				
				// We activate after data setup so binders can bind to actual data
				itemWidget.SetActive(true);
			}
			
			collection.DoneCallback?.Invoke(this);
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