namespace Flexy.UI.Bindings
{
	public struct Collection : IEnumerable
	{
		public Collection( IEnumerable collection, SetupCallback setup, IEqualityComparer<Object> comparer = null, Action<Binder_Collection> doneCallback = null )
		{
			_collection	= collection;
			_comparer	= comparer;

			_setup = setup;
			_doneCallback = doneCallback;
		}
		
		private readonly IEnumerable					_collection;
		private readonly IEqualityComparer<Object>		_comparer;

		private readonly SetupCallback				_setup;
		private readonly Action<Binder_Collection>	_doneCallback;

		public  Action<Binder_Collection>	DoneCallback	=> _doneCallback;
		public	SetupCallback				Setup			=> _setup;
		
		public	Boolean						IsEmpty			=> _collection == null || !_collection.GetEnumerator( ).MoveNext( );

		public IEnumerator GetEnumerator()
		{
			return _collection.GetEnumerator();
		}
		
		public delegate		void		SetupCallback			( GameObject widget, Object data, Boolean isNew, Int32 index );
	}
}