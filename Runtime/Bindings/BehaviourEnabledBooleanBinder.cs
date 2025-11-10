using System;
using UnityEngine;

namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Boolean))]
	public class BehaviourEnabledBooleanBinder : ABinder
	{
		[SerializeField]
		private						Behaviour		_true;
		[SerializeField]
		private						Behaviour		_false;
		
		private						Func<Boolean>	_getter;

		protected override			void			Bind			( Boolean init )				
		{
		    var isTrue					= _getter( );

		    if( _true != null )			_true.enabled =  isTrue;
			if( _false != null )		_false.enabled =  !isTrue;
		}

		private						void			Awake			( )				
		{
			Init( ref _getter );
		}
	}
}