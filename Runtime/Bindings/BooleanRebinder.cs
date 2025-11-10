using System;
using UnityEngine;

namespace Flexy.UI.Bindings
{
	[BindTo(typeof(Boolean))]
	public class BooleanRebinder : ABinder
	{
		[SerializeField]
		private ABinder _binder;
		
		protected override void Bind			( Boolean init )
		{
			_binder.Rebind( );
		}
	}
}