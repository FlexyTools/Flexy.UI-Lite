using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flexy.UI.Bindings
{
	public struct Collection : IEnumerable
	{
		public Collection( IEnumerable collection, SetupCallback setup, IEqualityComparer<Object> comparer = null, Action<Binder_Collection> doneCallback = null )
		{
			_collection	= collection;
			_comparer	= comparer;

			_setup = setup;
			_groupInjector = null;
			_groupSetup = null;
			_doneCallback = doneCallback;
		}
		public Collection( IEnumerable collection, SetupCallback setup, GroupInjectorCallback groupInjector, GroupSetupCallback	groupSetup, IEqualityComparer<Object> comparer = null, Action<Binder_Collection> doneCallback = null )
		{
			_collection	= collection;
			_comparer	= comparer;

			_setup = setup;
			_groupInjector = groupInjector;
			_groupSetup = groupSetup;
			_doneCallback = doneCallback;
		}

		private readonly IEnumerable					_collection;
		private readonly IEqualityComparer<Object>		_comparer;

		private readonly SetupCallback			_setup;
		private readonly GroupInjectorCallback	_groupInjector;
		private readonly GroupSetupCallback		_groupSetup;
		private readonly Action<Binder_Collection>	_doneCallback;

		public	IEnumerable					InnerCollection	=> _collection;
		public	IEqualityComparer<Object>	Comparer		=> _comparer;

		public  Action<Binder_Collection>	DoneCallback	=> _doneCallback;
		public	SetupCallback				Setup			=> _setup;
		public	GroupSetupCallback			GroupSetup		=> _groupSetup;
		public	GroupInjectorCallback		GroupInjector	=> _groupInjector;

		public	Boolean						IsEmpty			=> _collection == null || !_collection.GetEnumerator( ).MoveNext( );

		public IEnumerator GetEnumerator()
		{
			return _collection.GetEnumerator();
		}
		
		public delegate		void		SetupCallback			( GameObject widget, Object data, Boolean isNew, Int32 index );
		public delegate		Transform	GroupSetupCallback		( GameObject widget, Object data );
		public delegate		void		GroupInjectorCallback	( Object data1, Object data2, Int32 dataIndex, Action<Int32, Object> inject );
	}
}